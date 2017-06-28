using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using YourCourses.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using PagedList.Mvc;
using PagedList;

namespace YourCourses.Controllers
{
    public class SubLecturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: SubLectures
        public ActionResult Index(string q, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var subLectures = db.SubLectures.Include(p => p.Lecture);
            if (!String.IsNullOrEmpty(q))
            {
                ViewBag.SStr = q;
                subLectures = subLectures
                       .Where(p => p.SubName.Contains(q));

            }


            return View(subLectures.OrderBy(x => x.SubId).ToPagedList(pageNumber, pageSize));
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