using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MilwaukeeActivies.Models
{
    
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int Zipcode { get; set; }
        public IEnumerable<SelectListItem> Interests
        {
            get
            {
                return new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Football", Value = "Football"},
                        new SelectListItem { Text = "Rugby", Value = "Rugby"},
                        new SelectListItem { Text = "Horse Racing", Value = "Horse Racing"}
                    };
            }
        }

        public List<Favorite> Favorites { get; set; }
        
        
        [ForeignKey("IdentityUser")] public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

    }
}
