using System;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using YourCourses.Models;
using System.Text;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using RestSharp;
using Microsoft.AspNet.Identity;

namespace YourCourses.Controllers
{
    public class ExecuteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public const string StartingCodeBlock = @"public class Program
{
	public static void Main()
	{
		for (uint i = 1; i <= 100; i++)
		{
			string s = null;

			if (i % 3 == 0)
				s = ""Fizz"";

			if (i % 5 == 0)
				s += ""Buzz"";

			System.Console.WriteLine(s ?? i.ToString());
		}
	}
";

        public const string ApiUrl = "https://dotnetfiddle.net/api/fiddles/";
        public static bool correct = false;
        [Authorize]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            var user = User.Identity.GetUserId();
            var upcoming = db.PracticeAndUserMarks

            .Include(c => c.Artist)
            .Include(c => c.Practice)
            .Where(c => c.ArtistId == user)
            .Where(c => c.PracticePracticeId == id);

            
            if (upcoming.Count() == 0)
            {
                var model1 = new PracticeAndUserMark
                {
                    ArtistId = User.Identity.GetUserId(),
                    Mark = 0,
                    PracticePracticeId = id.Value


                };
                if (ModelState.IsValid)
                {
                    db.PracticeAndUserMarks.Add(model1);
                    db.SaveChanges();

                }
            }
            var Iddd = upcoming.Single(c => c.PracticePracticeId == id.Value);
            Session["Id"] = Iddd.Id;
            var up = upcoming.Single();

            Session["mark"] = up.Mark.ToString();
            Session["res"] = false;
            Session["CurrentUserId"] = User.Identity.GetUserId();
            // db.PracticeAndUserMarks.Find(Session["CurrentUserId"], Session["CurrentPractId"]);

            string text = (string)Session["UI"];
            var model = new FiddleExecuteModel()
            {
                CodeBlock = text
            };

            return View(model);
        }

        public ActionResult UpdateMark(int? id)
        {
            if (id.HasValue)
            {
                var user = User.Identity.GetUserId();
                var upcoming = db.PracticeAndUserMarks

                .Include(c => c.Artist)
                .Include(c => c.Practice)
                .Where(c => c.ArtistId == user)
                .Where(c => c.PracticePracticeId == id);
                //var Iddd= upcoming.Single(c => c.PracticePracticeId == id.Value);
                //Session["Id"] = Iddd.Id;
                if (upcoming.Count() == 0)
                {
                    var model = new PracticeAndUserMark
                    {
                        ArtistId = User.Identity.GetUserId(),
                        Mark = 0,
                        PracticePracticeId = id.Value


                    };
                    if (ModelState.IsValid)
                    {
                        db.PracticeAndUserMarks.Add(model);
                        db.SaveChanges();

                    }
                }
                else
                    return PartialView(upcoming);
            }
            return PartialView();
        }

        [Authorize]
        [HttpPost]
        public JsonResult Execute(string code)
        {
            Session["correct"] = false;
            var result = ExecuteFiddle(GetConsoleSample((string)Session["FP"] + (string)Session["TEST"] + code + (string)Session["LP"]));
            Session["correct"] = correct;
            return Json(result);
        }

        public ActionResult Close(int? id)
        {
            if ((bool)Session["correct"])
            {
                if (id.HasValue)
                {

                    var user = User.Identity.GetUserId();
                    //var upcoming = db.PracticeAndUserMarks

                    //.Include(c => c.Artist)
                    //.Include(c => c.Practice)
                    //.Where(c => c.ArtistId == user)
                    //.Where(c => c.PracticePracticeId == id);

                    //var up = upcoming.Single(c => c.PracticePracticeId == id.Value);


                    //var up_id = up.Id;
                    //int idd = (int)Session["Id"];
                    int d = (int)Session["Id"];
                    var model = new PracticeAndUserMark
                    {
                        ArtistId = user,
                        Mark = 5,
                        PracticePracticeId = id.Value,
                        Id= d

                    };
                    Session["mark"] = model.Mark;
                    if (ModelState.IsValid)
                    {
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
            }
            return PartialView();
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
                    //if (header.Name == "X-RateLimit-Limit")
                    //{
                    //    result.AppendLine("Your total per hour limit is " + header.Value);
                    //}

                    //if (header.Name == "X-RateLimit-Remaining")
                    //{
                    //    result.AppendLine("Your remaining executions count per hour is " + header.Value);
                    //}

                    if (header.Name == "X-RateLimit-Reset")
                    {
                        var epochTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        epochTime = epochTime.AddSeconds(int.Parse(header.Value.ToString()));
                        //result.AppendLine("UTC Time when limit will be refreshed " + epochTime);
                    }
                }

                //result.AppendLine();
                //result.AppendLine("Code output:");
                result.AppendLine(response.Data.ConsoleOutput);
            }
            var resultString = result.Replace(Environment.NewLine, "<br/>").ToString();
            Regex regex = new Regex("error");
            Regex regex1 = new Regex("exception");
            if (regex.IsMatch(resultString.ToLower()) || regex1.IsMatch(resultString.ToLower()))
            {
                correct = false;
                return "Не верно " + resultString;
            }


            correct = true;
            return "ЗАДАНИЕ ВЫПОЛНЕНО " + resultString;
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
    }
}