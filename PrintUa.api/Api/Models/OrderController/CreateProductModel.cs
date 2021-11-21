using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.OrderController
{
    public class CreateProductModel
    {
        [Required]
        public byte[] Picture { get; set; }

        public Guid ServiceId { get; set; }

        public int Amount { get; set; }
    }
}
