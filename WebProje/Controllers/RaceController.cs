using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProje.Models;
using WebProje.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebProje.Controllers
{
    public class RaceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public RaceController()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: Race
        public ActionResult Index()
        {
            return View(db.Race.ToList());
        }
        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race rc = db.Race.Find(id);
            if (rc == null)
            {
                return HttpNotFound();
            }
            var isKatilma = false;
            if (User.Identity.IsAuthenticated)
            {
                var user_id = User.Identity.GetUserId();
                var katilma = db.Participants.FirstOrDefault(p => p.UserId == user_id && p.RaceId == id);
                if (katilma != null)
                {
                    isKatilma = true;
                }

            }
            ViewBag.isKatilma = isKatilma;

            return View(rc);
        }

        [Authorize]
        public ActionResult Create()
        {
            var model = new RaceCategoryViewModel();
            var usrId = User.Identity.GetUserId();
            if (userManager.IsInRole(usrId, "Admin") || userManager.IsInRole(usrId, "Premium"))
            {

                var categories = db.RaceCategories;
                model.Categories = categories.Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() });

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Yetkiniz yok");
                return RedirectToAction("Index","Home",null);
            }
        }
        [HttpPost]
        public ActionResult Create(RaceCategoryViewModel viewModel)
        {
            var usrId = User.Identity.GetUserId();
            if (userManager.IsInRole(usrId, "Admin") || userManager.IsInRole(usrId, "Premium"))
            {
                var userId = User.Identity.GetUserId();
                viewModel.Race.UserId = userId;
                db.Race.Add(viewModel.Race);
                db.SaveChanges();
            }
            return RedirectToAction("MyRaces", "Profile", null);
        }

        [Authorize]
        public ActionResult Update(int? id)
        {
            var usrId = User.Identity.GetUserId();
            if (userManager.IsInRole(usrId, "Admin") || userManager.IsInRole(usrId, "Premium"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var model = new RaceDateViewModel();
                var categories = db.RaceCategories;
                model.Categories = categories.Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() });
                Race r = db.Race.Find(id);
                model.Race = r;
                model.Date = r.Date.ToShortDateString();
                //model.EndDate = evnt.FinishDate.ToShortDateString(); ÖNEMLİ FİNİSHDATE İÇİN AÇ
                if (r == null)
                {
                    return HttpNotFound();
                }
                if (User.Identity.GetUserId() == r.UserId)
                    return View(model);


            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int? id, RaceDateViewModel model)
        {
            var user = User.Identity.GetUserId();
            if (userManager.IsInRole(user, "Admin") || userManager.IsInRole(user, "Premium"))
            {
                var r = db.Race.Find(id);
                r.RaceName = model.Race.RaceName;
                r.CategoryId = model.Race.CategoryId;
                //r.Date = DateTime.Parse(model.Date);
                r.Date = model.Race.Date;
                r.Time = model.Race.Time;
                r.Location = model.Race.Location;
                r.Description = model.Race.Description;
                r.Coor_X = model.Race.Coor_X;
                r.Coor_Y = model.Race.Coor_Y;

                db.SaveChanges();
            }

            return RedirectToAction("MyRaces", "Profile", null);

        }
        [Authorize]
        public ActionResult Delete(int? id)
        {
            //var user = User.Identity.GetUserId();
            //if (userManager.IsInRole(user, "Admin") || userManager.IsInRole(user, "Premium"))
            //{
            //    var yaris = db.Race.Find(id);
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Race r = new Race();

            //    if (yaris != null && user == r.UserId)
            //    {
            //        var result = db.Race.Remove(yaris);
            //        db.SaveChanges();

            //    }
            //}
            //return RedirectToAction("MyRaces","Profile",null);

       
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race evnt = db.Race.Find(id);
            if (evnt == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() == evnt.UserId)
                return View(evnt);
            else
                return RedirectToAction("Myraces","Profile",null);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Race evnt = db.Race.Find(id);
            db.Race.Remove(evnt);
            db.SaveChanges();
            return RedirectToAction("MyRaces","Profile",null);
        }


        public ActionResult Participate(int id, Participant Participant)
        {
            var userId = User.Identity.GetUserId();
            var controlRace = db.Participants.FirstOrDefault(m => m.RaceId == id && m.UserId == userId);
            var controlUser = db.Race.FirstOrDefault(r => r.UserId == userId && r.Id == id);
            var contolRaceDate = db.Race.FirstOrDefault(d => d.Id == id);
            var dateControl = DateTime.Compare(contolRaceDate.Date, DateTime.Now.AddDays(1));
            if (controlUser == null && controlRace == null && dateControl >= 0)
            {
                Participant.RaceId = id;
                Participant.UserId = userId;
                db.Participants.Add(Participant);
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = id });
            }
            else
            {
                return RedirectToAction("Detail", new { id = id });
            }

        }

        public ActionResult NotParticipate(int id, Participant participant)
        {
            var userId = User.Identity.GetUserId();
            var model = db.Participants.FirstOrDefault(p => p.UserId == userId && p.RaceId == id);
            if (model == null)
            {
                ViewBag.errorMessage = "Hatalı bir durum oluştu";
                return View(ViewBag);
            }
            else
            {
                db.Participants.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Detail", new { id = id });
            }

        }
    }
}