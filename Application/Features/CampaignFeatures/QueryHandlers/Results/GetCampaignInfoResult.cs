using Application.Dto;

namespace Application.Features.CampaignFeatures.QueryHandlers.Results
{
  public class GetCampaignInfoResult
    {
        public string Name { get; set; }
        public Status Status { get; set; }
        public int TargetSalesCount { get; set; }
        public int TotalSales { get; set; }
        public decimal Turnover { get; set; }
        public decimal AverageItemPrice { get; set; }
    }
}