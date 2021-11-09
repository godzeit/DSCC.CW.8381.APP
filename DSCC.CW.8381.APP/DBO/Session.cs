using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSCC.CW._8381.APP.DBO
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Hall")]
        public int HallId { get; set; }
        public virtual Hall Hall { get; set; }

        [Required]
        [DisplayName("Movie")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [Required]
        [DisplayName("Date and Time")]
        public DateTime DateTime { get; set; }

        [NotMapped]
        public string Date { get => DateTime.ToString("d MMM yyyy"); }

        [NotMapped]
        public string Time { get => DateTime.ToString("H:mm"); }

        [DisplayName("Available Tickets")]
        public int? SeatsLeft { get; set; }

        public Session()
        {
        }

        public Session(Session model)
        {
            Id = model.Id;
            HallId = model.HallId;
            Hall = model.Hall;
            MovieId = model.MovieId;
            Movie = model.Movie;
            DateTime = model.DateTime;
        }
    }
}
