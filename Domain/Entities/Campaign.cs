using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public decimal PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime BeginDate { get; set; }
        public bool IsActive { get; set; }
    }
}