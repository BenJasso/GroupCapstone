using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository
{
    public class ActivityType
    {
        [Key]
        public int ActivityTypeId { get; set; }
        public string ActivityTypes { get; set; }
    }
}
