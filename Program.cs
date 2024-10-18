
using EventVault.Data;
using EventVault.Data.Repositories;
using EventVault.Data.Repositories.IRepositories;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventVault
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<EventVaultDbContext>( options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext")));

            // Identity framework

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<EventVaultDbContext>();

            builder.Services.AddControllers();


            // Controllers

            builder.Services.AddControllers();

            // Swagger - Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Added configuration to read key from appsetting
            builder.Configuration.AddJsonFile("appsettings.Development.json", false, reloadOnChange: true);

            // Services
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IEventServices, EventServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapIdentityApi<IdentityUser>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();


        }
    }
}
