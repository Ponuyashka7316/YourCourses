using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public virtual List<AnswerChoice> AnswerChoices { get; set; }
        public int AnswerId { get; set; }
        public Answer Answers { get; set; }
        public int QuizQuizId { get; set; }
        [ForeignKey("QuizQuizId")]
        public virtual Quiz Quizs { get; set; }
    }
}