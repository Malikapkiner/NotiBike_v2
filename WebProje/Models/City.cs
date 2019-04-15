using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProje.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Sehir { get; set; }
    }
}