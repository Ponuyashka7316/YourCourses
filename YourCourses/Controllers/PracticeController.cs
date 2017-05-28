using Microsoft.CodeDom.Providers.DotNetCompilerPlatform;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
            string code = @"// A Hello World! program in C#
using System;
namespace HelloWorld
    {
        class Hello
        {
            static void Main()
            {
                string s = ""This is YourCourses!"";

                
            }
        }
    }";
            ViewBag.sampleCode = code;
            return View();
        }

        [HttpPost]
        public ActionResult Out(string code)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            // Reference to System.Drawing library
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = true;
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = false;
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                ViewBag.result = sb;
               // throw new InvalidOperationException(sb.ToString());
            }
            else
            {
                ViewBag.result = "21313";
            }
            //ViewBag.result =results.Output.ToString();

            return PartialView();

        }
    }
}