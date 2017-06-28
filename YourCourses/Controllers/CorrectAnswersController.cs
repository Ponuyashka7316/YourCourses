using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;
using PagedList.Mvc;
using PagedList;

namespace YourCourses.Controllers
{
    public class CorrectAnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CorrectAnswers
        public ActionResult Index(string q, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            
            var correctAnswers = db.CorrectAnswers.Include(p => p.Practices);
            if (!String.IsNullOrEmpty(q))
            {
                ViewBag.SStr = q;
                correctAnswers = correctAnswers
                       .Where(p => p.Practices.PracticeName.Contains(q));

            }
           
            return View(correctAnswers.OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize));
        }

        // GET: CorrectAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorrectAnswer correctAnswer = db.CorrectAnswers.Find(id);
            if (correctAnswer == null)
            {
                return HttpNotFound();
            }
            return View(correctAnswer);
        }

        // GET: CorrectAnswers/Create
        public ActionResult Create()
        {
            ViewBag.PracticePracticeId = new SelectList(db.Practices, "PracticeId", "PracticeName");
            return View();
        }

        // POST: CorrectAnswers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Answer,PracticePracticeId")] CorrectAnswer correctAnswer)
        {
            if (ModelState.IsValid)
            {
                db.CorrectAnswers.Add(correctAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PracticePracticeId = new SelectList(db.Practices, "PracticeId", "PracticeName", correctAnswer.PracticePracticeId);
            return View(correctAnswer);
        }

        // GET: CorrectAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorrectAnswer correctAnswer = db.CorrectAnswers.Find(id);
            if (correctAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.PracticePracticeId = new SelectList(db.Practices, "PracticeId", "PracticeName", correctAnswer.PracticePracticeId);
            return View(correctAnswer);
        }

        // POST: CorrectAnswers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Answer,PracticePracticeId")] CorrectAnswer correctAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(correctAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PracticePracticeId = new SelectList(db.Practices, "PracticeId", "PracticeName", correctAnswer.PracticePracticeId);
            return View(correctAnswer);
        }

        // GET: CorrectAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorrectAnswer correctAnswer = db.CorrectAnswers.Find(id);
            if (correctAnswer == null)
            {
                return HttpNotFound();
            }
            return View(correctAnswer);
        }

        // POST: CorrectAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CorrectAnswer correctAnswer = db.CorrectAnswers.Find(id);
            db.CorrectAnswers.Remove(correctAnswer);
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
