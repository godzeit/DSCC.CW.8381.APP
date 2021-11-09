using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSCC.CW._8381.APP.DBO
{
    public class Cinema
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [DisplayName("Cinema")]
        public string Name { get; set; }


        public Cinema()
        {
        }

        public Cinema(Cinema model)
        {
            Id = model.Id;
            Name = model.Name;
        }
    }
}
