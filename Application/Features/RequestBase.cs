using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Features
{
    public abstract class RequestBase : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
