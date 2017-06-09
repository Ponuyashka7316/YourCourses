using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string FirstPart { get; set; }   //first part

        public string TestsPart { get; set; }   //not visible for user

        public string SecondPart { get; set; }    //second part after USER INPUT ("PracticeUserInput" prop)

        public double Mark { get; set; }    //middle users votes for the practice

        [Required]
        public int SublectureSubId { get; set; }

        public virtual List<CorrectAnswer> CorrectAnswer { get; set; } //contain true result/results for this practice

        [ForeignKey("SublectureSubId")]
        public virtual SubLecture Sublectures { get; set; }
    }
}