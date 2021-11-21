using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Interface.Models
{
    public class UpdateEmailModel
    {
        [Required]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}:;<>,.?~_+-=|]).{10,25}$")]
        public string CurrentPassword { get; set; }

        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
