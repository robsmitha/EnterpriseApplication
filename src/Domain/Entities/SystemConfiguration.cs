using Domain.Common;

namespace Domain.Entities
{
    public class SystemConfiguration : AuditableEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? Enabled { get; set; }
    }
}
