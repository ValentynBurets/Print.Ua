using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.Interface.Models
{
    public class UpdatePhoneNumberModel
    {
        [Required]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}:;<>,.?~_+-=|]).{10,25}$")]
        public string CurrentPassword { get; set; }

        [Phone]
        [Required]
        public string NewPhoneNumber { get; set; }
    }
}
