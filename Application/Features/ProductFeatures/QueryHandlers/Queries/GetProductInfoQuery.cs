using Application.Features.ProductFeatures.QueryHandlers.Results;
using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.ProductFeatures.QueryHandlers.Queries
{
   public class GetProductInfoQuery : RequestBase, IRequest<GetProductInfoResult>
    {
        public string ProductCode { get; set; }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(ProductCode))
                results.Add(new ValidationResult("Ürün kodu giriniz!"));

            base.Validate(validationContext).AddBaseValidationMessages(results);

            return results;
        }
    }
}
