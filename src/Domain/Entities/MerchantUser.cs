using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class MerchantUser : AuditableEntity
    {
        public int MerchantId { get; set; }
        [ForeignKey("MerchantId")]
        public Merchant Merchant { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
