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
            var usr = User.Identity.GetUserId();
            if (userManager.IsInRole(usr, "Admin"))
            {
                var usersWithRoles = (from user in db.Users
                                      select new
                                      {
                                          UserId = user.Id,
                                          Username = user.UserName,
                                          Email = user.Email,
                                          RoleNames = (from userRole in user.Roles
                                                       join role in db.Roles on userRole.RoleId
                                                       equals role.Id
                                                       select role.Name).ToList()
                                      }).ToList().Select(p => new GroupedUserViewModel()

                                      {
                                          UserId = p.UserId,
                                          Username = p.Username,
                                          Email = p.Email,
                                          Role = string.Join(",", p.RoleNames)
                                      });


                return View(usersWithRoles);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        [HttpGet]
        public ActionResult create()
        {
            var user = User.Identity.GetUserId();
            if (userManager.IsInRole(user, "Admin"))
            {
                return View();
            }
            return RedirectToAction("Index", "Home", null);
        }

        [HttpPost]
        public ActionResult create([Required]string name)
        {
            var user = User.Identity.GetUserId();
            if (userManager.IsInRole(user, "Admin"))
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
            return RedirectToAction("Index", "Home", null);
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            var user = User.Identity.GetUserId();
            if (userManager.IsInRole(user, "Admin"))
            {
                //var userId = User.Identity.GetUserId();
                //var profile = db.Users.FirstOrDefault(p => p.Id == userId);
                //model.apUser = profile;
                var model = new UserRoleViewModel();
                var allRoles = db.Roles;
                model.rol = allRoles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id });

                return View(model);
            }

            return RedirectToAction("Index", "Home", null);
        }

        [HttpPost]
        public ActionResult AddRole(/*[Required]string id,*/ UserRoleViewModel model)
        {
            var user = User.Identity.GetUserId();
            if (userManager.IsInRole(user, "Admin"))
            {
                var usr = db.Users.FirstOrDefault(m => m.UserName == model.apUser.UserName);
                if (usr != null)
                {
                    if (userManager.IsInRole(usr.Id, "Premium"))
                    {
                        ModelState.AddModelError("", "Bu üye zaten Premium");
                        return View(model);
                    }
                    else
                    {
                        if (userManager.IsInRole(usr.Id, "User"))
                        {
                            userManager.RemoveFromRole(usr.Id, "User");
                        }
                        userManager.AddToRole(usr.Id, "Premium");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Böyle bir üye yok");
                    return View(model);
                }

                return RedirectToAction("index");
            }
            return RedirectToAction("Index", "Home", null);
        }
        [HttpGet]
        public ActionResult ClearRole()
        {
            var user = User.Identity.GetUserId();
            if (userManager.IsInRole(user, "Admin"))
            {
                var model = new UserRoleViewModel();
                var allRoles = db.Roles;
                model.rol = allRoles.Select(a => new SelectListItem { Text = a.Name, Value = a.Id });

                return View(model);
            }
            return RedirectToAction("Index", "Home", null);
        }

        [HttpPost]
        public ActionResult ClearRole(UserRoleViewModel model)
        {
            var user = User.Identity.GetUserId();
            if (userManager.IsInRole(user, "Admin"))
            {
                var usr = db.Users.FirstOrDefault(u => u.UserName == model.apUser.UserName);
                if (userManager.IsInRole(usr.Id, "Premium"))
                {
                    userManager.RemoveFromRole(usr.Id, "Premium");
                    userManager.AddToRole(usr.Id, "User");
                }
                return RedirectToAction("index");
            }
            return RedirectToAction("Index", "Home", null);
        }
    }
}