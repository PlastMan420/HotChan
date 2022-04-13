using HotChan.DataAccess.Data;
using HotChan.DataAccess.DataLoader;
using HotChan.DataAccess.Repository;
using HotChan.DataAccess.Users;
using HotChan.DataBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//Microsoft.EntityFrameworkCore.Design import is required for efcore-tools. this is probably due to the "implicitusing usings" flag.
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;

// For Entity Framework
//var csBuilder = new SqlConnectionStringBuilder(
//                configuration.GetConnectionString("hotchandatabase-postgres-dev"));
//csBuilder.Password = configuration["db:password"];
//string _connection = csBuilder.ConnectionString;

builder.Services.AddPooledDbContextFactory<HotChanContext>(options => options.UseNpgsql(configuration.GetConnectionString("hotchandatabase-postgres-dev")));

// migrate any database changes on startup (includes initial db creation)
//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<HotChanContext>();
//    dataContext.Database.Migrate();
//}

// For Identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<HotChanContext>()
//    .AddDefaultTokenProviders();

// Adding Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})

//// Adding Jwt Bearer
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = configuration["JWT:ValidAudience"],
//        ValidIssuer = configuration["JWT:ValidIssuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
//    };
//});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

builder.Services
        //.AddSingleton<IUserRepository, UserRepository>()
        .AddMemoryCache()
        //.AddSha256DocumentHashProvider(HashFormat.Hex)
        .AddGraphQLServer()
        .RegisterDbContext<HotChanContext>(DbContextKind.Pooled)
        //.AddAuthorization()
        .AddQueryType<PostQuery>()
        //.AddTypeExtension<UserQuery>()
        //.AddTypeExtension<PostQuery>()
        //.UseAutomaticPersistedQueryPipeline()
        //.AddMutationType<PostMutation>()
        .AddDataLoader<PostsBatchDL>()
        //.AddDataLoader<UsersBatchDL>()
        //.AddDataLoader<UserSubmissionsDL>()
        //.AddType<UploadType>()
        ;

var app = builder.Build();

app.MapGraphQL();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseCors();

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}