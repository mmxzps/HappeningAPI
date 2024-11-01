using EventVault.Data;
using EventVault.Data.Repositories;
using EventVault.Data.Repositories.IRepositories;
using EventVault.Services;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using EventVault.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EventVault
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<EventVaultDbContext>( options =>

                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext")));

            //CORS-Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("LocalReact", policy =>
                {
                    //lägg in localhost reactapp som kör när vi startar react. 
                    policy.WithOrigins("http://localhost:5174")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


            //Policy som är mindre säker och tillåter vem som helst att ansluta. Om god säkerhet finns i api med auth, så kan den här användas.
            //builder.Services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(policy =>
            //    {
            //        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            //    });
            //});

            // Identity framework
            builder.Services.AddAuthorization();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<EventVaultDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

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
            });

            // Other services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Services & repos
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IEventServices, EventServices>();
            builder.Services.AddScoped<IVisitStockholmServices, VisitStockholmServices>();
            builder.Services.AddScoped<ITicketMasterServices, TicketMasterServices>();

            builder.Services.AddHttpClient<IEventbriteServices, EventbriteServices>();
            builder.Services.AddTransient<IAuthServices, AuthServices>();


            var smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER");
            var smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
            var smtpUser = Environment.GetEnvironmentVariable("SMTP_USER");
            var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS");

            builder.Services.AddTransient<IEmailSender, EmailSender>(i =>
               new EmailSender(
                   smtpServer,
                   smtpPort,
                   smtpUser,
                   smtpPass
               )
            );

            builder.Services.AddControllers();

            builder.Services.AddHttpClient();
            // Controllers

            builder.Services.AddControllers();

            // Swagger - Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Services
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IEventServices, EventServices>();
            builder.Services.AddScoped<IKBEventServices, KBEventServices>();

            var app = builder.Build();

            // Use corspolicy set above ^.
            app.UseCors("LocalReact");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapIdentityApi<IdentityUser>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
