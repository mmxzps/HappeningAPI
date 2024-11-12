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

            //CORS-Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("LocalReact", policy =>
                {
                    //l�gg in localhost reactapp som k�r n�r vi startar react. 
                    policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


            //Policy som �r mindre s�ker och till�ter vem som helst att ansluta. Om god s�kerhet finns i api med auth, s� kan den h�r anv�ndas.
            //builder.Services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(policy =>
            //    {
            //        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            //    });
            //});
          
            // Identity framework
            builder.Services.AddAuthorization();
            builder.Services.AddIdentity<User, IdentityRole>()
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

            // Adding MVC client
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowClientMVC", policy =>
                {
                    policy.WithOrigins("https://localhost:7175/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            // Other services
            builder.Services.AddControllers();         
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();

          // Event
          builder.Services.AddScoped<IEventRepository, EventRepository>();
          builder.Services.AddScoped<IEventServices, EventServices>();
          
          // KBE
          builder.Services.AddHttpClient<IKBEventServices, KBEventServices>();
          
          // Venue
          builder.Services.AddScoped<IVenueRepository, VenueRepository>();
          builder.Services.AddScoped<IVenueServices, VenueServices>();
          
          // TicketMaster
          builder.Services.AddScoped<ITicketMasterServices, TicketMasterServices>();
          
          // VisitStockholm
          builder.Services.AddScoped<IVisitStockholmServices, VisitStockholmServices>();
                 
          // Auth (Identity)
            builder.Services.AddTransient<IAuthServices, AuthServices>();
          
          // Role (Identity)
            builder.Services.AddTransient<IRoleServices, RoleServices>();
          
          // Admin
            builder.Services.AddTransient<IAdminServices, AdminServices>();
          
          // Email (Azure)
            builder.Services.AddScoped<IEmailService, EmailService>();

            // User Repo & Service
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();

            // Use CorsPolicy set above ^.
            app.UseCors("LocalReact");

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

