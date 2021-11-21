using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Employee : User
    {
        public Employee(Guid idLink) : base(idLink)
        {
        }

        public virtual ICollection<Order> MyOrdersAsEmployee { get; set; }
    }
}
