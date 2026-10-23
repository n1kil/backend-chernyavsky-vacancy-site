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

            app.MapGet("api/vacancies/{id}", (int id) =>
            {

            });

            app.MapPost("api/vacancies", (CreateVacancyDto dto) =>
            {

                if (string.IsNullOrWhiteSpace(dto.Position))
                {
                    return Results.BadRequest(new {error = "Поле должности является обязательным", field = "position" });
                }

                if (string.IsNullOrWhiteSpace(dto.Company))
                {
                    return Results.BadRequest(new { error = "Поле компании является обязательным", field = "company" });
                }

                if(dto.Salary < 0)
                {
                    return Results.BadRequest(new { error = "Зарплата не может быть отрицательной", field = "salary" });
                }


                var vacancy = new Vacancy(dto.Position, dto.Company, dto.Salary, dto.IsOpen);
                Storage.Vacancies.Add(vacancy);
                return Results.Created($"api/vacancies/{vacancy.Id}", vacancy);
            });


            

            app.Run();
        }
    }
}
