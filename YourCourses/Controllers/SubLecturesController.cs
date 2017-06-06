using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using YourCourses.Models;
using Microsoft.AspNet.Identity;

namespace YourCourses.Controllers
{
    public class SubLecturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SubLectures
        public ActionResult Index()
        {
            var subLectures = db.SubLectures.Include(s=>s.Lecture);
            return View(subLectures.ToList());
            
        }

        public ActionResult Create()
        {
            ViewBag.LectureLectureId = new SelectList(db.Lectures, "LectureId", "LectureName");
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubLectureId,SubName,LectureLectureId,LectureAdminOutput")]SubLecture subLecture)
        {
            var sublect = new SubLecture
            {
                ArtistId = User.Identity.GetUserId(),
                SubName = subLecture.SubName,
                LectureLectureId=subLecture.LectureLectureId,
                LectureAdminOutput = subLecture.LectureAdminOutput,
                CurrentRating=subLecture.CurrentRating


            };
            if (ModelState.IsValid)
            {
                db.SubLectures.Add(sublect);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LectureLectureId = new SelectList(db.Lectures, "LectureId", "LectureName", subLecture.LectureLectureId);
            return View(sublect);
        }

    }
}