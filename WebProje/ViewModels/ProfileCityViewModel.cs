using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProje.Models;

namespace WebProje.ViewModels
{
    public class ProfileCityViewModel
    {
        public UserDetail UserDetail { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public string DateOfBirth { get; set; }
    }
}