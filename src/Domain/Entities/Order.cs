using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : AuditableEntity
    {
        public string Note { get; set; }
        public int OrderStatusTypeId { get; set; }

        [ForeignKey("OrderStatusTypeId")]
        public OrderStatusType OrderStatusType { get; set; }

        /// <summary>
        /// Merchant the line items in the order belong to
        /// </summary>
        public int MerchantId { get; set; }

        [ForeignKey("MerchantId")]
        public Merchant Merchant { get; set; }

        /// <summary>
        /// Customer who placed the order if logged in
        /// </summary>
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }

        /// <summary>
        /// indicates order is open and needs to be paid for
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public bool IsOpenOrder()
        {
            if (OrderStatusType == null) return false;
            return OrderStatusType.IsOpenOrderStatus();
        }
    }
}
