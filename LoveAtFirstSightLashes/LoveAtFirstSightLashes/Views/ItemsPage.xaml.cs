using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LoveAtFirstSightLashes.Models;
using LoveAtFirstSightLashes.Views;
using System.Globalization;
using LoveAtFirstSightLashes.Interfaces;

namespace LoveAtFirstSightLashes.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {

        Client client = new Client();
        CultureInfo ci = new CultureInfo("fr-FR");


        public ItemsPage()
        {
            InitializeComponent();

        }



        async void AddItem_Clicked(object sender, EventArgs e)
        {
            DateTime date = dateEntry.Date;

            await Navigation.PushModalAsync(new NavigationPage(new NewRDV(date)));
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            DateTime today = DateTime.Today;

            string dateFormatted = String.Format(ci, "{0:D}", today);


            listViewAllMeetings.ItemsSource = await App.Database.GetAllMeetingsForToday(dateFormatted);
            foreach(Meeting mee in listViewAllMeetings.ItemsSource)
            { Console.WriteLine(mee.DateRDV); }
        }



        private async void dateEntry_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime date = dateEntry.Date;

            string dateFormatted = String.Format(ci, "{0:D}", date);
            Console.WriteLine(dateFormatted);
            listViewAllMeetings.ItemsSource = await App.Database.GetMeetingsForDay(dateFormatted);
            List<Meeting> list = listViewAllMeetings.ItemsSource as List<Meeting>;
            foreach (Meeting meeting in list)
            {
                Console.WriteLine("here");

                Console.WriteLine(meeting);
            }

            if (list.Count == 0)
            {
                noRDV.IsVisible = true;
                listViewAllMeetings.IsVisible = false;
            }
            else
            {
                noRDV.IsVisible = false;
                listViewAllMeetings.IsVisible = true;
            }
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {


            bool answer = await DisplayAlert("Action sur le rendez-vous", "Que souhaitez-vous faire ?", "Annuler", "Valider");
            var content = e.Item as Meeting;

            int nb = await App.Database.GetNbRDV(content.Name_Client);

            Console.WriteLine("Avant confirmation : " + nb);

            if (answer)
            {
                bool answer2 = await DisplayAlert("Confirmation d'annulation", "Voulez-vous vraiment supprimer le rendez-vous choisi ?", "Oui", "Non");

                if (answer2)
                {


                    await App.Database.RemoveMeeting(content.Name_Client,content.DateRDV,content.HourRDV);
                    DependencyService.Get<IMessage>().LongAlert("Rendez-vous supprimé avec succès");

                }
            }
            else if (!answer)
            {
                

                await App.Database.ConfirmMeeting(content.Name_Client, content.DateRDV,content.HourRDV);
                await App.Database.UpdateNBRDV(content.Name_Client);
               
                DependencyService.Get<IMessage>().LongAlert("Rendez-vous confirmé avec succès");


            }

            DateTime date = dateEntry.Date;
            CultureInfo ci = new CultureInfo("fr-FR");

            string dateFormatted = String.Format(ci, "{0:D}", date);
            listViewAllMeetings.ItemsSource = await App.Database.GetMeetingsForDay(dateFormatted);
            nb = await App.Database.GetNbRDV(content.Name_Client);
            Console.WriteLine("Après confirmation : " + nb);
            await DisplayAlert("Attention", content.Name_Client + " est actuellement à " +nb + "rendez-vous ", "OK");


        }

      
    }
}