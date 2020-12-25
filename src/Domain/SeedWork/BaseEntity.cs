using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.SeedWork
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }
        public BaseEntity()
        {
            //TODO: Move to save changes implementation in db context
            CreatedAt = DateTime.Now;
            Active = true;
        }
    }
}
