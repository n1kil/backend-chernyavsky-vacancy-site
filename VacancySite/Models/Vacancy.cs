using System;

namespace VacancySite.Models
{
    public class Vacancy
    {


        public int Id { get; private set; }
        public string Position { get; private set; }

        public string Company { get; private set; }

        public decimal Salary { get; private set; }

        public bool IsOpen { get; private set; }

        public int CountOfApplications { get; private set; }

        static public int NextId {  get; private set; }

        public Vacancy(string position, string company, decimal salary, bool isopen)
        {
            Id = ++NextId;
            Position = position;
            Company = company;
            Salary = salary;
            IsOpen = isopen;
        }

        public void Update(string? position = null, string? company = null, decimal? salary = null, bool? isOpen = null)
        {
            if (position != null) Position = position;
            if (company != null) Company = company;
            if (salary != null) Salary = salary.Value;
            if (isOpen != null) IsOpen = isOpen.Value;
        }

        public void MakeClosed() => IsOpen = false;
        public void MakeOpen() => IsOpen = true;


       

    }
}
