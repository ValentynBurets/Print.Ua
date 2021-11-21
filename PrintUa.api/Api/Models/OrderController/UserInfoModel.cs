using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models.OrderController
{
    public class UserInfoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
