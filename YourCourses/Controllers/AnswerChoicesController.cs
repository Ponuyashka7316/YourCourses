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
//    public class AnswerChoicesController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: AnswerChoices
//        public ActionResult Index()
//        {
//            var answerChoices = db.AnswerChoices.Include(a => a.Question);
//            return View(answerChoices.ToList());
//        }

//        // GET: AnswerChoices/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            AnswerChoice answerChoice = db.AnswerChoices.Find(id);
//            if (answerChoice == null)
//            {
//                return HttpNotFound();
//            }
//            return View(answerChoice);
//        }

//        // GET: AnswerChoices/Create
//        public ActionResult Create()
//        {
//            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText");
//            return View();
//        }

//        // POST: AnswerChoices/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "AnswerChoiceId,Choices,QuestionId")] AnswerChoice answerChoice)
//        {
//            if (ModelState.IsValid)
//            {
//                db.AnswerChoices.Add(answerChoice);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText", answerChoice.QuestionId);
//            return View(answerChoice);
//        }

//        // GET: AnswerChoices/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            AnswerChoice answerChoice = db.AnswerChoices.Find(id);
//            if (answerChoice == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText", answerChoice.QuestionId);
//            return View(answerChoice);
//        }

//        // POST: AnswerChoices/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "AnswerChoiceId,Choices,QuestionId")] AnswerChoice answerChoice)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(answerChoice).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText", answerChoice.QuestionId);
//            return View(answerChoice);
//        }

//        // GET: AnswerChoices/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            AnswerChoice answerChoice = db.AnswerChoices.Find(id);
//            if (answerChoice == null)
//            {
//                return HttpNotFound();
//            }
//            return View(answerChoice);
//        }

//        // POST: AnswerChoices/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            AnswerChoice answerChoice = db.AnswerChoices.Find(id);
//            db.AnswerChoices.Remove(answerChoice);
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
