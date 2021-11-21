using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interface.Models.OrdersInfoService
{
    public interface IOrderInfoModel
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string State { get; set; }
    }
}
