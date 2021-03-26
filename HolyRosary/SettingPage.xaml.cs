using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HolyRosary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        private const int V = 200;

        public SettingPage()
        {
            InitializeComponent();
            sliderRunLine.Value = V - MainPage.SliderValue;
            pickerLanguare.SelectedIndex = MainPage.Languare;
        }
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            MainPage.SliderValue = V - (int)value;
        }
        async void SetOKButton_Clicked(object b, EventArgs e)
        {
            if (pickerLanguare.SelectedIndex != MainPage.Languare)
            {
                MainPage.Languare = pickerLanguare.SelectedIndex;
                MainPage.setPrayLanguare(pickerLanguare.SelectedIndex);
            }
            double value = sliderRunLine.Value;
            MainPage.SliderValue = V - (int)value;
            await Navigation.PopModalAsync();

        }
    }
}