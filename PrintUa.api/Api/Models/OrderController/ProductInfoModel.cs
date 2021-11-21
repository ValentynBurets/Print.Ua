using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models.OrderController
{
    public class ProductInfoModel
    {
        public Guid Id { get; set; }
        public byte[] Picture { get; set; }
        public ServiceInfoModel Service { get; set; }
        public int Amount { get; set; }
    }
}
