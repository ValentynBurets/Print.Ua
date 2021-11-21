using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity.Base;

namespace Domain.Entity
{
    public class Order : EntityBase
    {
        public int OrderNumber { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Employee Employee { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsCanceled { get; set; }
        public long TTN { get; set; }
        public OrderState State { get; set; }
        public virtual ICollection<Product> MyProducts { get; set; }
        public virtual ICollection<OrderComment> MyComments { get; set; }
    }
}
