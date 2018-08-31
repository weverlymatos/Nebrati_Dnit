using Dnit.Core.Models.Location;
using Dnit.Core.Models.User;
using Dnit.Core.Services.Dependency;
using Dnit.Core.Services.Location;
using Dnit.Core.Services.Settings;
using Dnit.Core.ViewModels.Base;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dnit.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private bool _useAzureServices;
        private bool _allowGpsLocation;
        private bool _useFakeLocation;
        private string _identityEndpoint;
        private string _gatewayCatalogEndpoint;       
        private double _latitude;
        private double _longitude;
        private string _gpsWarningMessage;

        private readonly ISettingsService _settingsService;
        private readonly ILocationService _locationService;
        private readonly IDependencyService _dependencyService;

        public SettingsViewModel(ISettingsService settingsService, ILocationService locationService, IDependencyService dependencyService)
        {
            _settingsService = settingsService;
            _locationService = locationService;
            _dependencyService = dependencyService;

            _useAzureServices = !_settingsService.UseMocks;
            _identityEndpoint = _settingsService.IdentityEndpointBase;
            _gatewayCatalogEndpoint = _settingsService.GatewayCatalogEndpointBase;            
            _latitude = double.Parse(_settingsService.Latitude, CultureInfo.CurrentCulture);
            _longitude = double.Parse(_settingsService.Longitude, CultureInfo.CurrentCulture);
            _useFakeLocation = _settingsService.UseFakeLocation;
            _allowGpsLocation = _settingsService.AllowGpsLocation;
            _gpsWarningMessage = string.Empty;
        }

        public string TitleUseAzureServices
        {
            get { return !UseAzureServices ? "Use Mock Services" : "Use Microserviços do Dnit"; }
        }

        public string DescriptionUseAzureServices
        {
            get
            {
                return !UseAzureServices
                    ? "Mock Services são objetos simulados que imitam o comportamento de serviços reais usando uma abordagem controlada."
                        : "Ao ativar o uso de microsserviços, o aplicativo tentará usar serviços reais implementados no ponto de extremidade base especificado, que deverá ser acessível pela rede.";
            }
        }

        public bool UseAzureServices
        {
            get => _useAzureServices;
            set
            {
                _useAzureServices = value;
                UpdateUseAzureServices();
                RaisePropertyChanged(() => UseAzureServices);
            }
        }

        public string TitleUseFakeLocation
        {
            get { return !UseFakeLocation ? "Use localização real" : "Use localização Fake"; }
        }

        public string DescriptionUseFakeLocation
        {
            get
            {
                return !UseFakeLocation
                    ? "Ao ativar o local, o aplicativo tentará usar o local do dispositivo."
                        : "Dados falsos de localização são adicionados para testes de localização do usuário.";
            }
        }

        public bool UseFakeLocation
        {
            get => _useFakeLocation;
            set
            {
                _useFakeLocation = value;
                UpdateFakeLocation();
                RaisePropertyChanged(() => UseFakeLocation);
            }
        }

        public string TitleAllowGpsLocation
        {
            get { return !AllowGpsLocation ? "Localização GPS desativada" : "Localização GPS ativada"; }
        }

        public string DescriptionAllowGpsLocation
        {
            get
            {
                return !AllowGpsLocation
                    ? "Ao desativar o local, você não receberá campanhas de localização com base na sua localização."
                        : "Ao ativar o local, você receberá campanhas de localização com base na sua localização.";
            }
        }

        public string GpsWarningMessage
        {
            get => _gpsWarningMessage;
            set
            {
                _gpsWarningMessage = value;
                RaisePropertyChanged(() => GpsWarningMessage);
            }
        }

        public string IdentityEndpoint
        {
            get => _identityEndpoint;
            set
            {
                _identityEndpoint = value;
                if (!string.IsNullOrEmpty(_identityEndpoint))
                {
                    UpdateIdentityEndpoint();
                }
                RaisePropertyChanged(() => IdentityEndpoint);
            }
        }

        public string GatewayCatalogEndpoint
        {
            get => _gatewayCatalogEndpoint;
            set
            {
                _gatewayCatalogEndpoint = value;
                if (!string.IsNullOrEmpty(_gatewayCatalogEndpoint))
                {
                    UpdateGatewayCatalogEndpoint();
                }
                RaisePropertyChanged(() => GatewayCatalogEndpoint);
            }
        }              

        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                UpdateLatitude();
                RaisePropertyChanged(() => Latitude);
            }
        }

        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                UpdateLongitude();
                RaisePropertyChanged(() => Longitude);
            }
        }

        public bool AllowGpsLocation
        {
            get => _allowGpsLocation;
            set
            {
                _allowGpsLocation = value;
                UpdateAllowGpsLocation();
                RaisePropertyChanged(() => AllowGpsLocation);
            }
        }

        public bool UserIsLogged => !string.IsNullOrEmpty(_settingsService.AuthAccessToken);

        public ICommand ToggleMockServicesCommand => new Command(async () => await ToggleMockServicesAsync());

        public ICommand ToggleFakeLocationCommand => new Command(ToggleFakeLocationAsync);

        public ICommand ToggleSendLocationCommand => new Command(async () => await ToggleSendLocationAsync());

        public ICommand ToggleAllowGpsLocationCommand => new Command(ToggleAllowGpsLocation);

        private async Task ToggleMockServicesAsync()
        {
            ViewModelLocator.UpdateDependencies(!UseAzureServices);
            RaisePropertyChanged(() => TitleUseAzureServices);
            RaisePropertyChanged(() => DescriptionUseAzureServices);

            var previousPageViewModel = NavigationService.PreviousPageViewModel;
            if (previousPageViewModel != null)
            {
                if (previousPageViewModel is MainViewModel)
                {
                    // Slight delay so that page navigation isn't instantaneous
                    await Task.Delay(1000);
                    if (UseAzureServices)
                    {
                        _settingsService.AuthAccessToken = string.Empty;
                        _settingsService.AuthIdToken = string.Empty;

                        await NavigationService.NavigateToAsync<LoginViewModel>(new LogoutParameter { Logout = true });
                        await NavigationService.RemoveBackStackAsync();
                    }
                }
            }
        }

        private void ToggleFakeLocationAsync()
        {
            ViewModelLocator.UpdateDependencies(!UseAzureServices);
            RaisePropertyChanged(() => TitleUseFakeLocation);
            RaisePropertyChanged(() => DescriptionUseFakeLocation);
        }

        private async Task ToggleSendLocationAsync()
        {
            if (!_settingsService.UseMocks)
            {
                var locationRequest = new Location
                {
                    Latitude = _latitude,
                    Longitude = _longitude
                };
                var authToken = _settingsService.AuthAccessToken;

                await _locationService.UpdateUserLocation(locationRequest, authToken);
            }
        }

        private void ToggleAllowGpsLocation()
        {
            RaisePropertyChanged(() => TitleAllowGpsLocation);
            RaisePropertyChanged(() => DescriptionAllowGpsLocation);
        }

        private void UpdateUseAzureServices()
        {
            // Save use mocks services to local storage
            _settingsService.UseMocks = !_useAzureServices;
        }

        private void UpdateIdentityEndpoint()
        {
            // Update remote endpoint (save to local storage)
            GlobalSetting.Instance.BaseIdentityEndpoint = _settingsService.IdentityEndpointBase = _identityEndpoint;
        }

        private void UpdateGatewayCatalogEndpoint()
        {
            GlobalSetting.Instance.BaseGatewayCatalogEndpoint = _settingsService.GatewayCatalogEndpointBase = _gatewayCatalogEndpoint;
        }

       

        private void UpdateFakeLocation()
        {
            _settingsService.UseFakeLocation = _useFakeLocation;
        }

        private void UpdateLatitude()
        {
            // Update fake latitude (save to local storage)
            _settingsService.Latitude = _latitude.ToString();
        }

        private void UpdateLongitude()
        {
            // Update fake longitude (save to local storage)
            _settingsService.Longitude = _longitude.ToString();
        }

        private void UpdateAllowGpsLocation()
        {
            if (_allowGpsLocation)
            {
                var locator = _dependencyService.Get<ILocationServiceImplementation>();
                if (!locator.IsGeolocationEnabled)
                {
                    _allowGpsLocation = false;
                    GpsWarningMessage = "Ativar o sensor GPS no seu dispositivo";
                }
                else
                {
                    _settingsService.AllowGpsLocation = _allowGpsLocation;
                    GpsWarningMessage = string.Empty;
                }
            }
            else
            {
                _settingsService.AllowGpsLocation = _allowGpsLocation;
            }
        }
    }
}