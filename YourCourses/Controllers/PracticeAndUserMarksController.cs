using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;

namespace YourCourses.Controllers
{
    public class PracticeAndUserMarksController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: PracticeAndUserMarks
        public ActionResult Index()
        {
            var up = db.PracticeAndUserMarks.ToList();
            return View(up);
        }
    }
}