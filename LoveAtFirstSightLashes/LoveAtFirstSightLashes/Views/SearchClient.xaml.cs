using LoveAtFirstSightLashes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoveAtFirstSightLashes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchClient : ContentPage
    {
        public SearchClient()
        {
            InitializeComponent();
            Details_client.IsVisible = false;
            Details_client.HeightRequest = 0;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if(listClients.Items.Count == 0)
            {
                List<Client> list = await App.Database.GetAllClientsName();
                foreach (Client client in list)
                {
                    Console.WriteLine(client.Prenom);
                    listClients.Items.Add(client.Prenom);

                }
            }

        }

        private async void listClients_SelectedIndexChanged(object sender, EventArgs e)
        {

            Details_client.IsVisible = true;
            Details_client.HeightRequest = 50;

            Client client = await App.Database.GetClient(listClients.SelectedItem.ToString());

            Name_Client.Text = client.Prenom;
            
            Date_Birth.Text = client.DateBirth.Substring(0,client.DateBirth.Length-8);
            NbRDV.Text = client.NbRDV.ToString();
        }
    }
}