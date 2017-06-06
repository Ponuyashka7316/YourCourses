﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourCourses.Models
{
    public class SubLecture
    {
        [Required]
        public ApplicationUser Artist { get; set; } //Belonging to the user

        [Key]
        public int SubId { get; set; } //Ordinal number of the sublecture

        [Required]
        [StringLength(1000)]
        public string SubName { get; set; } //sublect. name

        [Required]
        public int LectureLectureId { get; set; }

        public Lecture Lecture { get; set; }

        //public int ImageId { get; set; }

        //[ForeignKey("ImageId")]
        //public Image Image { get; set; } //it's define the type of sublection(only lection or lection + practice) determines the type of image(three images from the Content folder)

        [Required]
        public string LectureAdminOutput { get; set; } // it is the lecture text

        public List<Practice> Practices { get; set; } //each lecture can contain {0; 10} practices
    }
}