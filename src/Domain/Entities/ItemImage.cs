using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class ItemImage : AuditableEntity
    {
        public string ImageUrl { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public bool IsDefault { get; set; }
    }
}
