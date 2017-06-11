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
using Microsoft.CSharp;
using System.Reflection;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Security.Permissions;

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

        public string CompileAndRuns(string Code)
        {

            return Code;
        }

        public ActionResult ShowEditorArea(string Code)
        {
            ViewBag.result = CompileAndRun(Code);//EvalCode("Program", "Main",Code);// CompileAndRun(Code);
            return View();
        }

        private string EvalCode(string typeName, string methodName, string sourceCode)
        {
            string output = "Output ";
            
            var compiler = CodeDomProvider.CreateProvider("CSharp");
            var parameters = new CompilerParameters
            {
                CompilerOptions = "/t:library",
                GenerateInMemory = true,
                IncludeDebugInformation = true
            };
            var results = compiler.CompileAssemblyFromSource(parameters, sourceCode);

            if (!results.Errors.HasErrors)
            {
                var assembly = results.CompiledAssembly;
                var evaluatorType = assembly.GetType(typeName);
                var evaluator = Activator.CreateInstance(evaluatorType);

                output += (string)InvokeMethod(evaluatorType, methodName, evaluator, new object[] { output });
                return output;
            }

            output+= "\r\nHouston, we have a problem at compile time!";
            return results.Errors.Cast<CompilerError>().Aggregate(output, (current, ce) => current + string.Format("\r\nline {0}: {1}", ce.Line, ce.ErrorText));
        }

        [FileIOPermission(SecurityAction.Deny, Unrestricted = true)]
        private object InvokeMethod(Type evaluatorType, string methodName, object evaluator, object[] methodParams)
        {
            return evaluatorType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, evaluator, methodParams);
        }

        static string CompileAndRun(string code)
        {
            string result;
            Dictionary<string, string> providerOptions = new Dictionary<string, string>
                {
                    {"CompilerVersion", "v3.5"}
                };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false
            };

            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, code);

            if (results.Errors.Count != 0)
            {
                result = "Error";
                return result;
            }

            object o = results.CompiledAssembly.CreateInstance("Program");
            MethodInfo mi = o.GetType().GetMethod("Main");
            result=(string)mi.Invoke(o, null);

            return result;
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
