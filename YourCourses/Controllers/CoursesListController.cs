using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;
using YourCourses.ViewModels;
using System.Data.Entity;

using System.Linq;
using System.Net;
using Microsoft.AspNet.Identity;

namespace YourCourses.Controllers
{
    public class CoursesListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CoursesList
        public ActionResult Index()
        {
            var upcoming = db.Courses
                .Include(c => c.Artist)
                .Include(c => c.CourseType);
            var viewCurses = db.Courses.ToList();
            return View(upcoming);
        }

        public ActionResult Lecture(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.id = id.Value;
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Current course id</param>
        /// <returns></returns>
        public ActionResult CreateLectureView(int? id) // current course id
        {
            if (id.HasValue)
            {
                ViewBag.courseId = id.Value ;
            }
            return View();
        }

        public ActionResult CreateLecture(LecturesFormViewModel viewModel)
        {

            var lecture = new Lecture
            {
                LectureName = viewModel.Name,
                
            };

            if (ModelState.IsValid)
            {
                db.Lectures.Add(lecture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }
    }
}