using CoreWebAPIs;
using CoreWebAPIs.Context;
using CoreWebAPIs.GraphQL;
using CoreWebAPIs.Interfaces;
using CoreWebAPIs.Models;
using CoreWebAPIs.Repositories;
using CoreWebAPIs.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "BaggageCalculatorAzureAd");
//builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "FlightAzureAd");
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "MilesCalculatorAzureAd");
builder.Services.Configure<SabreConfig>(builder.Configuration.GetSection("Sabre"));

//builder.Services.AddHttpClient<FlightStatusService>();
builder.Services.AddHttpClient();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreWebAPIs", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BaggageCalculatorConnectionStrings"),
    sqlOptions => sqlOptions.CommandTimeout(60)
    ));

builder.Services.AddDbContext<EBriefingDbContext>(
    options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightConnectionStrings"),
    sqlOptions => sqlOptions.CommandTimeout(60)
    ));

builder.Services.AddDbContext<ProfitabilityDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("RootVisionConnectionStrings"),
        oracleOptions => oracleOptions.CommandTimeout(60));
});
builder.Services.AddDbContext<ProfitabilityOTPDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OTPConnectionStrings"),
        oracleOptions => oracleOptions.CommandTimeout(60));
});
builder.Services.AddDbContext<ProfitabilityOTPDbContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("AirportDataStagingConnectionStrings"),
        oracleOptions => oracleOptions.CommandTimeout(60));
});
builder.Services.AddHttpClient();
builder.Services.AddDbContext<PaxFlightsDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("AirportDb")));


builder.Services.AddHttpClient<LoyaltyApiService>((sp, client) =>
{
    client.BaseAddress = new Uri("https://gf-uat.ibsplc.aero/iflyloyalty/api/member-retrieval/v50/rest/");

    var byteArray = System.Text.Encoding.ASCII.GetBytes("GFINTERNAL@GF:Gf!nternal@123");
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

    client.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient<LoyaltyApiService>();

builder.Services.AddHttpClient<GenesysCallbackService>();




builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<IDataInterface, DataRepository>();
builder.Services.AddSingleton<IPhoneParserService, PhoneParserService>();

builder.Services.AddScoped<IReportsInterface, ReportsRepository>();
builder.Services.AddScoped<ReportsQuery>();

builder.Services.AddHttpClient<ISabreService, SabreService>();

builder.Services.AddGraphQLServer()
    .AddQueryType<ReportsQuery>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

builder.Services.AddScoped<IFlightsInterface, FlightsRepository>();
builder.Services.AddScoped<FlightsQuery>();
builder.Services.AddGraphQLServer()
    .AddQueryType<FlightsQuery>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebAPIs");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();


app.MapGraphQL("/graphql");

app.MapControllers();

app.Run();


//static IEdmModel GetEdmModel()
//{
//    var builder = new ODataConventionModelBuilder();
//    builder.EntitySet<OtpFlightInfo>("OtpFlightInfo");
//    builder.EntitySet<AirportCode>("AirportCode");
//    return builder.GetEdmModel();
//}