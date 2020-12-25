using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SystemConfiguration : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? Enabled { get; set; }
    }
}
