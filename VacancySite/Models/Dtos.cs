namespace VacancySite.Models
{
    public record CreateVacancyDto(string Position, string Company, decimal Salary, bool IsOpen);

    public record UpdateVacancyDto(string? Position, string? Company, decimal? Salary, bool? IsOpen);

    public record CreateApplicationDto(string Name, string Email, string Letter);
}
