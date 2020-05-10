﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string SiteURL { get; set; }
        public string Season { get; set; }
        public int Rating { get; set; }
        public int ZipCode { get; set; }
        public string Address { get; set; }
        public bool Indoor { get; set; }
        public string Type { get; set; }
    }
}