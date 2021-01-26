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
using LoveAtFirstSightLashes.ViewModels;
using System.Collections.ObjectModel;
using System.Globalization;

namespace LoveAtFirstSightLashes.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

 



        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();



        }


        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            DateTime date = dateEntry.Date;

            await Navigation.PushModalAsync(new NavigationPage(new NewRDV(date)));
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;

           
           
            listViewAllMeetings.ItemsSource = await App.Database.GetAllMeetings();
            foreach(Meeting mee in listViewAllMeetings.ItemsSource)
            { Console.WriteLine(mee.DateRDV); }



        }


        async void ValidateRDV_Clicked(object sender, EventArgs e)
        {
          
        }
        async void SearchRDV(object sender, EventArgs e)
        {
            DateTime date = dateEntry.Date;
            CultureInfo ci = new CultureInfo("fr-FR");

            string dateFormatted = String.Format(ci, "{0:D}", date);
            Console.WriteLine(dateFormatted);
            listViewAllMeetings.ItemsSource = await App.Database.GetMeetingsForDay(dateFormatted);
            List<Meeting> list = listViewAllMeetings.ItemsSource as List<Meeting>;
            foreach (Meeting meeting in list)
            {
                Console.WriteLine("here");

                Console.WriteLine(meeting);
            }

           if(list.Count==0)
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
    }
}