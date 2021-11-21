using Business.Interface.Models;
using Business.Interface.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProfileManager<T> : UserManager<T>, IProfileManager where T : IdentityUser
    {
        public ProfileManager(IUserStore<T> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<T> passwordHasher, IEnumerable<IUserValidator<T>> userValidators,
            IEnumerable<IPasswordValidator<T>> passwordValidators, ILookupNormalizer lookupNormalizer, 
            IdentityErrorDescriber identityErrorDescriber, IServiceProvider serviceProvider,
            ILogger<UserManager<T>> logger) : 
                base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators,
                lookupNormalizer, identityErrorDescriber, serviceProvider, logger)
        {

        }

        public async Task<string> GetEmailByUserId(Guid id)
        {
            var user = await FindByIdAsync(id.ToString());

            return user.Email;
        }

        public async Task<string> GetPhoneNumberByUserId(Guid id)
        {
            var user = await FindByIdAsync(id.ToString());

            return user.PhoneNumber;
        }

        public async Task UpdateEmailByUserId(UpdateEmailModel model, Guid id)
        {
            var user = await FindByIdAsync(id.ToString());

            if (!await CheckPasswordAsync(user, model.CurrentPassword))
            {
                throw new Exception("Wrong password!");
            }

            await SetEmailAsync(user, model.NewEmail);
            await SetUserNameAsync(user, model.NewEmail);
        }

        public async Task UpdatePasswordByUserId(UpdatePasswordModel model, Guid id)
        {
            var user = await FindByIdAsync(id.ToString());

            var operationResult = await ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!operationResult.Succeeded)
            {
                throw new Exception("Wrong password!");
            }
        }

        public async Task UpdatePhoneNumberByUserId(UpdatePhoneNumberModel model, Guid id)
        {
            var user = await FindByIdAsync(id.ToString());

            if (!await CheckPasswordAsync(user, model.CurrentPassword))
            {
                throw new Exception("Wrong password!");
            }

            await SetPhoneNumberAsync(user, model.NewPhoneNumber);
        }
    }
}
