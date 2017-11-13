using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class Visitor
    {
        public int VisitorId { get; set; }
        public string NameV { get; set; }
        public string SurnameV { get; set; }
        public int? GroupId { get; set; }
        public int? AbonementId { get; set; }

        public Abonement Abonement { get; set; }
        public Groups Group { get; set; }
    }
}
