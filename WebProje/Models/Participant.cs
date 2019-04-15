using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProje.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Race")]
        public int RaceId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Race Race { get; set; }
    }
}