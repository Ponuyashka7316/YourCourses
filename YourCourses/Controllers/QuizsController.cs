//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using YourCourses.Models;

//namespace YourCourses.Controllers
//{
//    public class QuizsController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: Quizs
//        public ActionResult Index()
//        {
//            return View(db.Quizs.ToList());
//        }

//        // GET: Quizs/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Quiz quiz = db.Quizs.Find(id);
//            if (quiz == null)
//            {
//                return HttpNotFound();
//            }
//            return View(quiz);
//        }

//        // GET: Quizs/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Quizs/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "QuizId,StartTime,Duration,EndTime,Score")] Quiz quiz)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Quizs.Add(quiz);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(quiz);
//        }

//        // GET: Quizs/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Quiz quiz = db.Quizs.Find(id);
//            if (quiz == null)
//            {
//                return HttpNotFound();
//            }
//            return View(quiz);
//        }

//        // POST: Quizs/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "QuizId,StartTime,Duration,EndTime,Score")] Quiz quiz)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(quiz).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(quiz);
//        }

//        // GET: Quizs/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Quiz quiz = db.Quizs.Find(id);
//            if (quiz == null)
//            {
//                return HttpNotFound();
//            }
//            return View(quiz);
//        }

//        // POST: Quizs/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Quiz quiz = db.Quizs.Find(id);
//            db.Quizs.Remove(quiz);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
