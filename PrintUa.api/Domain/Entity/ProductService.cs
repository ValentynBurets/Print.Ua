using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity.Base;

namespace Domain.Entity
{
    public class ProductService : EntityBase
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public Guid MaterialId { get; set; }
        public virtual ServiceMaterial Material { get; set; }
        public Guid FormatId { get; set; }
        public virtual ServiceFormat Format { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public bool IsCanceled { get; set; }
    }
}
