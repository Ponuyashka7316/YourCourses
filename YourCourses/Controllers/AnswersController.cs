﻿//using System;
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
//    public class AnswersController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: Answers
//        public ActionResult Index()
//        {
//            return View(db.Answers.ToList());
//        }

//        // GET: Answers/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Answer answer = db.Answers.Find(id);
//            if (answer == null)
//            {
//                return HttpNotFound();
//            }
//            return View(answer);
//        }

//        // GET: Answers/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Answers/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "AnswerId,AnswerText")] Answer answer)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Answers.Add(answer);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(answer);
//        }

//        // GET: Answers/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Answer answer = db.Answers.Find(id);
//            if (answer == null)
//            {
//                return HttpNotFound();
//            }
//            return View(answer);
//        }

//        // POST: Answers/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "AnswerId,AnswerText")] Answer answer)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(answer).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(answer);
//        }

//        // GET: Answers/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Answer answer = db.Answers.Find(id);
//            if (answer == null)
//            {
//                return HttpNotFound();
//            }
//            return View(answer);
//        }

//        // POST: Answers/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Answer answer = db.Answers.Find(id);
//            db.Answers.Remove(answer);
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
