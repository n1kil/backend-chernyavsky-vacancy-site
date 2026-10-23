using System;

namespace VacancySite.Models
{
    public class Vacancy
    {


        public int Id { get; set; }
        public string Position { get; set; }

        public string Company { get; set; }

        public decimal Salary { get; set; }

        public bool IsOpen { get; set; }

        public int CountOfApplications { get; set; }

        private static int _nextId = 0;

        public Vacancy(string position, string company, decimal salary, bool isopen)
        {
            Id = ++_nextId;
            Position = position;
            Company = company;
            Salary = salary;
            IsOpen = isopen;
        }

        public void Update(string? position = null, string? company = null, decimal? salary = null, bool? isOpen = null)
        {
            if (position != null) Position = position;
            if (company != null) Company = company;
            if (salary is decimal s) Salary = s;
            if (isOpen is bool open) IsOpen = open;
        }


    }
}
