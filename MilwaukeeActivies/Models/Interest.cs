using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MilwaukeeActivies.Models
{
    public class Interest
    {

        [Key]
        public int InterestID { get; set; }
        public bool Indoor { get; set; }
        public int ActivityTypeId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

}
}
