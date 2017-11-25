using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Groups> Groups { get; set; }
        public PageViewModel PageViewModel { get; set; }

    }
}
