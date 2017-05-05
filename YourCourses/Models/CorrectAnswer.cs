using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class CorrectAnswer
    {
        [Key]
        public int Id  { get; set; }

        [Required]
        public string Answer { get; set; } //store the correct answer
    }
}