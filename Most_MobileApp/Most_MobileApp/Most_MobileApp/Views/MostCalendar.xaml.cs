using Most_MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Most_MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MostCalendar : ContentPage
	{
		public MostCalendar ()
		{
			InitializeComponent();
            Init();
            NavigationPage.SetHasNavigationBar(this, false);
            GetCalendarData();
        }

        void Init()
        {
            MostIcon.HeightRequest = Constants.LogoIconHeightInApp;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
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
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Application.Current.MainPage = new NavigationPage(new Construction());
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        //soon
                    }
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
                    DisplayAlert("", "Jesteś na Kalendarz", "OK");
                    break;
            }
        }

        string GetCalendarUrl()
        {
            string callId = "sda.most@gmail.com";
            string dateFormat = "T00%3A00%3A00%2B00%3A00";
            DateTime sundayDate = DateTime.Now;
            while (sundayDate.DayOfWeek != DayOfWeek.Sunday)
                sundayDate = sundayDate.AddDays(-1);
            string startDate = sundayDate.ToString("yyyy-MM-dd");
            string endDate = sundayDate.AddDays(7).ToString("yyyy-MM-dd");

            string CalendarUrl = "https://www.googleapis.com/calendar/v3/calendars/" + callId + "/events?key=AIzaSyCZRbmnwygYJgDriRG0MSeZH9tZr7l4DqQ&timeMin=" + startDate + dateFormat + "&timeMax=" + endDate + dateFormat + "&orderBy=startTime&singleEvents=true";

            return CalendarUrl;
        }

        async void GetCalendarData()
        {
            CalendarEvent temp_events = new CalendarEvent();
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(GetCalendarUrl())
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                temp_events = JsonConvert.DeserializeObject<CalendarEvent>(result);
                App.MostEvents = temp_events;
                DefaultView();
            }
            else
            {
                CalendarFrame.BackgroundColor = Color.FromHex("#3d5578");
                CalendarContent.Text += "Ups, coś poszło nie tak... \n\nPrawdopodobnie nie masz dostępu do internetu. Połącz się z internetem i otwórz Kalendarz jeszcze raz (czyli wybierz inny widok z menu, a potem wróć tutaj). \n\nJeśli problem nie zniknął, skontaktuj się ze mną: kszpieg@gmail.com";
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

        void PrintDayItems(List<Item> DayItems)
        {
            CalendarContent.Text = "";
            bool EventWithoutTime = false;
            foreach (Item itm in DayItems)
            {
                var StartDateToString = DateTime.Parse(itm.StartDate.StartDataTime.ToString("yyyy-MM-dd"));
                var EndDateToString = DateTime.Parse(itm.EndDate.EndDataTime.ToString("yyyy-MM-dd"));
                if (!EndDateToString.Equals(StartDateToString))
                {
                    CalendarContent.Text += itm.Summary + "\n\n";
                    EventWithoutTime = true;
                }
            }

            if(EventWithoutTime)
            {
                CalendarContent.Text += "__________________\n\n";
            }

            foreach(Item itm in DayItems)
            {
                var StartDateToString = DateTime.Parse(itm.StartDate.StartDataTime.ToString("yyyy-MM-dd"));
                var EndDateToString = DateTime.Parse(itm.EndDate.EndDataTime.ToString("yyyy-MM-dd"));
                if (EndDateToString.Equals(StartDateToString))
                {
                    CalendarContent.Text += itm.StartDate.StartDataTime.ToString("HH:mm") + " - " + itm.Summary + "\n\n";
                }
            }
            CalendarFrame.BackgroundColor = Color.FromHex("#3d5578");

            SundayButton.BackgroundColor = Color.Default;
            MondayButton.BackgroundColor = Color.Default;
            TuesdayButton.BackgroundColor = Color.Default;
            WednesdayButton.BackgroundColor = Color.Default;
            ThursdayButton.BackgroundColor = Color.Default;
            FridayButton.BackgroundColor = Color.Default;
            SaturdayButton.BackgroundColor = Color.Default;
        }

        void DefaultView()
        {
            DateTime TodayDate = DateTime.Now;
            var temp = App.MostEvents;
            List<Item> TodayItems = App.MostEvents.Items.FindAll(x => (x.EndDate.EndDataTime.Date >= TodayDate.Date && TodayDate.Date >= x.StartDate.StartDataTime.Date));
            PrintDayItems(TodayItems);
            switch (TodayDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    SundayButton.BackgroundColor = Color.FromHex("#364f70");
                    break;
                case DayOfWeek.Monday:
                    MondayButton.BackgroundColor = Color.FromHex("#364f70");
                    break;
                case DayOfWeek.Tuesday:
                    TuesdayButton.BackgroundColor = Color.FromHex("#364f70");
                    break;
                case DayOfWeek.Wednesday:
                    WednesdayButton.BackgroundColor = Color.FromHex("#364f70");
                    break;
                case DayOfWeek.Thursday:
                    ThursdayButton.BackgroundColor = Color.FromHex("#364f70");
                    break;
                case DayOfWeek.Friday:
                    FridayButton.BackgroundColor = Color.FromHex("#364f70");
                    break;
                case DayOfWeek.Saturday:
                    SaturdayButton.BackgroundColor = Color.FromHex("#364f70");
                    break;
            }
        }

        void SundayView(EventArgs e)
        {
            DateTime ThisDate = DateTime.Now;
            while (ThisDate.DayOfWeek != DayOfWeek.Sunday)
                ThisDate = ThisDate.AddDays(-1);
            List<Item> SundayItems = App.MostEvents.Items.FindAll(x => (x.EndDate.EndDataTime.Date >= ThisDate.Date && ThisDate.Date >= x.StartDate.StartDataTime.Date));
            PrintDayItems(SundayItems);
            SundayButton.BackgroundColor = Color.FromHex("#364f70");
        }

        void MondayView(EventArgs e)
        {
            DateTime ThisDate = DateTime.Now;
            if(ThisDate.DayOfWeek < DayOfWeek.Monday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Monday)
                    ThisDate = ThisDate.AddDays(1);
            }
            else if (ThisDate.DayOfWeek >= DayOfWeek.Monday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Monday)
                    ThisDate = ThisDate.AddDays(-1);
            }
            List<Item> MondayItems = App.MostEvents.Items.FindAll(x => (x.EndDate.EndDataTime.Date >= ThisDate.Date && ThisDate.Date >= x.StartDate.StartDataTime.Date));
            PrintDayItems(MondayItems);
            MondayButton.BackgroundColor = Color.FromHex("#364f70");
        }

        void TuesdayView(EventArgs e)
        {
            DateTime ThisDate = DateTime.Now;
            if (ThisDate.DayOfWeek < DayOfWeek.Tuesday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Tuesday)
                    ThisDate = ThisDate.AddDays(1);
            }
            else if (ThisDate.DayOfWeek >= DayOfWeek.Tuesday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Tuesday)
                    ThisDate = ThisDate.AddDays(-1);
            }
            List<Item> TuesdayItems = App.MostEvents.Items.FindAll(x => (x.EndDate.EndDataTime.Date >= ThisDate.Date && ThisDate.Date >= x.StartDate.StartDataTime.Date));
            PrintDayItems(TuesdayItems);
            TuesdayButton.BackgroundColor = Color.FromHex("#364f70");
        }

        void WednesdayView(EventArgs e)
        {
            DateTime ThisDate = DateTime.Now;
            if (ThisDate.DayOfWeek < DayOfWeek.Wednesday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Wednesday)
                    ThisDate = ThisDate.AddDays(1);
            }
            else if (ThisDate.DayOfWeek >= DayOfWeek.Wednesday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Wednesday)
                    ThisDate = ThisDate.AddDays(-1);
            }
            List<Item> WednesdayItems = App.MostEvents.Items.FindAll(x => (x.EndDate.EndDataTime.Date >= ThisDate.Date && ThisDate.Date >= x.StartDate.StartDataTime.Date));
            PrintDayItems(WednesdayItems);
            WednesdayButton.BackgroundColor = Color.FromHex("#364f70");
        }

        void ThursdayView(EventArgs e)
        {
            DateTime ThisDate = DateTime.Now;
            if (ThisDate.DayOfWeek < DayOfWeek.Thursday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Thursday)
                    ThisDate = ThisDate.AddDays(1);
            }
            else if (ThisDate.DayOfWeek >= DayOfWeek.Thursday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Thursday)
                    ThisDate = ThisDate.AddDays(-1);
            }
            List<Item> ThursdayItems = App.MostEvents.Items.FindAll(x => (x.EndDate.EndDataTime.Date >= ThisDate.Date && ThisDate.Date >= x.StartDate.StartDataTime.Date));
            PrintDayItems(ThursdayItems);
            ThursdayButton.BackgroundColor = Color.FromHex("#364f70");
        }

        void FridayView(EventArgs e)
        {
            DateTime ThisDate = DateTime.Now;
            if (ThisDate.DayOfWeek < DayOfWeek.Friday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Friday)
                    ThisDate = ThisDate.AddDays(1);
            }
            else if (ThisDate.DayOfWeek >= DayOfWeek.Friday)
            {
                while (ThisDate.DayOfWeek != DayOfWeek.Friday)
                    ThisDate = ThisDate.AddDays(-1);
            }
            List<Item> FridayItems = App.MostEvents.Items.FindAll(x => (x.EndDate.EndDataTime.Date >= ThisDate.Date && ThisDate.Date >= x.StartDate.StartDataTime.Date));
            PrintDayItems(FridayItems);
            FridayButton.BackgroundColor = Color.FromHex("#364f70");
        }

        void SaturdayView(EventArgs e)
        {
            DateTime ThisDate = DateTime.Now;
            while (ThisDate.DayOfWeek != DayOfWeek.Saturday)
                ThisDate = ThisDate.AddDays(1);
            List<Item> SaturdayItems = App.MostEvents.Items.FindAll(x => (x.EndDate.EndDataTime.Date >= ThisDate.Date && ThisDate.Date >= x.StartDate.StartDataTime.Date));
            PrintDayItems(SaturdayItems);
            SaturdayButton.BackgroundColor = Color.FromHex("#364f70");
        }
    }
}