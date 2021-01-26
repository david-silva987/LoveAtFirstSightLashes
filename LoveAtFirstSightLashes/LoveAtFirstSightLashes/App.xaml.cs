using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LoveAtFirstSightLashes.Services;
using LoveAtFirstSightLashes.Views;
using LoveAtFirstSightLashes.Data;
using System.IO;

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
