using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProje.Models;

namespace WebProje.ViewModels
{
    public class UserRoleViewModel
    {
        public ApplicationUser apUser { get; set; }
        public IEnumerable<SelectListItem> rol { get; set; }
    }
}