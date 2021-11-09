using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DSCC.CW._8381.APP.DBO;


namespace DSCC.CW._8381.APP.Models
{
    public class MovieViewModel : Movie, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            return validationResult;
        }

        public MovieViewModel()
         : base()
        {
        }

        public MovieViewModel(Movie model)
         : base(model)
        {
        }
    }
}
