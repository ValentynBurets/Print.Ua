using System;
using System.Collections.Generic;
using System.Text;
using Business.Interface.Models.OrdersInfoService;

namespace Api.Models.OrderController.DetailOrderInfoModels
{
    public class CustomerRoleDetailOrderInfoModel : IDetailOrderInfoModel
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string State { get; set; }
        public DateTime CreationDate { get; set; }
        public long TTN { get; set; }
        public UserInfoModel Employee { get; set; }
        public bool IsCanceled { get; set; }
        public ICollection<ProductInfoModel> Products { get; set; }
    }
}
