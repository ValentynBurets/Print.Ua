using Business.Interface.Models.OrdersInfoService;
using System;
using System.Collections.Generic;

namespace Api.Models.OrderController.DetailOrderInfoModels
{
    public class EmployeeRoleDetailOrderInfoModel : IDetailOrderInfoModel
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string State { get; set; }
        public DateTime CreationDate { get; set; }
        public long TTN { get; set; }
        public UserInfoModel Customer { get; set; }
        public bool IsCanceled { get; set; }
        public ICollection<ProductInfoModel> Products { get; set; }
    }
}
