using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MilwaukeeActivies.Models
{
    public class City
    {

        [Key]
        public int CityID { get; set; }
        public string State { get; set; }
        public string CityName { get; set; }
        [ForeignKey("Activity")]
        public int ActivityID { get; set; }
    }
}
