using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Customer : User
    {
        public Customer(Guid idLink) : base(idLink)
        {
        }

        public virtual ICollection<Order> MyOrdersAsCustomer { get; set; }
    }
}
