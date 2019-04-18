using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProje.Models;
using WebProje.ViewModels;

namespace WebProje.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        // GET: Admin
        public AdminController()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        public ActionResult Index()
        {

            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                var result = roleManager.Create(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                } 
            }
            return View(name);
        }

        [HttpGet]
        public ActionResult AddRole()
        {

            var roles = db.Roles;
            var model = new UserRoleViewModel();

            //model.DateOfBirth = profile.DateOfBirth.ToShortDateString();
            model.rol = roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });

            return View(model);
        }

        [HttpPost]
        public ActionResult AddRole([Required]string id, string usrid)
        {
            return RedirectToAction("Index");
        }
    }
}