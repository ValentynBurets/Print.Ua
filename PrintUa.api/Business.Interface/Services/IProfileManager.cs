using Business.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface.Services
{
    public interface IProfileManager
    {
        public Task<string> GetPhoneNumberByUserId(Guid id);
        public Task UpdatePhoneNumberByUserId(UpdatePhoneNumberModel model, Guid id);
        public Task<string> GetEmailByUserId(Guid id);
        public Task UpdateEmailByUserId(UpdateEmailModel model, Guid id);
        public Task UpdatePasswordByUserId(UpdatePasswordModel model, Guid id);
    }
}
