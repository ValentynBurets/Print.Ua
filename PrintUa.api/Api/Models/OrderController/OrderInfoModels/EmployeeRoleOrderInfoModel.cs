using Business.Interface.Models.OrdersInfoService;
using System;

namespace Api.Models.OrderController.OrderInfoModels
{
    public class EmployeeRoleOrderInfoModel : IOrderInfoModel
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string State { get; set; }
        public DateTime CreationDate { get; set; }
        public long TTN { get; set; }
        public bool IsCanceled { get; set; }
    }
}
