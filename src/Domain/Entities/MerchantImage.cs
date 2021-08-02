using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class MerchantImage : AuditableEntity
    {
        public int MerchantId { get; set; }
        [ForeignKey("MerchantId")]
        public Merchant Merchant { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDefault { get; set; }
    }
}
