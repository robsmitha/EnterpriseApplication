using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Merchant : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CallToAction { get; set; }
        public string ShortDescription { get; set; }
        public string WebsiteUrl { get; set; }
        public int MerchantTypeId { get; set; }
        [ForeignKey("MerchantTypeId")]
        public MerchantType MerchantType { get; set; }
    }
}
