using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProje.Models;
using WebProje.ViewModels;

namespace WebProje.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // GET: Profile
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var model = db.UserDetails.FirstOrDefault(x => x.UserId == userId);
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ProfileDetail(ApplicationUser User)
        {
            var model = db.UserDetails.FirstOrDefault(x => x.UserId == User.Id);
            return View(model);
        }
        public ActionResult EditProfile()
        {
            var userId = User.Identity.GetUserId();
            var profile = db.UserDetails.FirstOrDefault(p => p.UserId == userId);
            var cities = db.Cities;
            var model = new ProfileCityViewModel();

            model.UserDetail = profile;
            //model.DateOfBirth = profile.DateOfBirth.ToShortDateString();
            model.Cities = cities.Select(a => new SelectListItem { Text = a.Sehir, Value = a.Id.ToString() });

            return View(model);
        }
        [HttpPost]
        public ActionResult EditProfile(UserDetail UserDetail, HttpPostedFileBase file)
        {
            var userDetail = db.UserDetails.FirstOrDefault(p => p.Id == UserDetail.Id);
            userDetail.Name = UserDetail.Name;
            userDetail.LastName = UserDetail.LastName;
            userDetail.DateOfBirth = UserDetail.DateOfBirth;
            userDetail.CityId = UserDetail.CityId;
            userDetail.Location = UserDetail.Location;
            userDetail.About = UserDetail.About;
            if (file != null)
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Image/")
                                                      + file.FileName);
                UserDetail.PhotoUrl = file.FileName;
                userDetail.PhotoUrl = UserDetail.PhotoUrl;
                
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult MyRaces()
        {
            var userId = User.Identity.GetUserId();
            var races = db.Race.Where(p => p.UserId == userId);

            return View(races.ToList());
        }

        public ActionResult MyRaceList()
        {
            var userId = User.Identity.GetUserId();
            var myEvents = db.Participants.Where(e => e.UserId == userId);
            return View(myEvents.ToList());
        }
    }
}