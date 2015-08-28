using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAIDManangementSystem.Models
{
    public class IdModel
    { 
        public string SAId { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; } 
        public bool IsCitizen { get; set; } 
        public string DateConverter { get; set; } 
        public long GeneratedId { get; set; }
        public int Age { get; set; }

    }
}