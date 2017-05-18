using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;

namespace YourCourses.Controllers
{
    //[Authorize]
    public class AdminPageController : Controller
    {
        private ApplicationDbContext _context;
        public IEnumerable<CourseType> CourseTypes;

        public AdminPageController()
        {
            _context= new ApplicationDbContext();
        }
        // GET: About
        //  Start Admin Page
        public ActionResult InfoAbout()
        {
            
            return View();
        }

        //Create CourseInfo
        public ActionResult CourseInfo()
        {
            CourseTypes = _context.CourseTypes.ToList();
            return View(CourseTypes);
        }
    }
}