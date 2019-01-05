using Most_MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Most_MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            Init();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        void Init()
        {
            MostIcon.HeightRequest = Constants.LogoIconHeight;
        }

        void EntryOffline(EventArgs e)
        {
            //DisplayAlert("Wejście do MOSTu", "Wejście bez logowania...", "OK");
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                //soon
            }
        }
	}
}