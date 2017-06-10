using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;



namespace YourCourses.Controllers
{
    public class PracticeController : Controller
    {
        // GET: Practice
        [HttpGet]
        public ActionResult Index()
        {
            string codeFromDB1 = @"// A Hello World! program in C#
using System;


namespace HelloWorld
    {
        class Hello
        {
            static void Main()
            {
                string s = ""aaa"";
                
                Console.WriteLine(""s"");
                if (Run(""aab"")!=s)
                throw new ArgumentException();
                
            }
           
        
       
        ";
            string userOutput = @"            static public string Run(string str)
            {
                return str;
            }";
            string userInput = String.Empty;
            string codeFromDB2 = @"        }
       
    }";
            ViewBag.sampleCode = codeFromDB1+userOutput+codeFromDB2;
            return View();
        }

        [HttpPost]
        public ActionResult Out(string sampleCode)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            // Reference to System.Drawing library
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = true;
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = false;
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, sampleCode);


            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber,  error.ToString()));
                }

                ViewBag.result = sb.ToString() +" Затрачено времени на запрос: "+ ts.Milliseconds + "мс";
               // throw new InvalidOperationException(sb.ToString());
            }
            else
            {
                ViewBag.result = results.NativeCompilerReturnValue + " Затрачено времени на запрос: " + ts.Milliseconds + "мс";
            }
            //ViewBag.result =results.Output.ToString();

            return PartialView();

        }
    }
}