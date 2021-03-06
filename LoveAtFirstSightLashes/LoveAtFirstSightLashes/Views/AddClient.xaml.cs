﻿using LoveAtFirstSightLashes.Interfaces;
using LoveAtFirstSightLashes.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace LoveAtFirstSightLashes.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddClient : ContentPage
    {

        protected override bool OnBackButtonPressed() => false; //disable back button on android device

        MainPage RootPage { get => Application.Current.MainPage as MainPage; }


        public AddClient()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Add a new client if form is valid
        /// </summary>
        private async void AddNewClient()
        {

            if (IsDateFilled() && IsNameFilled() && IsNbMeetingsFilled())
            {
                string nbRDVSelected = rdvPicker.SelectedItem.ToString();

                await App.Database.SaveNewClient(new Client
                {
                    Prenom = nameEntry.Text,
                    DateBirth = dateEntry.Date.ToString(),
                    NbRDV = Convert.ToInt32(nbRDVSelected),
                });
                // await Navigation.PushAsync(new ItemsPage());
                //((App)App.Current).ChangeScreen(new ItemsPage());
                await RootPage.NavigateFromMenu(0);

            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("Veuillez remplir tous les champs du formulaire"); //invalid message

            }

        }
        void AddNewClient_Button(object sender, EventArgs e)
        {
            AddNewClient();

        }

        private bool IsNameFilled()
        {
            return !string.IsNullOrEmpty(nameEntry.Text);
        }

        private bool IsDateFilled()
        {
            if (dateEntry.Date == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsNbMeetingsFilled()
        {
            if (rdvPicker.SelectedIndex == -1)
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