using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using YourCourses.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace YourCourses.Controllers
{
    public class SubLecturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SubLectures
        public ActionResult Index()
        {
            var subLectures = db.SubLectures.Include(s => s.Lecture);
            return View(subLectures.ToList());

        }

        public ActionResult Create()
        {
            ViewBag.LectureLectureId = new SelectList(db.Lectures, "LectureId", "LectureName");
            return View();

        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubLecture subLecture)
        {
            var sublect = new SubLecture
            {
                ArtistId = User.Identity.GetUserId(),
                SubName = subLecture.SubName,
                LectureLectureId = subLecture.LectureLectureId,
                LectureAdminOutput = subLecture.LectureAdminOutput,
                CurrentRating = subLecture.CurrentRating


            };
            //if (ModelState.IsValid)
            //{
            db.SubLectures.Add(sublect);
            db.SaveChanges();
            return RedirectToAction("Index");
            //}

            ViewBag.LectureLectureId = new SelectList(db.Lectures, "LectureId", "LectureName", subLecture.LectureLectureId);
            return View(sublect);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubLecture sublecture = db.SubLectures.Find(id);
            if (sublecture == null)
            {
                return HttpNotFound();
            }
            ViewBag.LectureLectureId = new SelectList(db.Lectures, "LectureId", "LectureName", sublecture.LectureLectureId);
            return View(sublecture);

        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubLecture subLecture)
        {
            var sublect = new SubLecture
            {
                ArtistId = User.Identity.GetUserId(),
                SubName = subLecture.SubName,
                LectureLectureId = subLecture.LectureLectureId,
                LectureAdminOutput = subLecture.LectureAdminOutput,
                CurrentRating = subLecture.CurrentRating,
                SubId = subLecture.SubId



            };
            //if (ModelState.IsValid)
            //{
                db.Entry(sublect).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            //}

            //ViewBag.LectureLectureId = new SelectList(db.Lectures, "LectureId", "LectureName", subLecture.LectureLectureId);
            return View(subLecture);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubLecture subLecture = db.SubLectures.Find(id);
            if (subLecture == null)
            {
                return HttpNotFound();
            }
            return View(subLecture);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            SubLecture sublect = db.SubLectures.Find(id);
            db.SubLectures.Remove(sublect);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}