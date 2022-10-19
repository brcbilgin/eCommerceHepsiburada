using Application.Features.CampaignFeatures.CommandHandlers.Commands;
using Application.Features.CampaignFeatures.CommandHandlers.Results;
using Application.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.CampaignFeatures.CommandHandlers
{

    public class UpdateCampaignDiscountCommandHandler : IRequestHandler<UpdateCampaignDiscountCommand, UpdateCampaignDiscountResult>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCampaignDiscountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UpdateCampaignDiscountResult> Handle(UpdateCampaignDiscountCommand command, CancellationToken cancellationToken)
        {
            var allActiveCampaigns = _context.Campaigns.Where(a => a.IsActive).ToList();

            foreach (var campaign in allActiveCampaigns)
            {
                var product = _context.Products.Where(a => a.ProductCode == campaign.ProductCode).FirstOrDefault();
                if (product != null)
                {
                    var totalSale = _context.Orders.Where(a => a.ProductCode == campaign.ProductCode).Sum(a => a.Quantity);
                    if (totalSale == 0)
                    {
                        decimal newDiscountPercent = campaign.DiscountPercent + 10;
                        if (newDiscountPercent < campaign.PriceManipulationLimit)
                        {
                            campaign.DiscountPercent = newDiscountPercent; // satış yoksa %10 tutarında indirimi arttır.
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        if (product.Stock != 0)
                        {
                            decimal saledPercent = totalSale * 100 / Convert.ToDecimal(product.Stock);
                            if (saledPercent < 50)
                            {
                                decimal newDiscountPercent = campaign.DiscountPercent + 10;
                                if (newDiscountPercent < campaign.PriceManipulationLimit)
                                {
                                    campaign.DiscountPercent = newDiscountPercent; // satış azsa %10 tutarında indirimi arttır.
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }
            }

            return new UpdateCampaignDiscountResult();
        }
    }
}
