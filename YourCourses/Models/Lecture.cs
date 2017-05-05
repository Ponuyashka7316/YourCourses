using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class Lecture
    {
        [Required]
        [StringLength(1000)]
        public string LectureName { get; set; } //lecture name

        [Key]
        public int LectureId { get; set; }

        public List<SubLecture> SubLectures { get; set; } //Every Lecture contain many Subs
    }
}