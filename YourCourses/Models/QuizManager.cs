using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class QuizManager
    {
        static QuizManager instance;
        private ApplicationDbContext db = new ApplicationDbContext();
        int questionId = 1;
        public bool IsComplete = false;
        public Quiz quiz;

        private QuizManager()
        {
            quiz = new Quiz();
            quiz.StartTime = DateTime.Now;
            quiz.QuizId = 1;
            quiz.Score = 0;
        }

        public static QuizManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuizManager();
                return instance;
            }
        }

        public Question LoadQuiz()
        {
            var question = db.Questions.Find(questionId);
            return question;
        }

        public void SaveAnswer(string answers)
        {
            var question = db.Questions.Include("Answers").Where(x => x.QuestionId == questionId).Single();
            if (question.Answers.AnswerText == answers)
                quiz.Score++;
        }

        public bool MoveToNextQuestion()
        {
            bool canMove = false;

            if (db.Questions.Count() > questionId)
            {
                questionId++;
                canMove = true;
            }

            return canMove;
        }

        public bool PreviosQuestion()
        {
            bool canMove = false;

            if (questionId > 1)
            {
                questionId--;
                canMove = true;
            }

            return canMove;
        }
    }
}