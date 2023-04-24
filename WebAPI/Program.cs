using Application.Applications;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.Generics;
using Domain.Interfaces.Services;
using Domain.Services;
using Entities.Entities;
using Infra.Configs;
using Infra.Repositories;
using Infra.Repositories.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Token;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<Context>(
                   options =>
                   {
                       options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"));
                       options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                   }
                );
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<Context>();

            builder.Services.AddSingleton(typeof(IGenerics<>), typeof(GenericRepository<>));
            builder.Services.AddSingleton<INews, NewsRepository>();
            builder.Services.AddSingleton<IUser, UserRepository>();

            builder.Services.AddSingleton<INewsServices, NewsServices>();

            builder.Services.AddSingleton<INewsApplication, NewsApplication>();
            builder.Services.AddSingleton<IUserApplication, UserApplication>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "Teste.Securiry.Bearer",
                    ValidAudience = "Teste.Securiry.Bearer",
                    IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                };

                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}