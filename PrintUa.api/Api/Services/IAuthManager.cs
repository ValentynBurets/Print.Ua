using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserModel userModel);
        Task<string> CreateToken();
    }
}
