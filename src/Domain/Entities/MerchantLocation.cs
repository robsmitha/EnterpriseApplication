using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class MerchantLocation : AddressEntity
    {
        public int MerchantId { get; set; }

        [ForeignKey("MerchantId")]
        public Merchant Merchant { get; set; }
        public bool IsDefault { get; set; }
        public string Phone { get; set; }
        public string OperatingHours { get; set; }
        public string ContactEmail { get; set; }
    }
}
