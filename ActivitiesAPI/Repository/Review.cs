using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public int Rating { get; set; }
        public int CustomerId { get; set; }
    }
}
