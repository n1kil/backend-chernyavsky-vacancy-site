using System.Collections.Generic;
using VacancySite.Models;

namespace VacancySite.Data
{
    public static class Storage
    {
        public static List<Vacancy> vacancies = new List<Vacancy>();
        public static List<Application> applications = new List<Application>();
    }
}
