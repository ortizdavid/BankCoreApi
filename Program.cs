
using System.Data;
using System.Data.SqlClient;
using BankCoreApi.Extensions;
using BankCoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        // Add services to the container.
        // Connection String / DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(connectionString)
        );
        // Dapper
        builder.Services.AddScoped<IDbConnection>(
            sp => new SqlConnection(connectionString)
        );

        builder.Services.AddHttpContextAccessor();
        // Controllers
        builder.Services.AddControllers();
        // Logging
        builder.Logging.AddConsole();
        //Add repositories 
        builder.Services.AddBankRepositories();
        //Add repositories 
        builder.Services.AddBankServices();
        // Add JWT authentication
        builder.Services.AddJwtAuthentication(configuration);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //**************************************************************

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}