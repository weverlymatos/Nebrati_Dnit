using Dnit.Core.Helpers;
using Dnit.Core.Models.User;
using Dnit.Core.Services.RequestProvider;
using Dnit.Core.Services.Settings;
using System;
using System.Threading.Tasks;

namespace Dnit.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRequestProvider _requestProvider;

        public UserService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<UserInfo> GetUserInfoAsync(string authToken)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.UserInfoEndpoint);

            var userInfo = await _requestProvider.GetAsync<UserInfo>(uri, authToken);
            return userInfo;
        }
    }
}
