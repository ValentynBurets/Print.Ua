using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ProductImageViewModel
    {
        public Guid Id { get; set; }
        public byte[] Picture { get; set; }
    }
}
