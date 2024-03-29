using HotChan.DataAccess.Repositories;
using HotChan.DataBase;
using HotChan.DataBase.Models.Entities;
using HotChocolate.AspNetCore.Authorization;
using HotChan.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HotChocolate.Types.Pagination;
using HotChan.Api.Schema.Data;
using HotChan.Api.Exceptions;

namespace HotChan.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("hotchandatabase-postgres-dev");

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddScoped<HotChanContext>();
        builder.Services.AddScoped<IPasswordHasher<User>, Services.PasswordHasher<User>>();
        builder.Services.AddScoped<PostRepository>();
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddControllers();
        builder.Services.AddPooledDbContextFactory<HotChanContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("HotChan.Api")));
        //builder.Services.AddEntityFrameworkNpgsql().AddDbContext<HotChanContext>(opt =>
        //   opt.UseNpgsql(builder.Configuration.GetConnectionString(connectionString)));

        //you can pass in here a custom User type that implements IdentityUser
        builder.Services.AddIdentity<User, Role>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                    //Other options go here
                }
            )
            .AddEntityFrameworkStores<HotChanContext>()
            .AddDefaultTokenProviders();
        //builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<HotChanContext>();

        //builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); builder.Services.AddScoped<PostRepository>();

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

        builder.Services.AddAuthentication(cfg => {
            cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                ValidateAudience = false
            };
        });

        builder.Services.AddAuthorization();

        builder.Services
            .AddGraphQLServer()
            .RegisterDbContext<HotChanContext>(DbContextKind.Pooled)
            .AddAuthorization()
            .AddProjections() // order matters: UsePaging > UseProjection > UseFiltering > UseSorting
            .AddFiltering()
            .AddSorting()
            .AddQueryType<HotChanQuery>()
            .AddMutationType<HotChanMutation>()
            .SetPagingOptions(new PagingOptions { MaxPageSize = 20 })
            ;

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
       
        app.AddGlobalErrorHandler();
        app.UseCors();
        app.UseHttpsRedirection();
        app.MapGraphQL();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}
