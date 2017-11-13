using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class Instructor
    {
        public Instructor()
        {
            Groups = new HashSet<Groups>();
        }

        public int InstructorId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }

        public ICollection<Groups> Groups { get; set; }
    }
}
