using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DSCC.CW._8381.APP.DBO;

namespace DSCC.CW._8381.APP.Models
{
    public class TicketViewModel : Ticket, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            return validationResult;
        }

        public TicketViewModel()
         : base()
        {
        }

        public TicketViewModel(Ticket model)
         : base(model)
        {
        }
    }
}
