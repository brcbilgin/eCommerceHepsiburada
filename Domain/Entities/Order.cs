﻿using Domain.Common;

namespace Domain.Entities
{
    public class Order: BaseEntity
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int? CampaignId { get; set; }

    }
}