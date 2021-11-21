using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Admin : User
    {
        public Admin(Guid idLink) : base(idLink)
        {
        }
    }
}
