using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Data;

namespace Business.Interface.Services
{
    public interface IProfileRegistrationService
    {
        public Task<bool> CreateProfile(SystemUser user, string firstName, string lastName);
    }
}
