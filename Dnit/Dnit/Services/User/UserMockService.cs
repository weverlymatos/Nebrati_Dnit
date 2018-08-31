using Dnit.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dnit.Core.Services.User
{
    public class UserMockService : IUserService
    {
        private UserInfo MockUserInfo = new UserInfo
        {
            UserId = Guid.NewGuid().ToString(),
            Name = "Weverly",
            LastName = "Matos",
            PreferredUsername = "Weverly",
            Email = "weverly@nebrati.com.br",
            EmailVerified = true,
            PhoneNumber = "1198661-4910",
            PhoneNumberVerified = true,
            Address = "São Paulo, SP",
            Street = "R. Dr. Orlando de Paiva Martins 168",
            ZipCode = "08410-330",
            Country = "Brasil",
            State = "São Paulo",
            CardNumber = "378282246310005",
            CardHolder = "Visa",
            CardSecurityNumber = "1234"
        };

        public async Task<UserInfo> GetUserInfoAsync(string authToken)
        {
            await Task.Delay(10);
            return MockUserInfo;
        }
    }
}
