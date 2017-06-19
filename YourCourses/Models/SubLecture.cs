using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace YourCourses.Models
{
    public class SubLecture
    {
        [Key]
        public int SubId { get; set; } //Ordinal number of the sublecture

        [Required]
        public string ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public ApplicationUser Artist { get; set; } //Belonging to the user

        [DefaultValue(0)]
        [Display(Name = "Оценка лекции : ")]
        public int CurrentRating { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Имя лекции : ")]
        public string SubName { get; set; } //sublect. name

        //public int? ImageId { get; set; }

        //[ForeignKey("ImageId")]
        //public Image? Image { get; set; } //it's define the type of sublection(only lection or lection + practice) determines the type of image(three images from the Content folder)

        [Required]
        [Display(Name = "Содержимое лекции : ")]
        public string LectureAdminOutput { get; set; } // it is the lecture text

        [Required]
        [Display(Name = "Модуль : ")]
        public int LectureLectureId { get; set; }

        [ForeignKey("LectureLectureId")]
        [Display(Name = "Модуль : ")]
        public virtual Lecture Lecture { get; set; }

        public virtual List<Practice> Practices { get; set; } //each lecture can contain {0; 10} practices
    }
}