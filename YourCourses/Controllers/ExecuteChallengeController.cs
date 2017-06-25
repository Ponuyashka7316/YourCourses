using Microsoft.AspNet.Identity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using YourCourses.Models;

namespace YourCourses.Controllers
{
    public class ExecuteChallengeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public const string ApiUrl = "https://dotnetfiddle.net/api/fiddles/";
        public static bool correct = false;
        // GET: ExecuteChallenge

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var l = new List<int>();
            Session["CurrentUserId"] = User.Identity.GetUserId();
            if ((bool)Session["correct"])
            {
                Session["countSuccess"] = (int)Session["countSuccess"] + 1;
                if ((int)Session["countSuccess"]==1) {
                    Session["countSuccess"] = 0;
                    //тут будет выполняться переход на след ур-нь
                    Session["startmark"] = ((int)Session["startmark"]) + 1;
                }
                
                Session["correct"] = false;
                var strtmark = (int)Session["startmark"];
                var curPr = db.Practices
                    .Where(c => c.Mark == strtmark);
                if (curPr.Count() == 0)
                {
                    
                    return View("ShowResults");
                }
                int count = curPr.Count();

                int random = new Random().Next(0, count);
                foreach (var item in l)
                {
                    if (random==item) random = new Random().Next(0, count);
                }
                l.Add(random);
                var PR = curPr.AsEnumerable().ElementAt(random);   //получаем случайную практику из базы для текущей сложности
                if (PR.PracticeUserInput != null) { Session["UI"] = PR.PracticeUserInput.ToString(); }
                if (PR.FirstPart != null) { Session["FP"] = PR.FirstPart.ToString(); }
                if (PR.TestsPart != null) { Session["TEST"] = PR.TestsPart.ToString(); }
                if (PR.SecondPart != null) { Session["LP"] = PR.SecondPart.ToString(); }
                if (PR.PracticeDescription != null) { Session["TEXT"] = PR.PracticeDescription.ToString(); }  //записываем все в переменные сессии


                return RedirectToAction("Index", "ExecuteChallenge");
            }
            else
            {
               
                Session["countFails"] = (int)Session["countFails"] - 1;

                string text = (string)Session["UI"];

                var model = new FiddleExecuteModel()
                {
                    CodeBlock = text
                };

                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult Execute(string code)
        {
            Session["correct"] = correct;
            var result = ExecuteFiddle(GetConsoleSample((string)Session["FP"] + (string)Session["TEST"] + code + (string)Session["LP"]));
            Session["correct"] = correct;
            return Json(result);
        }

        [Authorize]
        public ActionResult ReExecute()
        {
            if ((bool)Session["correct"])
            {
                Session["startmark"] = ((int)Session["startmark"])+1;
                Session["correct"] = false;
                var strtmark = (int)Session["startmark"];
                var curPr = db.Practices
                    .Where(c => c.Mark == strtmark);
                int count = curPr.Count();  

                int random = new Random().Next(0, count);
                var PR = curPr.AsEnumerable().ElementAt(random);   //получаем случайную практику из базы для текущей сложности
                if(PR.PracticeUserInput != null) { Session["UI"] = PR.PracticeUserInput.ToString(); }
                if(PR.FirstPart != null) { Session["FP"] = PR.FirstPart.ToString(); }
                if(PR.TestsPart != null) { Session["TEST"] = PR.TestsPart.ToString(); }
                if(PR.SecondPart != null) { Session["LP"] = PR.SecondPart.ToString(); }
                if(PR.PracticeDescription != null) { Session["TEXT"] = PR.PracticeDescription.ToString(); }  //записываем все в переменные сессии


                return RedirectToAction("Index", "ExecuteChallenge"); 
            }
            return View();
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