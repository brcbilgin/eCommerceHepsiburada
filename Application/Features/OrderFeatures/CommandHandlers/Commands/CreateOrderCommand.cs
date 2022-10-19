using Application.Features.OrderFeatures.CommandHandlers.Results;
using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.OrderFeatures.CommandHandlers.Commands
{
    public class CreateOrderCommand : RequestBase, IRequest<CreateOrderResult>
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();     
            if(string.IsNullOrEmpty(ProductCode))
                results.Add(new ValidationResult("Ürün kodu giriniz!"));

            if (Quantity == 0)
                results.Add(new ValidationResult("Miktar giriniz!"));

            base.Validate(validationContext).AddBaseValidationMessages(results);

            return results;
        }
    }

    
}
