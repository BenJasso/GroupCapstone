using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilwaukeeActivies.Models
{
    public class InterestModel
    {
        public IList<SelectListItem> AvailableInterestTypes { get; set; }
        public IList<string> SelectedInterests { get; set; }

        public InterestModel()
        {
            SelectedInterests = new List<string>();
            AvailableInterestTypes = null;
        }
    }
}
