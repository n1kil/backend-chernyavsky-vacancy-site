using System.Collections.Generic;
using VacancySite.Models;

namespace VacancySite.Data
{
    public static class Storage
    {
        public static List<Vacancy> Vacancies = new List<Vacancy>();
        public static List<Application> Applications = new List<Application>();
    }
}
