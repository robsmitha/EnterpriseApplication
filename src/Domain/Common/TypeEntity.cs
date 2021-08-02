using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public class TypeEntity : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
