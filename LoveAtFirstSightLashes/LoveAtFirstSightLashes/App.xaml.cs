﻿using LoveAtFirstSightLashes.Data;
using LoveAtFirstSightLashes.Views;
using System;
using System.IO;
using Xamarin.Forms;

namespace LoveAtFirstSightLashes
{
    public partial class App : Application
    {
        static Database database;

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        /// <summary>
        /// Create database
        /// </summary>
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                }
                return database;
            }
        }

        public void ChangeScreen(Page page)
        {
            MainPage = page;
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
