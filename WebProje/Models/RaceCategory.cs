using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProje.Models
{
    public class RaceCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}