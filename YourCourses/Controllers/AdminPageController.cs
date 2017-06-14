using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Authorize(Roles = "admin")]
        public ActionResult InfoAbout()
        {
            
            return View();
        }

        //Create CourseInfo
        [Authorize(Roles = "admin")]
        public ActionResult CourseInfo()
        {
            CourseTypes = _context.CourseTypes.ToList();
            return View(CourseTypes);
        }

        
    }
}