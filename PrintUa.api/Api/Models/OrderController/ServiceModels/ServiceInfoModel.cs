using System;

namespace Api.Models.OrderController
{
    public class ServiceInfoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ServiceMaterialInfoModel Material { get; set; }
        public ServiceFormatInfoModel Format { get; set; }
        public double Cost { get; set; }
    }
}
