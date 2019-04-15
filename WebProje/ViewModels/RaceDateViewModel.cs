using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProje.Models;

namespace WebProje.ViewModels
{
    public class RaceDateViewModel
    {
        public Race Race { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}