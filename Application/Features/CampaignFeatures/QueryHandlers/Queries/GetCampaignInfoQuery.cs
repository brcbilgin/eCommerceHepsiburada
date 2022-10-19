using Application.Features.CampaignFeatures.QueryHandlers.Results;
using Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.CampaignFeatures.QueryHandlers.Queries
{
   public class GetCampaignInfoQuery : RequestBase, IRequest<GetCampaignInfoResult>
    {
        public string Name { get; set; }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (string.IsNullOrEmpty(Name))
                results.Add(new ValidationResult("Kampanya adı giriniz!"));

            base.Validate(validationContext).AddBaseValidationMessages(results);

            return results;
        }
    }
}
