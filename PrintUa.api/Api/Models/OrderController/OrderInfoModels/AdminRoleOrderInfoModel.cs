using Business.Interface.Models.OrdersInfoService;
using System;

namespace Api.Models.OrderController.OrderInfoModels
{
    public class AdminRoleOrderInfoModel : IOrderInfoModel
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string State { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerName { get; set; }
        public bool IsCanceled { get; set; }
    }
}
