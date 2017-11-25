using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class Schedule
    {
        public Schedule()
        {
            Groups = new HashSet<Groups>();
        }

        public int ScheduleId { get; set; }
        public int? GroupId { get; set; }
        public DateTime? Time { get; set; }
        public string DaysOfTheWeek { get; set; }

        public ICollection<Groups> Groups { get; set; }
    }
}
