using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class Abonement
    {
        public Abonement()
        {
            Visitor = new HashSet<Visitor>();
        }

        public int AbonementId { get; set; }
        public int? NumberOfVisits { get; set; }
        public double? Price { get; set; }

        public ICollection<Visitor> Visitor { get; set; }
    }
}
