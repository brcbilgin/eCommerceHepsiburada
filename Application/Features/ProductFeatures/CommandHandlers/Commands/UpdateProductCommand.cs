using Application.Features.ProductFeatures.CommandHandlers.Results;
using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.ProductFeatures.CommandHandlers.Commands
{
    public class UpdateProductCommand : RequestBase, IRequest<UpdateProductResult>
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
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
