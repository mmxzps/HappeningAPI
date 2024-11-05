using EventVault.Data;
using EventVault.Data.Repositories;
using EventVault.Data.Repositories.IRepositories;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using DotNetEnv;
using EventVault.Models;
using EventVault.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Azure.Communication.Email;
using Sprache;

namespace EventVault
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<EventVaultDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext")));

            // Identity framework
            builder.Services.AddAuthorization();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<EventVaultDbContext>()
                .AddDefaultTokenProviders();

            // JWT Authentication
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            })
            .AddGoogle(options =>
            {
                options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
                options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_SECRET");
                options.CallbackPath = "/signin-google";
                options.Scope.Add("profile");
                options.Scope.Add("email");
            });

            builder.Services.AddScoped(options =>
            {
                var azureConnectionString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");

                return new EmailClient(azureConnectionString);
            });

            // Other services
            builder.Services.AddControllers();         
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Services & repositories
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddHttpClient<IEventServices, EventServices>();
            builder.Services.AddHttpClient<IKBEventServices, KBEventServices>();
            
          
            builder.Services.AddTransient<IAuthServices, AuthServices>();
            builder.Services.AddTransient<IRoleServices, RoleServices>();
            builder.Services.AddTransient<IAdminServices, AdminServices>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var roleService = scope.ServiceProvider.GetRequiredService<IRoleServices>();
                await roleService.InitalizeRolesAsync();
            }

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

