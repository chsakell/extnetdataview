using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Model
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsGoodActor { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }
        public string FamousFor { get; set; }
        public int Age
        {
            get { return DateTime.Today.Year - DateOfBirth.Year; }
        }
    }
}