using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Net.Mail;
using System.IO;

namespace HolyRosary
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        private const int V = 200;
        public delegate void SendLanguare(int L);
        public event SendLanguare onCloseSetPage;
        private int CurrentLanguare;


        public SettingPage()
        {
            InitializeComponent();
            sliderRunLine.Value = V - MainPage.SliderValue;
            //sliderRunLine.Value = V - int.Parse(Preferences.Get("SliderValue","50"));
            pickerLanguare.SelectedIndex = MainPage.Languare;         
            Label myMail = this.FindByName<Label>("myMail");
            CurrentLanguare = MainPage.Languare;
        }

        async void SendMailCommand(object sender, EventArgs e) //Написать разработчикам
        {
            bool result = await DisplayAlert("Адпраўка E-mail", "Напісаць распрацоўшчыкам", "Так", "Не");
            if (result)
            {
                try
                {
                    var message = new EmailMessage
                    {
                        Subject = "From App HolyRosary",
                        Body = "Hello!",
                        To = { "rosary.valery.irtov@gmail.com" }
                    };

                    await Email.ComposeAsync(message);
                }
                catch (Exception ex) 
                { await DisplayAlert("Error", "Памылка запуску пачтовай прграммы: " + ex.ToString(), "OK"); }
            }
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
                //Setting.Text = "Настройки";
                //RunningSrting.Text = "Бегущая строка";
                //SendMailButton.Text = "Разработка: rosary.valery.irtov@gmail.com";
               //MainPage.setPrayLanguare(pickerLanguare.SelectedIndex);
            }
            double value = sliderRunLine.Value;
            MainPage.SliderValue = V - (int)value;
            Preferences.Set("SliderValue", MainPage.SliderValue.ToString());
            await Navigation.PopModalAsync();

        }

        void OnLanguareChanged(object b, EventArgs e)
        {
            if (pickerLanguare.SelectedIndex != CurrentLanguare)
            {
                //изменился язык
                if (CurrentLanguare == 0)  // 0 ru
                {
                    Setting.Text = "Усталяванні";
                    RunningSrting.Text = "Бягучы радок";
                    SendMailButton.Text = "Распрацоўка: rosary.valery.irtov@gmail.com";
                    CurrentLanguare = 1;
                }
                else // 1 by
                {
                    Setting.Text = "Настройки";
                    RunningSrting.Text = "Бегущая строка";
                    SendMailButton.Text = "Разработка: rosary.valery.irtov@gmail.com";
                    CurrentLanguare = 0;
                }
            }
        }
    }
}