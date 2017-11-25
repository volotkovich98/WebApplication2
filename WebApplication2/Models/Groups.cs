using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class Groups
    {
        public Groups()
        {
            Visitor = new HashSet<Visitor>();
        }

        public int GroupId { get; set; }
        public int? InstructorId { get; set; }
        public string Groupname { get; set; }
        public int? NumberOfLessons { get; set; }
        public int? ScheduleId { get; set; }

        public Instructor Instructor { get; set; }
        public Schedule Schedule { get; set; }
        public ICollection<Visitor> Visitor { get; set; }
    }
}
