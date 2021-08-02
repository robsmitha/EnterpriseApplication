using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderStatusType : TypeEntity
    {
        /// <summary>
        /// Indicates the system allows for payments to be added to orders in the given status
        /// </summary>
        public bool CanAddPayment { get; set; }

        /// <summary>
        /// Indicates the system allows for lineitems to be added to orders in the given status
        /// </summary>
        public bool CanAddLineItem { get; set; }

        /// <summary>
        /// Indicates if order status type allows new line items or payments
        /// </summary>
        /// <returns></returns>
        public bool IsOpenOrderStatus()
        {
            return CanAddLineItem && CanAddPayment;
        }
    }
}
