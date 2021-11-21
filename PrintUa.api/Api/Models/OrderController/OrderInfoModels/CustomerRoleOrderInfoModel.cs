using Business.Interface.Models;
using Business.Interface.Models.OrdersInfoService;
using System;
using System.Collections.Generic;

namespace Api.Models.OrderController.OrderInfoModels
{
    public class CustomerRoleOrderInfoModel : IOrderInfoModel
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string State { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }    
        public long TTN { get; set; }
        public bool IsCanceled { get; set; }
        public ICollection<ProductImageViewModel> ImageArray { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
