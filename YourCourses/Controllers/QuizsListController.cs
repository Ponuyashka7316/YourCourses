using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;
using System.Data.Entity;

namespace YourCourses.Controllers
{
    public class QuizsListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuizsList
        public ActionResult Index()
        {
            var upcoming = db.Quizs.Include(c => c.Artist);
            
            return View(upcoming);
        }

        public ActionResult Questions(int? id)
        {
            ViewBag.rate = 0;
            var upcoming = db.Questions
                    .Where(c => c.QuizId == id);
            if (id.HasValue)
            {
                ViewBag.id = id.Value;
            }
            return View(upcoming);
        }

        [HttpGet]
        public ActionResult QuestionsList(int? id)
        {
            if (id.HasValue)
            {
                var quest = db.Options
                   .Where(c => c.QuestionId == id);
                return View(quest);

            }
            else
            {
                var upcoming = db.Options.ToList();
                return View(upcoming);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult QuestionsList(Option options)
        {

            return View();
        }
    }
}