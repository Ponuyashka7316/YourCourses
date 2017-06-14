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
        [Display(Name = "Id : ")]
        public int PracticeId { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Название практики : ")]
        public string PracticeName { get; set; } //Name of practice exercise

        
        
        [Display(Name = "Описание практики : ")]
        public string PracticeDescription { get; set; }

        [Display(Name = "Пользовательский ввод : ")]
        public string PracticeUserInput { get; set; } //user input text from textArea, Sets the default value for textArea

        [Required]
        [Display(Name = "Первая часть(директивы и метод main) : ")] //Метод Main
        public string FirstPart { get; set; }   //first part

        [Display(Name = "Тестовая часть(является частью main, не видна пользователю) : ")] //в классе методе Main 
        public string TestsPart { get; set; }   //not visible for user

        [Display(Name = "Вторая часть : ")]
        public string SecondPart { get; set; }    //second part after USER INPUT ("PracticeUserInput" prop)

        [Display(Name = "Оценка практики : ")]
        public double Mark { get; set; }    //middle users votes for the practice

        [Required]
        [Display(Name = "Лекция : ")]
        public int SublectureSubId { get; set; }

        public virtual List<CorrectAnswer> CorrectAnswer { get; set; } //contain true result/results for this practice

        [ForeignKey("SublectureSubId")]
        public virtual SubLecture Sublectures { get; set; }

        public virtual List<PracticeAndUserMark> PracticeAndUserMark { get; set; }
    }
}