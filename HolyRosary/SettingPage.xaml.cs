using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace HolyRosary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        private const int V = 200;
        public delegate void SendLanguare(int L);
        public event SendLanguare onCloseSetPage;
        public SettingPage()
        {
            InitializeComponent();
            //sliderRunLine.Value = V - MainPage.SliderValue;
            sliderRunLine.Value = V - int.Parse(Preferences.Get("SliderValue","50"));
            pickerLanguare.SelectedIndex = MainPage.Languare;
            
        }
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            MainPage.SliderValue = V - (int)value;
            Preferences.Set("SliderValue", MainPage.SliderValue.ToString()); 
        }
        async void SetOKButton_Clicked(object b, EventArgs e)
        {
            if (pickerLanguare.SelectedIndex != MainPage.Languare)
            {
                MainPage.Languare = pickerLanguare.SelectedIndex;
                Preferences.Get("Languare", MainPage.Languare.ToString());
                onCloseSetPage(pickerLanguare.SelectedIndex);
               //MainPage.setPrayLanguare(pickerLanguare.SelectedIndex);
            }
            double value = sliderRunLine.Value;
            MainPage.SliderValue = V - (int)value;
            Preferences.Set("SliderValue", MainPage.SliderValue.ToString());
            await Navigation.PopModalAsync();

        }
    }
}