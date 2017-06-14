using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YourCourses.Models;

namespace YourCourses.Models
{
    public class PracticeAndUserMark //нужно для выставления оценки студенту за практику
    {
        public int Id { get; set; }

        [DefaultValue(0)]
        public int Mark { get; set; }

        [Required]
        public string ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public ApplicationUser Artist { get; set; } //Belonging to the user

        [Required]
        public int PracticePracticeId { get; set; }

        [ForeignKey("PracticePracticeId")]
        public Practice Practice { get; set; }
    }
}