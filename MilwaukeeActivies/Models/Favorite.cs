using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MilwaukeeActivies.Models
{
    public class Favorite
    {

        [Key]
        public int FavoriteID { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        [ForeignKey("Activity")]
        public int ActivityID { get; set; }
    }
}
