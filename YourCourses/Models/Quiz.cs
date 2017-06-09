using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YourCourses.Models
{
    public class Quiz

    {

        [Required]
        public string ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public ApplicationUser Artist { get; set; } //Belonging to the user

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Info { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}