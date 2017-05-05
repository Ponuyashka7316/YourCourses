using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class Course
    {
        [Required]
        public ApplicationUser Artist { get; set; } //Belonging to the user

        [Key]
        public int CourseId { get; set; } //1,2,3, ...

        [Required]
        public CourseType CourseType { get; set; }

        //[StringLength(1000)]
        //public string CourseType { get; set; } //interactive or quiz 

        [Required]
        [StringLength(1000)]
        public string CourseName {get; set;}  //The .NET basics, .NET design and so on

        [StringLength(2500)]
        public string CourseInfo { get; set; }  //some text info about common course

        [Required]
        public DateTime DateOfCourseCreation { get; set; } //date of course creation

        public List<Lecture> Lectures { get; set; } //every Course can contain some lectures

    }
}