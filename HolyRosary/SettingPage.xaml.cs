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
        }
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            MainPage.SliderValue = V - (int)value;
        }
    }
}