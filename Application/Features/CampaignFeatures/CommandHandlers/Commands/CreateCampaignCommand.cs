using Application.Features.CampaignFeatures.CommandHandlers.Results;
using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.CampaignFeatures.CommandHandlers.Commands
{
    public class CreateCampaignCommand : RequestBase, IRequest<CreateCampaignResult>
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public decimal PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {           
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(Name))
                results.Add(new ValidationResult("Kampanya adı giriniz!"));
            if (string.IsNullOrEmpty(ProductCode))
                results.Add(new ValidationResult("Ürün kodu giriniz!"));

            base.Validate(validationContext).AddBaseValidationMessages(results);

            return results;
        }
    }
}
