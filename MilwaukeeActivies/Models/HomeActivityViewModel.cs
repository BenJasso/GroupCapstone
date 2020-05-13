using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilwaukeeActivies.Models
{
    public class HomeActivityViewModel
    {

       public IEnumerable<Activities> Activities { get; set; }

       public double MinBudget { get; set; }
       public double MaxBudget { get; set; }

        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }

        public string season { get; set; }
        public bool inside { get; set; }
    }
}
