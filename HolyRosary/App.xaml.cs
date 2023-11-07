using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;

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
            Picker picker2 = MainPage.FindByName<Picker>("picker2");
            picker1.Items.Add("Хвалебныя таямніцы");
            picker1.Items.Add("Радасныя таямніцы");
            picker1.Items.Add("Балесныя таямніцы");
            picker1.Items.Add("Таямніцы святла"); 
            picker1.SelectedIndex = MysteryToDay();

            switch (picker1.SelectedIndex)
            {
                case 0: picker2.BackgroundColor = Color.OrangeRed; break;
                case 1: picker2.BackgroundColor = Color.Gold; ; break;
                case 2: picker2.BackgroundColor = Color.Magenta; break;
                case 3: picker2.BackgroundColor = Color.LightBlue; break;
            }

        }
            // BY
            

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Preferences.Set("Nexti",HolyRosary.MainPage.Nexti.ToString());
            Preferences.Set("ci", HolyRosary.MainPage.ci.ToString());
            Preferences.Set("d", HolyRosary.MainPage.d.ToString());
            Picker picker1 = MainPage.FindByName<Picker>("picker1");
            Picker picker2 = MainPage.FindByName<Picker>("picker2");
            Preferences.Set("Picker1", picker1.SelectedIndex.ToString());
            Preferences.Set("Picker2", picker2.SelectedIndex.ToString());
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            HolyRosary.MainPage.Nexti = int.Parse(Preferences.Get("Nexti", "0"));
            HolyRosary.MainPage.ci = int.Parse(Preferences.Get("ci", "0"));
            HolyRosary.MainPage.d = float.Parse(Preferences.Get("d", "0"));
            Picker picker1 = MainPage.FindByName<Picker>("picker1");
            Picker picker2 = MainPage.FindByName<Picker>("picker2");
            picker1.SelectedIndex = int.Parse(Preferences.Get("Picker1", "0"));
            picker2.SelectedIndex = int.Parse(Preferences.Get("Picker2", "0"));
            ImageButton img1 = MainPage.FindByName<ImageButton>("img1");
            if (HolyRosary.MainPage.Nexti > 7)
            {
                int pic1 = picker1.SelectedIndex + 1;
                int pic2 = picker2.SelectedIndex + 1;
                string filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                img1.Source = filename;
            }
            else {
                img1.Source = "img1.jpg";
            }
            img1.HorizontalOptions = LayoutOptions.FillAndExpand;
            img1.VerticalOptions = LayoutOptions.Start;
            img1.Aspect = Aspect.Fill;
        }
        public int MysteryToDay()
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
