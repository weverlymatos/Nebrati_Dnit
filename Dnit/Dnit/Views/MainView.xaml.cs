using Dnit.Core.ViewModels;
using Dnit.Core.ViewModels.Base;
using Xamarin.Forms;


namespace Dnit.Core.Views
{
    public partial class MainView : TabbedPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainViewModel, int>(this, MessageKeys.ChangeTab, (sender, arg) =>
            {
                switch (arg)
                {
                    case 0:
                        CurrentPage = HomeView;
                        break;
                    case 1:
                        CurrentPage = LogoutView;
                        break;
                    case 2:
                        //CurrentPage = BasketView;
                        break;
                    case 3:
                        //CurrentPage = CampaignView;
                        break;
                }
            });

            await ((CatalogViewModel)HomeView.BindingContext).InitializeAsync(null);
            await ((ProfileViewModel)LogoutView.BindingContext).InitializeAsync(null);

        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            
            ////////if (CurrentPage is ProfileView)
            ////////{
            ////////    // Force profile view refresh every time we access it
            ////////   ///// await (ProfileView.BindingContext as ViewModelBase).InitializeAsync(null);
            ////////}
        }
    }
}
