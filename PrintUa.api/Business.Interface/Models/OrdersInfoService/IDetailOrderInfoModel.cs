using System;
using System.Collections.Generic;
using System.Text;
using Business.Interface.Models.OrdersInfoService;

namespace Business.Interface.Models.OrdersInfoService
{
    public interface IDetailOrderInfoModel : IOrderInfoModel
    {
        public DateTime CreationDate { get; set; }
        public long TTN { get; set; }
    }
}
