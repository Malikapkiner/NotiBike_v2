using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProje.Models
{
    public class Race
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RaceName { get; set; }
        public int CategoryId { get; set; }
        [DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:dd/MM/yy}",ApplyFormatInEditMode =true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time), DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        public string Coor_X { get; set; }
        [Required]
        public string Coor_Y { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }


        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Participant> Participant { get; set; }
        public virtual RaceCategory Category { get; set; }












    }
}