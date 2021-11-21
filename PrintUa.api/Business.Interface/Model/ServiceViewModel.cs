using Domain.Entity;
using System;

namespace Business.Model
{
    public class ServiceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public ServiceMaterial Material { get; set; }
        public ServiceFormat Format { get; set; }
        public bool IsCanceled { get; set; }
    }
}
