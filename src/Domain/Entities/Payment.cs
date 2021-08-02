using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public decimal Amount { get; set; }
        public int PaymentTypeId { get; set; }

        [ForeignKey("PaymentTypeId")]
        public PaymentType PaymentType { get; set; }
        public int PaymentStatusTypeId { get; set; }

        [ForeignKey("PaymentStatusTypeId")]
        public PaymentStatusType PaymentStatusType { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        // TODO: Add StripePayment extension
        //public string StripePaymentMethodId { get; set; }
        public DateTime ChargedAt { get; set; }
    }
}
