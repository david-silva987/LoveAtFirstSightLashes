using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LoveAtFirstSightLashes.Models;
using System.Globalization;

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
           foreach(Client client in list)
            {
                Console.WriteLine(client.Prenom);
                listClients.Items.Add(client.Prenom);
            }

            switchStudent.Toggled += switcher_Toggled;
        }

        private void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            if (!e.Value)
            {
                isAStudent = false;
            }
            else
            {
                isAStudent = true;
            }

            Console.WriteLine(isAStudent);
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void AddNewRDVClicked(object sender, EventArgs e)
        {
            AddNewClient();
            await Navigation.PushAsync(new ItemsPage());


        }

        private async void AddNewClient()
        {

            string nameClient = listClients.SelectedItem.ToString();
            Console.WriteLine(nameClient);
            //bool isToggled = switchStudent.Toggled; //verifier switch is toggled
            int id = await App.Database.GetIdClient(nameClient);
            Console.WriteLine(id);

            DateTime date = dateEntry.Date;
            String timeChoose = TimePicker24.Time.ToString();
            CultureInfo ci = new CultureInfo("fr-FR");

            string dateFormatted = String.Format(ci,"{0:D}", date);

              await App.Database.SaveNewMeeting(new Meeting
            {
                Name_Client = nameClient,

                DateRDV = dateFormatted,
                HourRDV = timeChoose.Substring(0, timeChoose.Length - 3),
                TypePose = typePicker.SelectedItem.ToString(),
                SheCame = false,
            });
        }
    }
}