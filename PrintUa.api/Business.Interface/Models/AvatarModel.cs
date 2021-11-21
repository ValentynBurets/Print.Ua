using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Interface.Models
{
    public class AvatarModel
    {
        [Required]
        public string EncodedPictureBase64 { get; set; }
    }
}
