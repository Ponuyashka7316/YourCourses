using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;

namespace YourCourses.Controllers
{
    public class PracticesHomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PracticesHome
        public ActionResult Index()
        {
            var practices = db.Practices.Include(p => p.Sublectures);
            return View(practices.ToList());
        }

        // GET: PracticesHome/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practice practice = db.Practices.Find(id);
            if (practice == null)
            {
                return HttpNotFound();
            }
            return View(practice);
        }

        // GET: PracticesHome/Create
        public ActionResult Create()
        {
            ViewBag.SublectureSubId = new SelectList(db.SubLectures, "SubId", "SubName");
            return View();
        }

        // POST: PracticesHome/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PracticeId,PracticeName,PracticeUserInput,FirstPart,TestsPart,SecondPart,SublectureSubId")] Practice practice)
        {
            if (ModelState.IsValid)
            {
                db.Practices.Add(practice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SublectureSubId = new SelectList(db.SubLectures, "SubId", "SubName", practice.SublectureSubId);
            return View(practice);
        }

        // GET: PracticesHome/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practice practice = db.Practices.Find(id);
            if (practice == null)
            {
                return HttpNotFound();
            }
            ViewBag.SublectureSubId = new SelectList(db.SubLectures, "SubId", "SubName", practice.SublectureSubId);
            return View(practice);
        }

        // POST: PracticesHome/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PracticeId,PracticeName,PracticeUserInput,FirstPart,TestsPart,SecondPart,SublectureSubId")] Practice practice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(practice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SublectureSubId = new SelectList(db.SubLectures, "SubId", "SubName", practice.SublectureSubId);
            return View(practice);
        }

        // GET: PracticesHome/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practice practice = db.Practices.Find(id);
            if (practice == null)
            {
                return HttpNotFound();
            }
            return View(practice);
        }

        // POST: PracticesHome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Practice practice = db.Practices.Find(id);
            db.Practices.Remove(practice);
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
