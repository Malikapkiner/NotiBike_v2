using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProje.Models
{
    public class UserDetail
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }

        public string About { get; set; }
        public string PhotoUrl { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual City City { get; set; }
    }
}