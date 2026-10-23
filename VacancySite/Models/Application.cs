using System.IO;

namespace VacancySite.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        
        public string Email { get; private set; }

        public string Letter {  get; private set; }

        public int AppliedVacancyId { get; private set; }

        private static int _nextId = 0;
        public Application(string name, string email, string letter, int appliedVacancyId)
        {
            Id = ++_nextId;
            Name = name;
            Email = email;
            Letter = letter;
            AppliedVacancyId = appliedVacancyId;
            PostApplication();
        }


        public bool PostApplication()
        {
            if(AppliedVacancy.IsOpen)
            {
                AppliedVacancy.AddApplication();
                return true;
            }
            return false;
        }
        
    }
}
