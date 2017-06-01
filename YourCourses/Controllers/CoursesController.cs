using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;
using YourCourses.ViewModels;

namespace YourCourses.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            var viewModel = new CourseFormViewModel
            {
                CourseTypes = db.CourseTypes.ToList()
            };
            return View(viewModel);
        }

        // POST: Courses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        // [Bind(Include = "CourseId,CourseName,CourseInfo,Date,Time,CourseType")] CourseFormViewModel viewModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CourseFormViewModel viewModel)
        {
            var artistId = User.Identity.GetUserId();
            var artist = db.Users.Single(u=>u.Id==artistId);
            var courseType = db.CourseTypes.Single(t=>t.Id==viewModel.Type);
            var course = new Course
            {
                Artist = artist,
                CourseName = viewModel.CourseName,
                DateOfCourseCreation = DateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time)),
                CourseType = courseType,
                CourseInfo = viewModel.CourseInfo
            };
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseFormViewModel viewModel)
        {
            var artistId = User.Identity.GetUserId();
            var artist = db.Users.Single(u => u.Id == artistId);
            var courseType = db.CourseTypes.Single(t => t.Id == viewModel.Type);
            var courseId = db.CourseTypes.Single(t => t.Id == viewModel.CourseId);
            var course = new Course
            {
                
                Artist = artist,
                CourseName = viewModel.CourseName,
                DateOfCourseCreation = viewModel.DateTime,
                CourseType = courseType,
                CourseInfo = viewModel.CourseInfo
            };

            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
