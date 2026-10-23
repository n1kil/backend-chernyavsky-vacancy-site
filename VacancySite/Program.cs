using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System;
using System.Linq;
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

            app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

            app.MapGet("/api/vacancies", () =>
            {

                return Results.Ok(Storage.Vacancies);

            });

            app.MapGet("/api/vacancies/{id}", (int id) =>
            {
                foreach(var vacancy in Storage.Vacancies)
                {
                    if(vacancy.Id == id)
                    {
                        return Results.Ok(vacancy);
                    }
                }
                return Results.NotFound(new {error = "Вакансии с таким айди не найдено"});
            });

            app.MapPost("/api/vacancies", (CreateVacancyDto dto) =>
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
                return Results.Created($"/api/vacancies/{vacancy.Id}", vacancy);
            });


            app.MapGet("/api/vacancies/{id}/applications", (int id) =>
            {
                var vacancyExists = Storage.Vacancies.Any(v => v.Id == id);

                if (!vacancyExists)
                {
                    return Results.NotFound(new { error = "Вакансия не найдена" });
                }

                var applications = Storage.Applications.Where(a => a.AppliedVacancyId == id).ToList();

                return Results.Ok(applications);

            });


            app.MapPost("/api/vacancies/{id}/applications", (int id, CreateApplicationDto dto) =>
            {
                var vacancy = Storage.Vacancies.FirstOrDefault(v => v.Id == id);
                if (vacancy == null)
                {
                    return Results.NotFound(new { error = "Вакансия не найдена" });
                }

                if (!vacancy.IsOpen)
                {
                    return Results.Conflict(new { error = "Нельзя откликнуться на закрытую вакансию" });
                }

                bool alreadyApplied = Storage.Applications.Any(a => a.AppliedVacancyId == id && a.Email == dto.Email);
                if (alreadyApplied)
                {
                    return Results.Conflict(new { error = "Вы уже откликались на эту вакансию с этим email" });
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return Results.BadRequest(new { error = "Поле Name обязательно", field = "name" });
                }

                if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                {
                    return Results.BadRequest(new { error = "Некорректный Email", field = "email" });
                }

                var application = new Application(dto.Name, dto.Email, dto.Letter, id);
                Storage.Applications.Add(application);

                vacancy.CountOfApplications++;

                return Results.Created($"/api/vacancies/{id}/applications/{application.Id}", application);
            });

            app.MapPatch("/api/vacancies/{id}", (int id, UpdateVacancyDto dto) =>
            {
                var vacancy = Storage.Vacancies.FirstOrDefault(v => v.Id == id);
                if (vacancy == null)
                {
                    return Results.NotFound(new { error = "Вакансия не найдена" });
                }

                if (dto.Position != null && string.IsNullOrWhiteSpace(dto.Position))
                {
                    return Results.BadRequest(new { error = "Поле Position не может быть пустым", field = "position" });
                }
                if (dto.Company != null && string.IsNullOrWhiteSpace(dto.Company))
                {
                    return Results.BadRequest(new { error = "Поле Company не может быть пустым", field = "company" });
                }
                if (dto.Salary != null && dto.Salary < 0)
                {
                    return Results.BadRequest(new { error = "Зарплата не может быть отрицательной", field = "salary" });
                }

                vacancy.Update(dto.Position, dto.Company, dto.Salary, dto.IsOpen);

                return Results.Ok(vacancy);
            });


            app.MapDelete("/api/vacancies/{id}", (int id) =>
            {
                var vacancy = Storage.Vacancies.FirstOrDefault(v => v.Id == id);
                if (vacancy == null)
                {
                    return Results.NotFound(new { error = "Вакансия не найдена" });
                }

                Storage.Vacancies.Remove(vacancy);

                return Results.NoContent();
            });

            app.MapDelete("/api/vacancies/{vacancyId}/applications/{applicationId}", (int vacancyId, int applicationId) =>
            {
                var application = Storage.Applications.FirstOrDefault(a => a.Id == applicationId && a.AppliedVacancyId == vacancyId);

                if (application == null)
                {
                    return Results.NotFound(new { error = "Отклик не найден" });
                }

                Storage.Applications.Remove(application);

                var vacancy = Storage.Vacancies.FirstOrDefault(v => v.Id == vacancyId);
                if (vacancy != null && vacancy.CountOfApplications > 0)
                {
                    vacancy.CountOfApplications--;
                }

                return Results.NoContent();
            });



            app.Run();
        }
    }
}
