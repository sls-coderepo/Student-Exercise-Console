using System;
using System.Collections.Generic;
using System.Text;

namespace studentExercises.Model
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort  Cohort { get; set; }
        public string Speciality { get; set; }

        public Instructor()
        {
            Cohort = new Cohort();
        }
    }
}
