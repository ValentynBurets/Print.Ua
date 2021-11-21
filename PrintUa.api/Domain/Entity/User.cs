using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entity.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entity
{
    public abstract class User : EntityBase
    {
        public User(Guid idLink)
        {
            IdLink = idLink;
        }

        public Guid IdLink { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
