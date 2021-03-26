using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BugReport.Models
{
    class PersonDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProblemTitle { get; set; }
        public string Description { get; set; }

        public List<string> Images = new List<string>();

        private static PersonDataModel _person;

        public static void savePerson(PersonDataModel person)
        {
            _person = person;
        }

        public static PersonDataModel returnPerson()
        {
            return _person;
        }

    }
}
