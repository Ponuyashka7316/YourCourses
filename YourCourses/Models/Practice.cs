using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class Practice
    {
        //[Required]
        //public ApplicationUser Artist { get; set; } //Belonging to the user

        [Required]
        public int PracticeId { get; set; }

        [Required]
        [StringLength(1000)]
        public string PracticeName { get; set; } //Name of practice exercise

        public string PracticeUserInput { get; set; } //user input text from textArea, Sets the default value for textArea

        [Required]
        public string TestsForUserInput { get; set; }   //user input tests

        public List<CorrectAnswer> CorrectAnswer { get; set; } //contain true result/results for this practice

        public SubLecture Sublectures { get; set; }
    }
}