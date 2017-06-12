﻿using System;
using System.Web.Mvc;

using YourCourses.Models;
using System.Text;
using System.Net;

using RestSharp;

namespace YourCourses.Controllers
{
    public class ExecuteController : Controller
    {
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
}";

        public const string ApiUrl = "https://dotnetfiddle.net/api/fiddles/";

        [HttpGet]
        public ActionResult Index()
        {
            var model = new FiddleExecuteModel()
            {
                CodeBlock = StartingCodeBlock
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
    }
}