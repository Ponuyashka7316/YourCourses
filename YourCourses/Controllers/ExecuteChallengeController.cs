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
        public const string ApiUrl = "https://dotnetfiddle.net/api/fiddles/";
        public static bool correct = false;
        // GET: ExecuteChallenge

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {

            Session["CurrentUserId"] = User.Identity.GetUserId();

            string text = (string)Session["UI"];
            
            var model = new FiddleExecuteModel()
            {
                CodeBlock = text
            };

            return View(model);
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
            if (regex.IsMatch(resultString) || regex1.IsMatch(resultString))
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