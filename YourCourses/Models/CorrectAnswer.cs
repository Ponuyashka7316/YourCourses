using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int PracticePracticeId { get; set; }

        [ForeignKey("PracticePracticeId")]
        public Practice Practices { get; set; }
    }
}