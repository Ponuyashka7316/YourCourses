using System;
using System.Linq;
using System.Web.Mvc;
using YourCourses.Models;
using System.Data.Entity;
using System.Collections.Generic;


namespace YourCourses.Controllers
{
    //public static class StringExtension
    //{
    //     public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
    //    {
    //        int index = rand.Next(0, enumerable.Count());
    //        return enumerable.ElementAt(index);
    //    }
    //}

    public class ChallengeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Random rnd = new Random();
        public ChallengeController() {
            var countSuccess = 0;
            Session["startmark"] = 0;
           
        }
        // GET: Challenge
        [Authorize]
        public ActionResult Index()
        {
            
            var curPr = db.Practices
                .Include(c => c.Mark== (int)Session["startmark"]);
            
            var PR_number = rnd.Next(curPr.Count());
            var PR = curPr.ElementAt(PR_number);

            return View(PR);
        }
        //public static T RandomElement<T>(this IEnumerable<T> enumerable)
        //{
        //    return enumerable.RandomElementUsing<T>(new Random());
        //}

       

        // Usage:
        //var ints = new int[] { 1, 2, 3 };
        //int randomInt = ints.RandomElement();

        // If you have a preexisting `Random` instance, rand, use it:
        // this is important e.g. if you are in a loop, because otherwise you will create new
        // `Random` instances every time around, with nearly the same seed every time.
       




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