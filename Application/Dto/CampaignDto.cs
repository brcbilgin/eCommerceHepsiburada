
using System;

namespace Application.Dto
{
    public class CampaignDto : BaseDto
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public decimal PriceManipulationLimit { get; set; } 
        public int TargetSalesCount { get; set; }
        public decimal Turnover { get; set; }
        public int TotalSales { get; set; }
        public decimal AverageItemPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime BeginDate { get; set; }
        public bool IsActive { get; set; }
        public Status Status => IsActive ? Status.Continuing : Status.Ended;

    }

    public enum Status
    {
        Continuing,
        Ended
    }
}
