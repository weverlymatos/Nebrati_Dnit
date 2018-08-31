using Dnit.Core.Extensions;
using Dnit.Core.Models.User;
using Dnit.Core.Services.Settings;
using Dnit.Core.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dnit.Core.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
       

        public ProfileViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;           
        }

        public ICommand LogoutCommand => new Command(async () => await LogoutAsync());
        
        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            var authToken = _settingsService.AuthAccessToken;   
            IsBusy = false;
        }

        private async Task LogoutAsync()
        {
            IsBusy = true;

            // Logout
            await NavigationService.NavigateToAsync<LoginViewModel>(new LogoutParameter { Logout = true });
            await NavigationService.RemoveBackStackAsync();

            IsBusy = false;
        }

        
    }
}
