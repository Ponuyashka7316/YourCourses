using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourCourses.Models;

namespace YourCourses.ViewModels
{
    public class CourseFormViewModel
    {
        public string CourseName { get; set; }
        public string CourseInfo { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Type { get; set; }
        public int CourseId { get; set; }
        public DateTime DateTime
        {
            get { return DateTime.Parse(string.Format("{0} {1}", Date, Time)); }
        }
        public IEnumerable<CourseType> CourseTypes { get; set; }
    }
}