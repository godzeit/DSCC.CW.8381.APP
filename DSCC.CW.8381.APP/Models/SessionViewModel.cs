using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DSCC.CW._8381.APP.DBO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DSCC.CW._8381.APP.Models
{
    public class SessionViewModel : Session, IValidatableObject
    {
        public SelectList Halls { get; set; }
        public SelectList Movies { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();

            if (DateTime <= DateTime.Now)
            {
                validationResult.Add(new ValidationResult("Session date must be in future", new[] { "DateTime" }));
            }

            return validationResult;
        }

        public SessionViewModel()
         : base()
        {
        }

        public SessionViewModel(Session model)
         : base(model)
        {
        }
    }
}
