using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CoreWebAPIs.Controllers
{

    public class SqlQueryRequest
    {
        public string SqlQuery { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public int? MaxRows { get; set; }
        public int? TimeoutSeconds { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<QueryController> _logger;

        public QueryController(IConfiguration configuration, ILogger<QueryController> logger)
        {
            _configuration = configuration;
            _connectionString = "Server=ebriefingdb.database.windows.net;Database=eBriefingDB;User ID=CSM;Password=Puzzle#888;TrustServerCertificate=True;";
            _logger = logger;
        }

        [HttpPost]
        [Route("ExecuteQuery")]
        public async Task<IActionResult> ExecuteQuery([FromBody] SqlQueryRequest request)
        {
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(request.SqlQuery))
                {
                    return BadRequest(new { Error = "The sqlQuery field is required." });
                }

                // Security checks
                if (!IsSelectQuery(request.SqlQuery))
                {
                    _logger.LogWarning($"Attempted non-SELECT query: {request.SqlQuery}");
                    return BadRequest(new { Error = "Only SELECT queries are allowed." });
                }

                if (ContainsDangerousKeywords(request.SqlQuery))
                {
                    _logger.LogWarning($"Dangerous keywords detected in query: {request.SqlQuery}");
                    return BadRequest(new { Error = "Query contains forbidden keywords." });
                }

                var dataTable = new DataTable();
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = request.SqlQuery;
                        command.CommandType = CommandType.Text;

                        // Set timeout
                        command.CommandTimeout = request.TimeoutSeconds ?? 30;

                        // Add parameters safely with proper type conversion
                        if (request.Parameters != null)
                        {
                            foreach (var param in request.Parameters)
                            {
                                var paramValue = ConvertFromJsonElement(param.Value);
                                command.Parameters.AddWithValue(param.Key, paramValue ?? DBNull.Value);
                            }
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // Handle maximum rows limit
                            if (request.MaxRows.HasValue)
                            {
                                int rowCount = 0;
                                while (await reader.ReadAsync() && rowCount < request.MaxRows.Value)
                                {
                                    if (dataTable.Columns.Count == 0)
                                    {
                                        // Create columns
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                                        }
                                    }

                                    // Add row
                                    var row = dataTable.NewRow();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        row[i] = reader.IsDBNull(i) ? DBNull.Value : reader.GetValue(i);
                                    }
                                    dataTable.Rows.Add(row);
                                    rowCount++;
                                }
                            }
                            else
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                }

                var result = DataTableToDictionary(dataTable);
                return Ok(new
                {
                    RowCount = result.Count,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing query");
                return StatusCode(500, new { Error = "An error occurred while executing the query." });
            }
        }

        private object ConvertFromJsonElement(object value)
        {
            if (value is JsonElement element)
            {
                switch (element.ValueKind)
                {
                    case JsonValueKind.Null:
                        return null;
                    case JsonValueKind.Number:
                        if (element.TryGetInt32(out int intValue))
                            return intValue;
                        if (element.TryGetInt64(out long longValue))
                            return longValue;
                        if (element.TryGetDecimal(out decimal decimalValue))
                            return decimalValue;
                        return element.GetDouble();
                    case JsonValueKind.String:
                        if (DateTime.TryParse(element.GetString(), out DateTime dateValue))
                            return dateValue;
                        return element.GetString();
                    case JsonValueKind.True:
                        return true;
                    case JsonValueKind.False:
                        return false;
                    default:
                        return element.ToString();
                }
            }
            return value;
        }

        private bool IsSelectQuery(string query)
        {
            var normalizedQuery = query.Trim().ToUpperInvariant();
            return normalizedQuery.StartsWith("SELECT ") &&
                   !Regex.IsMatch(normalizedQuery, @"\bINTO\b");
        }

        private bool ContainsDangerousKeywords(string query)
        {
            var normalizedQuery = query.ToUpperInvariant();
            var dangerousKeywords = new[]
            {
            "DROP", "DELETE", "UPDATE", "INSERT", "TRUNCATE", "ALTER", "MERGE",
            "EXEC", "EXECUTE", "SP_", "XP_", "CREATE", "BACKUP", "RESTORE"
        };

            return dangerousKeywords.Any(keyword =>
                Regex.IsMatch(normalizedQuery, $@"\b{keyword}\b"));
        }

        private List<Dictionary<string, object>> DataTableToDictionary(DataTable table)
        {
            var result = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                }
                result.Add(dict);
            }
            return result;
        }
    }
}
