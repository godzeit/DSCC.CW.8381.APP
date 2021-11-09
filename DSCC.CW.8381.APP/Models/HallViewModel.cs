using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DSCC.CW._8381.APP.DBO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DSCC.CW._8381.APP.Models
{
    public class HallViewModel : Hall, IValidatableObject
    {
        public SelectList Cinemas { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            return validationResult;
        }

        public HallViewModel()
         : base()
        {
        }

        public HallViewModel(Hall model)
         : base(model)
        {
        }
    }
}
