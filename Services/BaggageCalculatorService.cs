
using CoreWebAPIs.Context;
using CoreWebAPIs.Models;

namespace CoreWebAPIs.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;
        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DbSaveStatusModel> SaveChangesAsync()
        {
            DbSaveStatusModel dbSaveStatusModel = new DbSaveStatusModel();
            try
            {
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    dbSaveStatusModel.IsSaveSuccessfully = true;
                }
            }
            catch (Exception ex)
            {
                dbSaveStatusModel.IsSaveSuccessfully = false;
                dbSaveStatusModel.Error = ex.Message.ToString();

            }

            return dbSaveStatusModel;
        }

    }
}
