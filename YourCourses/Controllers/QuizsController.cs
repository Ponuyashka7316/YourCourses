using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;

namespace YourCourses.Controllers
{
    public class QuizsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Quizs
        public ActionResult Index()
        {
            var quizs = db.Quizs.ToList();
            return View(quizs);
            
        }

        public ActionResult Create()
        {
           //ViewBag.SublectureSubId = new SelectList(db.SubLectures, "SubId", "SubName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quiz quiz)
        {
            var quizmodel = new Quiz
            {
                Name = quiz.Name,
                ArtistId = User.Identity.GetUserId(),
                Info=quiz.Info
            };
            
                db.Quizs.Add(quizmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quizs.Find(id);
            db.Quizs.Remove(quiz);
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