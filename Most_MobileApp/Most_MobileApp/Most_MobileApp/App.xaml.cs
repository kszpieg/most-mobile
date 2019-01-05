using Most_MobileApp.Models;
using Most_MobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Most_MobileApp
{
	public partial class App : Application
	{
        public static int chosenMenuOption = 0;
        public static CalendarEvent MostEvents;
        public static int GlobalPrzesloId = 0;
        public App ()
		{
			InitializeComponent();
            MainPage = new StartPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static CalendarEvent CalendarEvents
        {
            get
            {
                if (MostEvents == null)
                {
                    MostEvents = new CalendarEvent();
                }
                return MostEvents;
            }

        }
    }
}
