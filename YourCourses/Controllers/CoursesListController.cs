using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;
using YourCourses.ViewModels;
using System.Data.Entity;
using System.Text;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CSharp;
using System.Reflection;
using System.Net;
using RestSharp;
using Microsoft.AspNet.Identity;
using System.Security.Permissions;

namespace YourCourses.Controllers
{
    public class CoursesListController : Controller
    {
        public void CoursesController() {

        }
        public const string ApiUrl = "https://dotnetfiddle.net/api/fiddles/";
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

        public ActionResult ShowPractice(int? id)
        {
            if (id.HasValue)
            {
                var practice = db.Practices
                    .Where(c => c.SublectureSubId == id);


                return View(practice);

            }

            else
            {
                var practice = db.Practices;
                return View(practice);
            }

        }
        
        public ActionResult Show()
        {

            var model = new FiddleExecuteModel()
            {
                CodeBlock = ViewBag.text
            };

            return View(model);
        }

        public ActionResult ShowEditorArea(string Code)
        {
            var model = new FiddleExecuteModel()
            {
                CodeBlock = Code
                //ProjectType= ProjectType.Console
            };

            return View(model);
           
        }

       
        [HttpPost]
        public JsonResult Execute(string code)
        {

            var result = ExecuteFiddle(GetConsoleSample(code));
            return Json(result);
        }

        private static string ExecuteFiddle(FiddleExecuteModel model)
        {
            var client = new RestClient(ApiUrl);

            // execute request through API
            var request = new RestRequest("execute", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(model);

            IRestResponse<FiddleExecuteResult> response = client.Execute<FiddleExecuteResult>(request);

            StringBuilder result = new StringBuilder();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                result.AppendLine("Failed to execute API request. Here is an answer from API");
                result.AppendLine("Response Code: " + response.StatusCode);
                result.AppendLine("Response Body: " + response.Content);
            }
            else
            {
                // write usage statistics
                foreach (var header in response.Headers)
                {
                    if (header.Name == "X-RateLimit-Limit")
                    {
                        result.AppendLine("Your total per hour limit is " + header.Value);
                    }

                    if (header.Name == "X-RateLimit-Remaining")
                    {
                        result.AppendLine("Your remaining executions count per hour is " + header.Value);
                    }

                    if (header.Name == "X-RateLimit-Reset")
                    {
                        var epochTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        epochTime = epochTime.AddSeconds(int.Parse(header.Value.ToString()));
                        result.AppendLine("UTC Time when limit will be refreshed " + epochTime);
                    }
                }

                result.AppendLine();
                result.AppendLine("Code output:");
                result.AppendLine(response.Data.ConsoleOutput);
            }

            return result.Replace(Environment.NewLine, "<br/>").ToString();
        }

        // get code block
        private static FiddleExecuteModel GetConsoleSample(string code)
        {
            return new FiddleExecuteModel()
            {
                Compiler = Compiler.Net45,
                Language = Language.CSharp,
                ProjectType = ProjectType.Console,
                CodeBlock = code
            };
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
