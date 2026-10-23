using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection.Metadata.Ecma335;
using VacancySite.Data;
using VacancySite.Models;

namespace VacancySite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            app.MapGet("/health", () => Results.Ok(new { status = "OK" }));

            app.MapGet("api/vacancies", () =>
            {

                return Results.Ok(Storage.Vacancies);

            });




            app.Run();
        }
    }
}
