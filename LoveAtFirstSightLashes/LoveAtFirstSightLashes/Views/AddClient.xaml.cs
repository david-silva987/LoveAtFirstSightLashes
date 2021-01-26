using LoveAtFirstSightLashes.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoveAtFirstSightLashes.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddClient : ContentPage
    {
        public AddClient()
        {
            InitializeComponent();
        }


        private async void AddNewClient()
        {

            string nbRDVSelected = rdvPicker.SelectedItem.ToString();
            await App.Database.SaveNewClient(new Client
            {
                Prenom = nameEntry.Text,
                DateBirth = dateEntry.Date.ToString(),
                NbRDV = Convert.ToInt32(nbRDVSelected),
            });
        }
        async void AddNewClient_Button(object sender, EventArgs e)
        {
            AddNewClient();
            await Navigation.PushAsync(new ItemsPage());


        }

    }
}