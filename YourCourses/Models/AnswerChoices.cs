using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class AnswerChoice
    {
        public int AnswerChoiceId { get; set; }
        public string Choices { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}