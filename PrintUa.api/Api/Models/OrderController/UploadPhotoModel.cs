using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.OrderController
{
    public class UploadPhotoModel
    {
        [Required]
        public Guid productId { get; set; }
        [Required]
        public string base64String { get; set; }
    }
}
