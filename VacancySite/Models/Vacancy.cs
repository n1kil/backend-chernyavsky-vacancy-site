using System;
using System.Collections.Generic;

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


        public List<Application> Applications { get; set; } = new();

        public Vacancy() { }
        public Vacancy(string position, string company, decimal salary, bool isOpen)
        {
            Position = position;
            Company = company;
            Salary = salary;
            IsOpen = isOpen;
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
