using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class Course
    {
        [Required]
        public string ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public ApplicationUser Artist { get; set; } //Belonging to the user

        [Key]
        public int CourseId { get; set; } //1,2,3, ...

        public CourseType CourseType { get; set; }

        [Required]
        [Display(Name = "Тип курса : ")]
        public int CourseTypeId { get; set; }

        //[StringLength(1000)]
        //public string CourseType { get; set; } //interactive or quiz 

        [Required]
        [StringLength(1000)]
        [Display(Name = "Название курса : ")]
        public string CourseName {get; set;}  //The .NET basics, .NET design and so on

        [StringLength(2500)]
        [Display(Name = "Описание курса : ")]
        public string CourseInfo { get; set; }  //some text info about common course

        [Required]
        [Display(Name = "Дата создания курса : ")]
        public DateTime DateOfCourseCreation { get; set; } //date of course creation

        

        public virtual List<Lecture> Lectures { get; set; } //every Course can contain some lectures

        
    }
}