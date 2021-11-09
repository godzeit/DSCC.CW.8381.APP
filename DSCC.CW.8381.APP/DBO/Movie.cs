using DSCC.CW._8381.APP.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSCC.CW._8381.APP.DBO
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [DisplayName("Movie")]
        public string Name { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [DisplayName("Duration (min.)")]
        public int Duration { get; set; }

        public Movie()
        {
        }

        public Movie(Movie model)
        {
            Id = model.Id;
            Name = model.Name;
            Category = model.Category;
            Duration = model.Duration;
        }
    }
}
