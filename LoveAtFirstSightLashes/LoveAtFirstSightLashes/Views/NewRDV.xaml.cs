using LoveAtFirstSightLashes.Interfaces;
using LoveAtFirstSightLashes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace LoveAtFirstSightLashes.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewRDV : ContentPage
    {
        public bool isAStudent;
        public DateTime DateNEW { get; set; }

        public NewRDV(DateTime dateChosen)
        {
            InitializeComponent();
            DateNEW = dateChosen;
            Console.WriteLine("NEWRDV");
            Console.WriteLine(DateNEW);

            dateEntry.Date = DateNEW;




        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<Client> list = await App.Database.GetAllClientsName();
            foreach (Client client in list)
            {
                Console.WriteLine(client.Prenom);
                listClients.Items.Add(client.Prenom);
            }

        }


        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void AddNewMeetingClicked(object sender, EventArgs e)
        {
            AddNewMeeting();


        }

        private async void AddNewMeeting()
        {

            if (IsClientChosen() && IsTypeChosen() && IsNotAtMidnight())
            {


                string nameClient = listClients.SelectedItem.ToString();
                Console.WriteLine(nameClient);
                int id = await App.Database.GetIdClient(nameClient);
                Console.WriteLine(id);

                DateTime date = dateEntry.Date;
                String timeChoose = TimePicker24.Time.ToString();
                CultureInfo ci = new CultureInfo("fr-FR");

                string dateFormatted = String.Format(ci, "{0:D}", date);

                await App.Database.SaveNewMeeting(new Meeting
                {
                    Name_Client = nameClient,

                    DateRDV = dateFormatted,
                    HourRDV = timeChoose.Substring(0, timeChoose.Length - 3),
                    TypePose = typePicker.SelectedItem.ToString(),
                    SheCame = false,
                });
                await Navigation.PushAsync(new ItemsPage());
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("Veuillez remplir tous les champs du formulaire"); //invalid message

            }

        }



        private bool IsClientChosen()
        {
            if (listClients.SelectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsTypeChosen()
        {
            if (typePicker.SelectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsNotAtMidnight()
        {

            String timeChoose = TimePicker24.Time.ToString();

            if (timeChoose.Substring(0, timeChoose.Length - 3) == "00:00")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
