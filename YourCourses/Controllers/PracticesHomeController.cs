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
    public class PracticesHomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public ActionResult Search(string q)
        //{
        //    if (q != null)
        //    {
        //        var practices = db.Practices.Include(p => p.Sublectures)
        //            .Where(p => p.PracticeName.Contains(q));
        //    }
        //        return View(practices.ToList(), "Index");
        //}

        // GET: PracticesHome
        public ActionResult Index(string q, int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var practices = db.Practices.Include(p => p.Sublectures);
            if (!String.IsNullOrEmpty(q))
            {
                ViewBag.SStr = q;
                practices = practices
                       .Where(p => p.PracticeName.Contains(q));
                
            }
               
                
            return View(practices.OrderBy(x=>x.PracticeId).ToPagedList(pageNumber, pageSize));
        }
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PracticeId,PracticeName,PracticeDescription,PracticeUserInput,FirstPart,TestsPart,SecondPart,Mark,SublectureSubId")] Practice practice)
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        // POST: PracticesHome/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PracticeId,PracticeName,PracticeDescription,PracticeUserInput,FirstPart,TestsPart,SecondPart,Mark,SublectureSubId")] Practice practice)
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
