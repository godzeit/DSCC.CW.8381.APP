using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSCC.CW._8381.APP.DBO
{
    public class Hall
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [DisplayName("Hall")]
        public string Name { get; set; }

        [NotMapped]
        public string FullName { get => $"{Name} ({Cinema?.Name})"; }


        [Required]
        [DisplayName("Seats")]
        public int PlacesCount { get; set; }

        [Required]
        [DisplayName("Cinema")]
        public int CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }


        public Hall()
        {
        }

        public Hall(Hall model)
        {
            Id = model.Id;
            Name = model.Name;
            PlacesCount = model.PlacesCount;
            CinemaId = model.CinemaId;
            Cinema = model.Cinema;
        }
    }
}
