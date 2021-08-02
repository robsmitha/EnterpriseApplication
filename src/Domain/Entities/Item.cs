using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Item : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public string Code { get; set; }
        public string Sku { get; set; }
        public bool DefaultTaxRates { get; set; }
        public bool IsRevenue { get; set; }
        public string LookupCode { get; set; }
        public decimal? Percentage { get; set; }
        public int? MaxAllowed { get; set; }
        public string ShortDescription { get; set; }

        public int MerchantId { get; set; }

        [ForeignKey("MerchantId")]
        public Merchant Merchant { get; set; }

        public int ItemTypeId { get; set; }

        [ForeignKey("ItemTypeId")]
        public ItemType ItemType { get; set; }

        public int UnitTypeId { get; set; }

        [ForeignKey("UnitTypeId")]
        public UnitType UnitType { get; set; }

        public int PriceTypeId { get; set; }

        [ForeignKey("PriceTypeId")]
        public PriceType PriceType { get; set; }

        public bool HasKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return true;

            return Name?.Contains(keyword) == true
                || Description?.Contains(keyword) == true
                || ItemType?.Name?.Contains(keyword) == true
                || ItemType?.Description?.Contains(keyword) == true;
        }
    }
}
