using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class Lecture
    {
        [Key]
        public int LectureId { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Название лекции : ")]
        public string LectureName { get; set; } //lecture name

        //[Required]
        //public int? SubLectureSubId { get; set; }

        public virtual List<SubLecture> SubLectures { get; set; } //Every Lecture contain many Subs

        [Required]
        [Display(Name = "Курс : ")]
        public int CourseCourseId { get; set; }

        [ForeignKey("CourseCourseId")]
        [Display(Name = "Курс : ")]
        public virtual Course Course { get; set; }
    }
}