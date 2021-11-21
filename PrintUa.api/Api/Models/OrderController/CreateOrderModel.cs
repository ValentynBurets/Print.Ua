using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.OrderController
{
    public class CreateOrderModel
    {
        [Required]
        public ICollection<CreateProductModel> Products { get; set; }
        public ICollection<CreateCommentViewModel> Comments { get; set; }
    }
}
