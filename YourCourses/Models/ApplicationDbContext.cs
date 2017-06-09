using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace YourCourses.Models
{
    
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        //for interactive courses
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<SubLecture> SubLectures { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Image> Images { get; set; }

        //for quiz courses
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }


        public ApplicationDbContext()
            : base("aspnet-YourCourses-20170505085850", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}