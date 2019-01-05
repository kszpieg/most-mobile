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
	public partial class PrzesloPage : ContentPage
	{
        List<Kadra> data_chef;
        List<Przeslo> data_przeslo;
        List<Podprzeslo> data_podprzesla;
        public PrzesloPage ()
		{
			InitializeComponent ();
            Init();
            NavigationPage.SetHasNavigationBar(this, false);
            GetDataFromDatabase(App.GlobalPrzesloId);
        }

        void Init()
        {
            MostIcon.HeightRequest = Constants.LogoIconHeightInApp;
            Contact_chef.HeightRequest = Constants.ProfileIconHeight;
            Phone_chef.HeightRequest = Constants.ProfileIconHeight;
            Email_chef.HeightRequest = Constants.ProfileIconHeight;

            PrzesloFrame.BackgroundColor = Color.FromHex("#3d5578");
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

        void BackToConstruction(EventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new NavigationPage(new Construction());
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                //soon
            }
        }

        void GetDataFromDatabase(int GlobalPrzesloId)
        {
            SQLiteConnection dbConnection;
            dbConnection = DependencyService.Get<IDBInterface>().CreateConnection();

            data_przeslo = dbConnection.Query<Przeslo>("Select * From [Przesla] Where ID = " + GlobalPrzesloId);
            data_chef = dbConnection.Query<Kadra>("Select * From [Kadra] Where Przeslo_ID = " + GlobalPrzesloId);
            data_podprzesla = dbConnection.Query<Podprzeslo>("Select * From [Podprzesla] Where Przeslo_ID = " + GlobalPrzesloId);
            InfoFromDb();
        }

        void InfoFromDb()
        {
            Picture_chef.Source = data_chef[0].Photo;
            Chef_name.Text = data_chef[0].Name + " " + data_chef[0].Surname;
            Chef_phone.Text = data_chef[0].Phone;
            Chef_mail.Text = data_chef[0].E_mail;

            PrzesloName.Text = "Przęsło " + data_przeslo[0].Name;
            PrzesloDescription.Text = data_przeslo[0].Description;

            foreach(Podprzeslo podprzeslo in data_podprzesla)
            {
                PrzesloContent.Text += "- " + podprzeslo.Name + "\n\n";
            }
        }
    }
}