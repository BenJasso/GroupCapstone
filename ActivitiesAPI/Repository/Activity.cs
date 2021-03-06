﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        [ForeignKey("City")]
        public string CityName { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string EventName { get; set; }
        public string Company { get; set; }
        public string SiteURL { get; set; }
        public string Season { get; set; }
        public List<Review> Reviews { get; set; }
        public int ZipCode { get; set; }
        public string Address { get; set; }
       
        public bool Indoor { get; set; }
        [ForeignKey("ActivityTypes")]
        public string ActivityTypes { get; set; }
        public string Description { get; set; }

        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
