using Most_MobileApp.Database;
using Most_MobileApp.Models;
using SQLite;
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
	public partial class Construction : ContentPage
	{
        List<Kadra> data;
        public Construction ()
		{
			InitializeComponent ();
            Init();
            NavigationPage.SetHasNavigationBar(this, false);
            
            GetDataFromDatabase(11);
        }

        void Init()
        {
            MostIcon.HeightRequest = Constants.LogoIconHeightInApp;
            Contact_chef.HeightRequest = Constants.ProfileIconHeight;
            Phone_chef.HeightRequest = Constants.ProfileIconHeight;
            Email_chef.HeightRequest = Constants.ProfileIconHeight;

            Contact_vce1chef.HeightRequest = Constants.ProfileIconHeight;
            Phone_vce1chef.HeightRequest = Constants.ProfileIconHeight;
            Email_vce1chef.HeightRequest = Constants.ProfileIconHeight;

            Contact_vce2chef.HeightRequest = Constants.ProfileIconHeight;
            Phone_vce2chef.HeightRequest = Constants.ProfileIconHeight;
            Email_vce2chef.HeightRequest = Constants.ProfileIconHeight;

        }

        void MenuOption(object sender, EventArgs args)
        {
            Picker MainMenu = (Picker)sender;

            App.chosenMenuOption = MainMenu.SelectedIndex;

            switch (App.chosenMenuOption)
            {
                case 0: //SDA MOST
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new HomePage());
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        //soon
                    }
                    break;
                case 1: //Konstrukcja
                    DisplayAlert("", "Jesteś na Konstrukcja", "OK");
                    break;
                case 2: //Duszpasterze
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new Priests());
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        //soon
                    }
                    break;
                case 3: //Plan tygodnia
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new WeekPlan());
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        //soon
                    }
                    break;
                case 4: //Jak trafić?
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new HowTo());
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        //soon
                    }
                    break;
                case 5: //Kalendarz
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new MostCalendar());
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        //soon
                    }
                    break;
            }
        }

        void GetDataFromDatabase(int GlobalPrzesloId)
        {
            SQLiteConnection dbConnection;
            dbConnection = DependencyService.Get<IDBInterface>().CreateConnection();

            data = dbConnection.Query<Kadra>("Select * From [Kadra] Where Przeslo_ID = " + GlobalPrzesloId);
            InfoFromDb();
        }

        void InfoFromDb()
        {
            chef.Source = data[0].Photo;
            Chef_name.Text = data[0].Name + " " + data[0].Surname;
            Chef_phone.Text = data[0].Phone;
            Chef_mail.Text = data[0].E_mail;

            vce1_chef.Source = data[1].Photo;
            vce1_name.Text = data[1].Name + " " + data[1].Surname;
            vce1_phone.Text = data[1].Phone;
            vce1_mail.Text = data[1].E_mail;

            vce2_chef.Source = data[2].Photo;
            vce2_name.Text = data[2].Name + " " + data[2].Surname;
            vce2_phone.Text = data[2].Phone;
            vce2_mail.Text = data[2].E_mail;
        }

        void BackProcedure(EventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new NavigationPage(new StartPage());
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                //soon
            }
        }

        void GetPrzesloPage()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new NavigationPage(new PrzesloPage());
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                //soon
            }
        }

        void DuchoweView(EventArgs e)
        {
            App.GlobalPrzesloId = 1;
            GetPrzesloPage();
        }
        void LiturgiczneView(EventArgs e)
        {
            App.GlobalPrzesloId = 2;
            GetPrzesloPage();
        }
        void FormacyjneView(EventArgs e)
        {
            App.GlobalPrzesloId = 3;
            GetPrzesloPage();
        }
        void KulturalneView(EventArgs e)
        {
            App.GlobalPrzesloId = 4;
            GetPrzesloPage();
        }
        void MedialneView(EventArgs e)
        {
            
            App.GlobalPrzesloId = 5;
            GetPrzesloPage();
        }
        void GospodarczeView(EventArgs e)
        {
            
            App.GlobalPrzesloId = 6;
            GetPrzesloPage();
        }
        void SportoweView(EventArgs e)
        {
            
            App.GlobalPrzesloId = 7;
            GetPrzesloPage();
        }
        void DlaInnychView(EventArgs e)
        {
            
            App.GlobalPrzesloId = 8;
            GetPrzesloPage();
        }
        void TurystyczneView(EventArgs e)
        {
            
            App.GlobalPrzesloId = 9;
            GetPrzesloPage();
        }
        void MuzyczneView(EventArgs e)
        {
            
            App.GlobalPrzesloId = 10;
            GetPrzesloPage();
        }
    }
}