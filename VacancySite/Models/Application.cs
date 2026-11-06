using System.IO;

namespace VacancySite.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }

        public string Letter {  get;  set; }

        public int AppliedVacancyId { get; set; }

        public Application() { }
        public Application(string name, string email, string letter, int appliedVacancyId)
        {
            Name = name;
            Email = email;
            Letter = letter;
            AppliedVacancyId = appliedVacancyId;
        }
        
    }
}
