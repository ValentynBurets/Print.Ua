using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity.Base;

namespace Domain.Entity
{
    public class Product : EntityBase
    {
        public byte[] Picture { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order ContainingOrder { get; set; }
        public Guid ServiceId { get; set; }
        public virtual ProductService ServiceInProduct { get; set; }
        public int Amount { get; set; }
    }
}
