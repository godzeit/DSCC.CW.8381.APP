using DSCC.CW._8381.APP.DBO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DSCC.CW._8381.APP.Models
{
    public class CinemaViewModel : Cinema, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            return validationResult;
        }

        public CinemaViewModel()
         : base()
        {
        }

        public CinemaViewModel(Cinema model)
         : base(model)
        {
        }
    }
}
