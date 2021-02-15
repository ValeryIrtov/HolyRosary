using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace HolyRosary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage(); 
           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Picker picker1 = MainPage.FindByName<Picker>("picker1");
            picker1.Items.Add("Тайны славные");
            picker1.Items.Add("Тайны радостные");
            picker1.Items.Add("Тайны скорбные");
            picker1.Items.Add("Тайны светлые");
            picker1.SelectedIndex = MysteryToDay();
            //DrawingRosaryFull();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private int MysteryToDay()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday ||
                    DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) return 0;
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Monday ||
                    DateTime.Now.DayOfWeek == DayOfWeek.Saturday) return 1;
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday ||
                    DateTime.Now.DayOfWeek == DayOfWeek.Friday) return 2;
            else return 3;
        }
       
         
                
    }
}
