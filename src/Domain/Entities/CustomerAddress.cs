using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class CustomerAddress : AddressEntity
    {
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }
        public bool IsDefault { get; set; }
    }
}
