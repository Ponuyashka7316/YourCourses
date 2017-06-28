using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;
using System.Data.Entity;
using PagedList;

namespace YourCourses.Controllers
{
    public class PracticeAndUserMarksController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: PracticeAndUserMarks
        [Authorize(Roles = "admin")]
        public ActionResult Index(string q, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var up = db.PracticeAndUserMarks
                .Include(c=>c.Artist)
                .Include(c=>c.Practice)
                .ToList();

            if (!String.IsNullOrEmpty(q))
            {
                ViewBag.SStr = q;
                up = up
                       .Where(p => p.Artist.UserName.Contains(q)).ToList();

            }
            return View(up.OrderBy(x => x.Id).ToPagedList(pageNumber, pageSize));
        }
    }
}