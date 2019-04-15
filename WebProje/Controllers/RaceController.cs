﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebProje.Models;
using WebProje.ViewModels;
using Microsoft.AspNet.Identity;


namespace WebProje.Controllers
{
    public class RaceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
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
            var categories = db.RaceCategories;
            model.Categories = categories.Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() });

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(RaceCategoryViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            viewModel.Race.UserId = userId;
            db.Race.Add(viewModel.Race);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Update(int? id)
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
            else
                return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int? id, RaceDateViewModel model)
        {

            var r = db.Race.Find(id);
            r.RaceName = model.Race.RaceName;
            r.CategoryId = model.Race.CategoryId;
            r.Date = DateTime.Parse(model.Date);
            var user = User.Identity.GetUserId();
            db.SaveChanges();

            return RedirectToAction("Index");
            
        }
        [Authorize]
        public ActionResult Delete(int? id)
        {
            var yaris = db.Race.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race r = new Race();
            var user = User.Identity.GetUserId();
            if (yaris!=null && user==r.UserId)
            {
                var result = db.Race.Remove(yaris);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult NotParticipate()
        {
            return View();
        }
    }
}