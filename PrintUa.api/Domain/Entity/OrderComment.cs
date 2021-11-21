using System;
using Domain.Entity.Base;

namespace Domain.Entity
{
    public class OrderComment : EntityBase
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public virtual Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
