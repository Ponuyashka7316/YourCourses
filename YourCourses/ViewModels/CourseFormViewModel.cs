using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YourCourses.Models;

namespace YourCourses.ViewModels
{
    public class CourseFormViewModel
    {
        [Display(Name = "Имя курса : ")]
        public string CourseName { get; set; }
        [Display(Name = "Информация о курсе : ")]
        public string CourseInfo { get; set; }
        [Display(Name = "Дата создания : ")]
        public string Date { get; set; }
        [Display(Name = "Время создания : ")]
        public string Time { get; set; }
        public int Type { get; set; }
        [Display(Name = "Тип курса : ")]
        public int CourseId { get; set; }
        public DateTime DateTime
        {
            get { return DateTime.Parse(string.Format("{0} {1}", Date, Time)); }
        }
        public IEnumerable<CourseType> CourseTypes { get; set; }
    }
}