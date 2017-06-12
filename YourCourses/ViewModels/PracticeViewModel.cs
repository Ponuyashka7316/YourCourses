using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourCourses.Models;

namespace YourCourses.ViewModels
{
    public class PracticeViewModel
    {
        public Language Language { get; set; }
        public ProjectType ProjectType { get; set; }
        public Compiler Compiler { get; set; }
        public string CodeBlock { get; set; }
        public string[] ConsoleInputLines { get; set; }
        public MvcCodeBlock MvcCodeBlock { get; set; }
        public NuGetPackages[] NuGetPackages { get; set; }

        public int PracticeId { get; set; }    
        public string PracticeName { get; set; } //Name of practice exercise
        public string PracticeUserInput { get; set; } //user input text from textArea, Sets the default value for textArea        
        public string FirstPart { get; set; }   //first part
        public string TestsPart { get; set; }   //not visible for user
        public string SecondPart { get; set; }    //second part after USER INPUT ("PracticeUserInput" prop)
        public double Mark { get; set; }    //middle users votes for the practice     
        public int SublectureSubId { get; set; }
        public virtual List<CorrectAnswer> CorrectAnswer { get; set; } //contain true result/results for this practice        
        public virtual SubLecture Sublectures { get; set; }
    }
}