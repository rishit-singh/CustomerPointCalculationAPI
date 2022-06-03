using System;
using CustomerPointCalculationAPI;
using CustomerPointCalculationAPI.Tests;


namespace CustomerPointCalculationAPI
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            Database.Connect();

            app.Run();
        }
    }
}
