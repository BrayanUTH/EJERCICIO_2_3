using EJERCICIO_2_3.Controller;
using EJERCICIO_2_3.View;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EJERCICIO_2_3
{
    public partial class App : Application
    {
        static Database db;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PagePrincipal());
        }

        public static Database DBase
        {
            get
            {
                if (db == null)
                {
                    string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "audio.db3");
                    db = new Database(folderPath);
                }

                return db;
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
