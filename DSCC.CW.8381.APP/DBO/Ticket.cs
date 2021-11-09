using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSCC.CW._8381.APP.DBO
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Count { get; set; }

        [Required]
        [DisplayName("Session")]
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }

        public Ticket()
        {
        }

        public Ticket(Ticket model)
        {
            Id = model.Id;
            Count = model.Count;
            SessionId = model.SessionId;
            Session = model.Session;
        }
    }
}
