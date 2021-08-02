using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LineItem : AuditableEntity
    {
        public decimal ItemAmount { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
