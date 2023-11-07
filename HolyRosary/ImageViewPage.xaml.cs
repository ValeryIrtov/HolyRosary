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
    public partial class ImageViewPage : ContentPage
    {
        public ImageViewPage(string fileName, string ImageLabeltxt, string labelPrayText)
        {
            InitializeComponent();
            ImgViewButton.Source = fileName;
            ImageLabel.Text = ImageLabeltxt;
            LabelPray.Text = labelPrayText;
        }
        async void ButtonOKClicked(object b, EventArgs e)
        {
            
            await Navigation.PopModalAsync();
        }
    }
}