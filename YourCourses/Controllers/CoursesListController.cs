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
            var upcoming = db.Lectures
                    .Where(c => c.CourseCourseId == id);
            if (id.HasValue)
            {
                ViewBag.id = id.Value;
            }
            return View(upcoming);
        }

        public ActionResult SublecturesList(int? id)
        {
            if (id.HasValue)
            {
                var upcoming = db.SubLectures
                    .Where(c => c.LectureLectureId == id);
                return View(upcoming);
            }
            else
            {
                var upcoming = db.SubLectures;
                return View(upcoming);
            }
            //var viewCurses = db.Courses.ToList();

        }

        public ActionResult ShowLecture(int? id)
        {
            if (id.HasValue)
            {
                SubLecture subLecture = db.SubLectures.Find(id);
                if (subLecture == null)
                {
                    return HttpNotFound();
                }
                return View(subLecture);
                
            }
            else
            {
                var upcoming = db.SubLectures;
                return View(upcoming);
            }
            
        }

        /// <summary>
        /// create lecture for current course
        /// </summary>
        /// <param name="id">Current course id</param>
        /// <returns></returns>
        //public ActionResult CreateLectureView(int? id) // current course id
        //{
        //    if (id.HasValue)
        //    {
        //        ViewBag.courseId = id.Value ;
        //    }
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult CreateLecture(LecturesFormViewModel viewModel)
        //{
        //   // Course course = db.Courses.First(p => p.CourseId == ViewBag.courseId);
        //    Lecture lecture = new Lecture
        //    {
        //        LectureName = viewModel.Name,
        //        //CourseCourseId = viewModel.Course_CourseId,


        //    };

        //    if (ModelState.IsValid)
        //    {
        //        //course.Lectures = new List<Lecture> { lecture };
        //        //db.Courses.Add(course);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(lecture);
        //}
    }
}