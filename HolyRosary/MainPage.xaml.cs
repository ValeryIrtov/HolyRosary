﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Timers;
using Xamarin.Essentials;




namespace HolyRosary
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //SKBitmap bitmap;
        SKSurface surface, surface2;
        SKCanvas canvas, canvas2;
        public static int ci = 0; //счетчик бусинок
        bool NextPressed = false; //button Next is not pressing
        public static int Nexti = 0; //счетчик нажатий
        float cx = 0, cy = 0;  //canvas size
        public static float d = 0; // diametr
        //private static System.Timers.Timer prayTimer;
        public int[,] roll = new int[60, 3];
        public int currentsymbol = 0; //счетчик в тексте молитвы
        char[] tmp = new char[41]; //отображаемый текст молитвы
        public static int SliderValue = 50; // скорость бегущей строки
        public static int Languare = 1;  // 0 -RU 1-BY
        public int x2 = 0; int y2 = 0; //размеры поля бегущей строки
        


        static CancellationTokenSource cts = new CancellationTokenSource(); //для отмены асинхронного потока
        CancellationToken token = cts.Token;
        //кисть для основных бусинок
        SKPaint circleFill = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            Color = SKColors.LightSkyBlue
        };
        //кисть для 10-к
        SKPaint circleFill2 = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            Color = SKColors.CornflowerBlue
        };
        //кисть для выделенных элементов
        SKPaint circleFill3 = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            Color = SKColors.Yellow
        };

        
        static string pray1RU = "Во имя Отца и Сына и Святого Духа. Аминь. ";
        static string pray1BY = "У імя Айца і Сына, і Духа Святога. Амэн. ";
        static string pray1 = "";
        static string CurrentPray = ""; //текущий текст для отправки в просмотр  ImageViewPage
        static string pray2RU = "Я верю в Бога, Отца Всемогущего, Творца неба и земли. И в Иисуса Христа, единого Его Сына, Господа нашего, " +
            " который был зачат от Духа Святого, родился от Девы Марии, страдал при Понтии Пилате, был распят, умер и погребён; сошел в ад;" +
            " в третий день воскрес из мертвых, восшел на небеса и сидит одесную Бога, Отца Всемогущего, оттуда придет судить живых и мертвых." +
            " Верую в духа святого, святую католическую Церьковь, святых общение, оставление грехов, воскрешение плоти, жизнь вечную. Аминь.";
        static string pray2BY = "Веру ў Бога, Айца ўсемагутнага, Стварыцеля неба і зямлі, і ў Езуса Хрыста, Сына Яго адзінага, Пана нашага," +
            " які быў зачаты ад Духа Святога, нарадзіўся з Дзевы Марыі, замучаны пры Понцкім Пілаце, укрыжаваны, памёр і быў пахаваны;зышоў да " +
            " адхлані, на трэці дзень уваскрос з памерлых, узышоў на неба, сядзіць праваруч Бога Айца ўсемагутнага, адтуль прыдзе судзіць " +
            " жывых і памерлых. Веру ў Духа Святога, святы Касцёл каталіцкі, еднасць святых, адпушчэнне грахоў, уваскрашэнне цела, жыццё вечнае. Амэн. ";
        static string pray2 = "";

        static string pray3RU = "Отче наш,сущий на небесах! Да святится Имя Твое, да придет царство Твое, да будет воля Твоя как на небе, так и на земле; " +
            " хлеб наш насущный дай нам на сей день; и прости нам долги наши, как и мы прощаем должникам нашим; и не введи нас в искушение, но " +
            " избавь нас от лукавого. Аминь.";
        static string pray3BY = "Ойча наш, каторы ёсць у небе, свяціся імя Тваё, прыйдзі Валадарства Тваё, будзь воля Твая як у небе так і на зямлі. " +
            " Хлеба нашага штодзённага дай нам сёння, і адпусці нам правіны нашы, як і мы адпускаем вінаватым нашым, і не ўводзь нас у спакусу, але збаў" +
            " нас ад злога. Амэн.";
        static string pray3 = "";

        static string pray4RU = "Радуйся, Мария благодати полная, Господь с тобой. Благословенна Ты между женами и благословен плод чрева Твоего Иисус." +
            " Святая Мария, Матерь Божия, молись о нас, грешных, ныне и в час смерти нашей. Аминь.";
        static string pray4BY = "Вітай, Марыя, поўная ласкі, Пан з Табою, благаслаўлёная Ты між жанчынамі і благаслаўлёны плод улоння Твайго, Езус. " +
            " Святая Марыя, Маці Божая, маліся за нас грэшных, цяпер і ў хвіліну смерці нашай. Амэн.";
        static string pray4 = "";

        static string pray5RU = "Слава Отцу и Сыну, и Святому Духу, как было вначале,и ныне, и всегда, и во веки веков. Аминь.";
        static string pray5BY = "Хвала Айцу і Сыну і Духу Святому. Як было спрадвеку, цяпер і заўсёды, і на векі вечныя. Амэн.";
        static string pray5 = "";

        static string pray6RU = "О Мария, без первородного греха зачатая! Моли Бога о нас, к Тебе прибегающих.";
        static string pray6BY = "О Марыя, без граху першароднага зачатая, маліся за нас, хто ў Цябе паратунку шукае.";
        static string pray6 = "";

        static string pray7RU = "О мой Иисус, прости нам наши прегрешения, избавь нас от огня адского и приведи все души на небо, особенно те, которые " +
            " наиболее нуждаются в Твоем милосердии.";
        static string pray7BY = "О мой Езу, прабач нам грахі нашыя, захавай нас ад агню пякельнага, правядзі ўсе душы на неба і дапамажы асабліва тым, " +
            "каму найбольш патрэбна Твая міласэрнасць.";
        static string pray7 = "";

        static string pray8RU = "Под Твою защиту прибегаем, Пресвятая Богородица. Не презри молений наших в скорбях наших, но от всех опасностей " +
            "избавляй нас всегда, Дева преславная и благословенная. Владычица наша, Защитница наша, Заступница наша, Утешительница наша!" +
            " С Сыном Твоим примири нас, Сыну Твоему поручи нас, Сыну Твоему отдай нас.";
        static string pray8BY = "Тваёй абароне аддаёмся, Святая Багародзіца, у нашых патрэбах не пагарджай маленнем нашым. І ад усялякай злой прыгоды" +
            "выбаўляй нас заўсёды, Панна хвалебная і благаслаўлёная.    О Пані наша, Апякунка наша, Суцяшальніца наша, Ты заступніца наша, з Сынам " +
            "сваім нас паяднай, Сыну свайму нас даручай, Сыну свайму нас аддавай.";
        static string pray8 = "";

        static string quitBY = "РУЖАНЕЦ ЗАВЕРШАН";

        static string quitQuestBY = "Закрыць аплікацыю?";
        public static string quitApp = quitBY;
        static string quitRU = "РОЗАРИЙ ЗАВЕРШЁН";
        static string quitQuestRU = "Закрыть приложение?";
        public static string quitQuest = quitQuestBY;

        public MainPage()
        {
            //инициализация параметров из сохраненных настроек приложения
            Languare = int.Parse(Preferences.Get("Languare", "1"));
            SliderValue = int.Parse(Preferences.Get("SliderValue", "50"));
            // изициализируем массив точек - центр бусинок
            roll[0, 0] = 6; roll[0, 1] = 33; roll[0, 2] = 1;
            roll[1, 0] = 6; roll[1, 1] = 30; roll[1, 2] = 2;
            roll[2, 0] = 6; roll[2, 1] = 28; roll[2, 2] = 2;
            roll[3, 0] = 6; roll[3, 1] = 26; roll[3, 2] = 2;
            roll[4, 0] = 6; roll[4, 1] = 23; roll[4, 2] = 1;
            // 1
            roll[5, 0] = 3; roll[5, 1] = 22; roll[5, 2] = 2;
            roll[6, 0] = 3; roll[6, 1] = 20; roll[6, 2] = 2;
            roll[7, 0] = 3; roll[7, 1] = 18; roll[7, 2] = 2;
            roll[8, 0] = 3; roll[8, 1] = 16; roll[8, 2] = 2;
            roll[9, 0] = 3; roll[9, 1] = 14; roll[9, 2] = 2;
            roll[10, 0] = 3; roll[10, 1] = 12; roll[10, 2] = 2;
            roll[11, 0] = 3; roll[11, 1] = 10; roll[11, 2] = 2;
            roll[12, 0] = 3; roll[12, 1] = 8; roll[12, 2] = 2;
            roll[13, 0] = 3; roll[13, 1] = 6; roll[13, 2] = 2;
            roll[14, 0] = 3; roll[14, 1] = 4; roll[14, 2] = 2;
            //1-2
            roll[15, 0] = 6; roll[15, 1] = 3; roll[15, 2] = 1;
            //2
            roll[16, 0] = 9; roll[16, 1] = 3; roll[16, 2] = 2;
            roll[17, 0] = 11; roll[17, 1] = 3; roll[17, 2] = 2;
            roll[18, 0] = 13; roll[18, 1] = 3; roll[18, 2] = 2;
            roll[19, 0] = 15; roll[19, 1] = 3; roll[19, 2] = 2;
            roll[20, 0] = 17; roll[20, 1] = 3; roll[20, 2] = 2;
            roll[21, 0] = 19; roll[21, 1] = 3; roll[21, 2] = 2;
            roll[22, 0] = 21; roll[22, 1] = 4; roll[22, 2] = 2;
            roll[23, 0] = 21; roll[23, 1] = 6; roll[23, 2] = 2;
            roll[24, 0] = 21; roll[24, 1] = 8; roll[24, 2] = 2;
            roll[25, 0] = 21; roll[25, 1] = 10; roll[25, 2] = 2;
            //2-3
            roll[26, 0] = 21; roll[26, 1] = 13; roll[26, 2] = 1;
            //3
            roll[27, 0] = 21; roll[27, 1] = 16; roll[27, 2] = 2;
            roll[28, 0] = 21; roll[28, 1] = 18; roll[28, 2] = 2;
            roll[29, 0] = 21; roll[29, 1] = 20; roll[29, 2] = 2;
            roll[30, 0] = 21; roll[30, 1] = 22; roll[30, 2] = 2;
            roll[31, 0] = 21; roll[31, 1] = 24; roll[31, 2] = 2;
            roll[32, 0] = 21; roll[32, 1] = 26; roll[32, 2] = 2;
            roll[33, 0] = 21; roll[33, 1] = 28; roll[33, 2] = 2;
            roll[34, 0] = 21; roll[34, 1] = 30; roll[34, 2] = 2;
            roll[35, 0] = 21; roll[35, 1] = 32; roll[35, 2] = 2;
            roll[36, 0] = 21; roll[36, 1] = 34; roll[36, 2] = 2;
             
            roll[37, 0] = 19; roll[37, 1] = 36; roll[37, 2] = 1;
            ////4
            roll[38, 0] = 16; roll[38, 1] = 37; roll[38, 2] = 2;
            roll[39, 0] = 14; roll[39, 1] = 37; roll[39, 2] = 2;
            roll[40, 0] = 12; roll[40, 1] = 36; roll[40, 2] = 2;
            roll[41, 0] = 11; roll[41, 1] = 34; roll[41, 2] = 2;
            roll[42, 0] = 11; roll[42, 1] = 32; roll[42, 2] = 2;
            roll[43, 0] = 11; roll[43, 1] = 30; roll[43, 2] = 2;
            roll[44, 0] = 11; roll[44, 1] = 28; roll[44, 2] = 2;
            roll[45, 0] = 12; roll[45, 1] = 26; roll[45, 2] = 2;
            roll[46, 0] = 14; roll[46, 1] = 25; roll[46, 2] = 2;
            roll[47, 0] = 16; roll[47, 1] = 24; roll[47, 2] = 2;
            ////4-5
            roll[48, 0] = 17; roll[48, 1] = 21; roll[48, 2] = 1;
            ////5
            roll[49, 0] = 17; roll[49, 1] = 18; roll[49, 2] = 2;
            roll[50, 0] = 17; roll[50, 1] = 16; roll[50, 2] = 2;
            roll[51, 0] = 17; roll[51, 1] = 14; roll[51, 2] = 2;
            roll[52, 0] = 15; roll[52, 1] = 13; roll[52, 2] = 2;
            roll[53, 0] = 13; roll[53, 1] = 13; roll[53, 2] = 2;
            roll[54, 0] = 11; roll[54, 1] = 14; roll[54, 2] = 2;
            roll[55, 0] = 10; roll[55, 1] = 16; roll[55, 2] = 2;
            roll[56, 0] = 10; roll[56, 1] = 18; roll[56, 2] = 2;
            roll[57, 0] = 10; roll[57, 1] = 20; roll[57, 2] = 2;
            roll[58, 0] = 9; roll[58, 1] = 22; roll[58, 2] = 2;
            roll[59, 0] = 6; roll[59, 1] = 23; roll[59, 2] = 1;

            // BY
            if (Languare == 1)
            {
                
                pray1 = pray1BY;
                pray2 = pray2BY;
                pray3 = pray3BY;
                pray4 = pray4BY;
                pray5 = pray5BY;
                pray6 = pray6BY;
                pray7 = pray7BY;
                pray8 = pray8BY;
               
            }
            else //RU
            {
                pray1 = pray1RU;
                pray2 = pray2RU;
                pray3 = pray3RU;
                pray4 = pray4RU;
                pray5 = pray5RU;
                pray6 = pray6RU;
                pray7 = pray7RU;
                pray8 = pray8RU;
                }

            
            InitializeComponent();
            // Picker picker1 = this.FindByName<Picker>("picker1");
            

        }
        public void SetPrayLanguare(int LanguareP)
        {
            // сохранить выбранное таинство
            int currentTile = picker1.SelectedIndex;
            int currentmistery = picker2.SelectedIndex;
            picker1.Items.Clear();
            if (LanguareP == 1)
            {
                Header.Text = "СВЯТЫ РУЖАНЕЦ";
                pray1 = pray1BY;
                pray2 = pray2BY;
                pray3 = pray3BY;
                pray4 = pray4BY;
                pray5 = pray5BY;
                pray6 = pray6BY;
                pray7 = pray7BY;
                pray8 = pray8BY;
                quitApp = quitBY;
                quitQuest = quitQuestBY;
                picker1.Items.Add("Хвалебныя таямніцы");
                picker1.Items.Add("Радасныя таямніцы");
                picker1.Items.Add("Балесныя таямніцы");
                picker1.Items.Add("Таямніцы святла");
            }
            else
            {
                Header.Text = "СВЯТОЙ РОЗАРИЙ";
                pray1 = pray1RU;
                pray2 = pray2RU;
                pray3 = pray3RU;
                pray4 = pray4RU;
                pray5 = pray5RU;
                pray6 = pray6RU;
                pray7 = pray7RU;
                pray8 = pray8RU;
                quitApp = quitRU;
                quitQuest = quitQuestRU;
                picker1.Items.Add("Тайны славные");
                picker1.Items.Add("Тайны радостные");
                picker1.Items.Add("Тайны скорбные");
                picker1.Items.Add("Тайны светлые");
            }
            picker1.SelectedIndex = currentTile;
            picker2.SelectedIndex = currentmistery;
            
        }

        async void RunningLine(string pray, CancellationToken token)
        {
             
            int L = (int)(2.3 * x2 / y2); //подбираем число символов по размеру поля
           //
            for (int c = 0; c < L; c++)
            {
               pray = pray.Insert(c, " ");
            }
            pray = pray.Insert(L, " ");
            currentsymbol = 0;
            if (pray.Length <= L)
            {
                pray.CopyTo(currentsymbol, tmp, 0, pray.Length);
                canvasview2.InvalidateSurface();
            }
            else
            {
                    do
                    {
                        if (token.IsCancellationRequested) return;
                        pray.CopyTo(currentsymbol, tmp, 0, L);
                        canvasview2.InvalidateSurface();
                        await Task.Delay(SliderValue);
                        currentsymbol++;
                    }
                    while (currentsymbol + L <= pray.Length);
            }
        
        }
        private void OnPainting2(object sender, SKPaintSurfaceEventArgs e)
        {
            //Рисует текст в бегущей строке
            // получаем текущую поверхность из аргументов
            surface2 = e.Surface;
            // Получаем холст на котором будет рисовать
            canvas2 = surface2.Canvas;
            //var surfaceize = e.Info.Size;
            x2 = e.Info.Width;
            y2 = e.Info.Height;
            canvas2.Clear();
            var textPaint = new SKPaint
            {
                //IsAntialias = true,
                IsAntialias = false,
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black,
                TextSize = (float)(y2*0.8)
            };

            // Рисуем текст
            //canvas2.DrawText(new string(tmp), 10, y2-5, textPaint);
            
            canvas2.DrawText(new string(tmp), 0, (int)(y2-y2*0.1), textPaint);
           
        }
        private void Picker1_change(object sender, EventArgs e)
        {
            //удалить старые записи, если они есть
            while (picker2.Items.Count != 0)
            {
                picker2.Items.Clear();
            }
            if (picker1.SelectedIndex == 0) //тайны славные
            {
                picker2.BackgroundColor = Color.OrangeRed;
                switch (Languare)
                {
                    case 0:
                        {
                            picker2.Items.Add("Таинство первое. Воскресение");
                            picker2.Items.Add("Таинство второе. Вознесение");
                            picker2.Items.Add("Таинство третье. Сошествие Святого Духа");
                            picker2.Items.Add("Таинство четвертое. Успение");
                            picker2.Items.Add("Таинство пятое. Коронация");
                        } break;
                    case 1:
                        {
                            picker2.Items.Add("Таямніца першая. Змёртвыхпаўстанне Хрыста");
                            picker2.Items.Add("Таямніца другая. Унебаўшэсце Хрыста");
                            picker2.Items.Add("Таямніца трэцяя. Спасланне Духа Святога");
                            picker2.Items.Add("Таямніца чацьвёртая. Унебаўзяцце Найсвяцейшай Панны Марыі");
                            picker2.Items.Add("Таямніца пятая. Укаранаванне Найсвяцейшай Панны Марыі");
                        }break;

                }
               
            }
            else if (picker1.SelectedIndex == 1) //тайны радостные
            {
                picker2.BackgroundColor = Color.Gold; 
                switch (Languare)
                {
                    case 0:
                        {
                            picker2.Items.Add("Таинство первое. Благовествование");
                            picker2.Items.Add("Таинство второе. Посещение Елисаветы");
                            picker2.Items.Add("Таинство третье. Рождение Иисуса Христа");
                            picker2.Items.Add("Таинство четвертое. Сретение");
                            picker2.Items.Add("Таинство пятое. Обретение Иисуса в Храме");
                        }
                        break;
                    case 1:
                        {
                            picker2.Items.Add("Таямніца першая. Звеставанне Найсвяцейшай Панне Марыі");
                            picker2.Items.Add("Таямніца другая. Адведзіны Паннай Марыяй святой Альжбэты");
                            picker2.Items.Add("Таямніца трэцяя. Нараджэнне Збаўцы нашага, Езуса Хрыста");
                            picker2.Items.Add("Таямніца чацьвёртая. Ахвяраванне Хрыста ў Святыні");
                            picker2.Items.Add("Таямніца пятая. Езуса знаходзяць у Ерузалемскай святыні");
                        }
                        break;

                }
               
            }
            else if (picker1.SelectedIndex == 2) //тайны скорбные
            {
                picker2.BackgroundColor = Color.Magenta;
                switch (Languare)
                {
                    case 0:
                        {
                            picker2.Items.Add("Таинство первое. Борение в Гефсиманском саду");
                            picker2.Items.Add("Таинство второе. Бичевание в колоннаде");
                            picker2.Items.Add("Таинство третье. Возложение тернового венка");
                            picker2.Items.Add("Таинство четвертое. Крестный путь");
                            picker2.Items.Add("Таинство пятое. Распятие");
                        }
                        break;
                    case 1:
                        {
                            picker2.Items.Add("Таямніца першая. Малітва Езуса Хрыста ў садзе Аліўным");
                            picker2.Items.Add("Таямніца другая. Бічаванне Езуса");
                            picker2.Items.Add("Таямніца трэцяя. Укаранаванне Езуса цернем");
                            picker2.Items.Add("Таямніца чацьвёртая. Езус нясе крыж на Галгофу");
                            picker2.Items.Add("Таямніца пятая. Укрыжаванне і смерць Езуса на крыжы");
                        }
                        break;

                }
 
            }
            else  //тайны светлые
            {
                picker2.BackgroundColor = Color.LightBlue;
                switch (Languare)
                {
                    case 0:
                        {
                            picker2.Items.Add("Таинство первое. Крещение в Иордане");
                            picker2.Items.Add("Таинство второе. Свадьба в Канне");
                            picker2.Items.Add("Таинство третье. Нагорная проповедь");
                            picker2.Items.Add("Таинство четвертое. Преображение");
                            picker2.Items.Add("Таинство пятое. Установление Эвхористии");
                        }
                        break;
                    case 1:
                        {
                            picker2.Items.Add("Таямніца першая. Хрост Пана Езуса ў Ярдане");
                            picker2.Items.Add("Таямніца другая. Езус аб'яўляе Сябе на вяселлі ў Кане");
                            picker2.Items.Add("Таямніца трэцяя. Абвяшчэнне Валадарства Божага і заклік да навяртання");
                            picker2.Items.Add("Таямніца чацьвёртая. Перамяненне Пана на гары Табор");
                            picker2.Items.Add("Таямніца пятая. Устанаўленне Эўхарыстыі");
                        }
                        break;

                }

            }
            picker2.SelectedIndex = 0;
            if( Nexti < 8 ) picker2.IsEnabled = false;
        }
        private void Picker2_change(object sender, EventArgs e)
        {
            if (Nexti > 8)
            {
                switch (picker2.SelectedIndex)
                {
                    case 0: { Nexti = 8; ci = 4; } break;
                    case 1: { Nexti = 23; ci = 15; } break;
                    case 2: { Nexti = 38; ci = 26; } break;
                    case 3: { Nexti = 53; ci = 37; } break;
                    case 4: { Nexti = 68; ci = 48; } break;
                }
                canvasview.InvalidateSurface();
            }
        }
        private async void Button1_Clicked(object b, EventArgs e)
        { 
            
            cts.Cancel();
            NextPressed = true;
            int pic1, pic2;
            string filename;
            var APray3 = new[] { 9, 24, 39, 54, 69 };
            var APray5 = new[] { 7, 20, 35, 50, 65, 80, 84};
            var APray6 = new[] { 21, 36, 51, 66, 81};
            var APray7 = new[] { 22, 37, 52, 67, 82};
            //img1.HorizontalOptions = LayoutOptions.Center;
            //img1.VerticalOptions = LayoutOptions.Center;
            //imgButton1.Aspect = Aspect.AspectFit;
            if (Nexti == 8) picker2.IsEnabled = true;
            if (APray3.Contains(Nexti))
            {
                CurrentPray = pray3;
                cts = new CancellationTokenSource();
                token = cts.Token;
                RunningLine(pray3, token);
                canvasview.InvalidateSurface();
                Nexti++;
            }
            else if (APray5.Contains(Nexti))
            {
                CurrentPray = pray5;
                cts = new CancellationTokenSource();
                token = cts.Token;
                RunningLine(pray5, token);
                canvasview.InvalidateSurface();
                Nexti++;
            }
            else
                if (APray6.Contains(Nexti))
            {
                CurrentPray = pray6;
                cts = new CancellationTokenSource();
                token = cts.Token;
                RunningLine(pray6, token);
                canvasview.InvalidateSurface();
                Nexti++;
            }
            else if (APray7.Contains(Nexti))
            {
                CurrentPray = pray7;
                cts = new CancellationTokenSource();
                token = cts.Token;
                RunningLine(pray7, token);
                canvasview.InvalidateSurface();
                Nexti++;
            }
            else {
                switch (Nexti)
                {
                    case 1:
                        {
                            CurrentPray = pray1;
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            RunningLine(pray1, token);
                     //       var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                     //       player.Load("pray1.mp3");
                     //       player.Play();
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 2:
                        {
                            CurrentPray = pray2;
                            //Label1.Text = pray2;
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            RunningLine(pray2, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 3:
                        {
                            CurrentPray = pray3;
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            RunningLine(pray3, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 4:
                        {
                            CurrentPray = pray4;
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            RunningLine(pray4, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 5:
                        {
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            RunningLine(pray4, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 6:
                        {
                            CurrentPray = pray4;
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            RunningLine(pray4, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 8:
                        {
                            //BoxViev1.Color = Color.AliceBlue;
                            picker2.SelectedIndex = 0;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            imgButton1.Source = filename;
                            //img1.HorizontalOptions = LayoutOptions.FillAndExpand;
                            //img1.VerticalOptions = LayoutOptions.Start;
                            //img1.Aspect = Aspect.Fill;
                            imgButton1.Aspect = Aspect.AspectFit;
                            //замена бегущей строки пробелами
                            for (int n = 0; n < tmp.Length; n++) { tmp.SetValue(' ', n); }; 
                            canvasview2.InvalidateSurface();
                            CurrentPray = "";
                            //runningLine(pray3);
                            canvasview.InvalidateSurface();
                            Nexti++;

                        }
                        break;
                    case 23:
                        {
                            picker2.SelectedIndex = 1;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            imgButton1.Source = filename;
                            for (int n = 0; n < tmp.Length; n++) { tmp.SetValue(' ', n); };
                            canvasview2.InvalidateSurface();
                            canvasview.InvalidateSurface();
                            CurrentPray = "";
                            Nexti++;

                        }
                        break;
                    case 38:
                        {
                            picker2.SelectedIndex = 2;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            imgButton1.Source = filename;
                            for (int n = 0; n < tmp.Length; n++) { tmp.SetValue(' ', n); };
                            canvasview2.InvalidateSurface();
                            //runningLine(pray3);
                            canvasview.InvalidateSurface();
                            CurrentPray = "";
                            Nexti++;

                        }
                        break;
                    case 53:
                        {
                            picker2.SelectedIndex = 3;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            imgButton1.Source = filename;
                            for (int n = 0; n < tmp.Length; n++) { tmp.SetValue(' ', n); };
                            canvasview2.InvalidateSurface();
                            //img1.Source = "img14.jpg";
                            canvasview.InvalidateSurface();
                            CurrentPray = "";
                            Nexti++;
                        }
                        break;
                    case 68:
                        {
                            picker2.SelectedIndex = 4;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            imgButton1.Source = filename;
                            for (int n = 0; n < tmp.Length; n++) { tmp.SetValue(' ', n); };
                            canvasview2.InvalidateSurface();
                            canvasview.InvalidateSurface();
                            CurrentPray = "";
                            Nexti++;
                        }
                        break;
                    case 83:
                        {
                            CurrentPray = pray8;
                            imgButton1.Source = "pray8.jpg";
                            //cts.Cancel();
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            RunningLine(pray8, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;

                    default:
                        {
                            if (Nexti >= 85) //85 exit 
                            {
                                bool resquit = await DisplayAlert(quitApp, quitQuest, "Yes", "No");
                                if (resquit)
                                try
                                {
                                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                                }
                                catch (Exception errExit){ await DisplayAlert("Error", errExit.Message, "OK"); };
                            }
                            if (Nexti != 0 & Nexti != 8 & Nexti != 23 & Nexti != 38 & Nexti != 53 & Nexti != 68)
                            {
                                CurrentPray = pray4;
                                cts = new CancellationTokenSource();
                                token = cts.Token;
                                RunningLine(pray4, token);
                            }
                            canvasview.InvalidateSurface();
                            Nexti++;
                            
                        }
                        break;
                }
               
            }
                 
        }
        async  void SetingButton_Cliked(object b, EventArgs e)
        {
            int oldLanguare = Languare;
            SettingPage setPage = new SettingPage();
            setPage.OnCloseSetPage += SetPrayLanguare;
            await Navigation.PushModalAsync(setPage);
            if (oldLanguare != Languare)
                SetPrayLanguare(Languare);
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {

            //Здесь можно рисовать
           
            // получаем текущую поверхность из аргументов
            surface = e.Surface;
            // Получаем холст на котором будем рисовать
            canvas = surface.Canvas;    
            //var surfaceize = e.Info.Size;
            cx = e.Info.Width;
            cy = e.Info.Height;
           // get radius d witch first running
            if (Nexti == 0) //ci == 0) // Netx did not pressed
            {

                d = 1 + (int)cx / 25;
                canvas.Clear(SKColors.AliceBlue);
            }
            // there are drawing a rosary for something of start 
            // croc
            //кисть для медальона рисуем 2 раза
            if (Nexti != 0)
            {
                var pathCroc = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Gray,
                    StrokeWidth = 8
                };
                // Создаем путь
                var path = new SKPath();
                path.MoveTo(2 * d, 39 * d);
                path.LineTo(6 * d, 35 * d);
                path.MoveTo(4 * d, 35 * d);
                path.LineTo(6 * d, 37 * d);
                canvas.DrawPath(path, pathCroc);

                var pathCroc2 = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.CornflowerBlue,
                    StrokeWidth = 6
                };
                // Создаем путь
                var path2 = new SKPath();
                path2.MoveTo(2 * d, 39 * d);
                path2.LineTo(6 * d, 35 * d);
                path2.MoveTo(4 * d, 35 * d);
                path2.LineTo(6 * d, 37 * d);
                canvas.DrawPath(path2, pathCroc2);
                // end of croc
                // 
                var pathStroke = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.DarkBlue,
                    StrokeWidth = 3
                };
                // Рисуем путь розария
                var path1 = new SKPath();
                path1.MoveTo(6 * d, 35 * d);
                for (int pi = 0; pi <= 59; pi++)
                {
                    path1.LineTo(roll[pi, 0] * d, roll[pi, 1] * d);
                }
                canvas.DrawPath(path1, pathStroke);

                // рисуем бусинки
                for (int i = 0; i < 59; i++)
                {
                    if (roll[i, 2] == 2)
                        canvas.DrawCircle(roll[i, 0] * d, roll[i, 1] * d, d, circleFill);
                    else
                        canvas.DrawCircle(roll[i, 0] * d, roll[i, 1] * d, d + 1, circleFill2);
                }
            }

            if (NextPressed)
            { 
                if (Nexti == 2 || Nexti == 3 )
                {
                    //canvas.Save();
                    //рисуем контур медальона yellow
                    // Создаем путь
                    var pathCroc = new SKPaint
                    {
                        IsAntialias = true,
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Yellow,
                        StrokeWidth = 6
                    };
                    var path = new SKPath();
                    path.MoveTo(2 * d, 39 * d);
                    path.LineTo(6 * d, 35 * d);
                    path.MoveTo(4 * d, 35 * d);
                    path.LineTo(6 * d, 37 * d);
                    canvas.DrawPath(path, pathCroc);
                }
                  else if (Nexti !=1)
                  {
                      if ( ci < 60 )
                      {
                        // canvas.Restore();
                            if (ci > 0)
                            {
                                if (roll[ci - 1, 2] == 2) canvas.DrawOval(roll[ci - 1, 0] * d, roll[ci - 1, 1] * d, d, d, circleFill);
                                else canvas.DrawOval(roll[ci - 1, 0] * d, roll[ci - 1, 1] * d, d, d, circleFill2);
                            }
                            canvas.DrawOval(roll[ci, 0] * d, roll[ci, 1] * d, d, d, circleFill3); 
                        var Nexiarray = new[] { 3, 8, 9, 21, 22, 23, 24, 36, 37, 38, 39, 51, 52, 53, 54, 66, 67, 68, 69, 81, 82, 83, 84 };
                        // bool contains = Array.TrueForAll(Nexiarray, Nexti)
                        if (!Nexiarray.Contains(Nexti))//на больших бусинках ci не увеличиваем, 
                        {                              
                            ci++;
                        }
                        
                      }
                        
                  }

                
            }
           
            NextPressed = false;
            canvas.Save();
        }

        async void ImageButton_Clicked(object b, EventArgs e)
        {
            string filename2;
            //imgButton1.VerticalOptions = LayoutOptions.Start;
            //imgButton1.HorizontalOptions = LayoutOptions.StartAndExpand;
            //imgButton1.Aspect = Aspect.AspectFit;
            

            if (Nexti < 9)
            {
                filename2 = "img1.jpg";
                ImageViewPage ImagePage = new ImageViewPage(filename2, picker1.SelectedItem.ToString(),CurrentPray);
                await Navigation.PushModalAsync(ImagePage);
            }
            else
            if (Nexti < 84)
            {
                int pic1 = picker1.SelectedIndex + 1;
                int pic2 = picker2.SelectedIndex + 1;
                filename2 = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
               
                ImageViewPage ImagePage = new ImageViewPage(filename2, picker2.SelectedItem.ToString(), CurrentPray);
                await Navigation.PushModalAsync(ImagePage);
            }
            else
            {
                filename2 = "pray8.jpg";
                ImageViewPage ImagePage = new ImageViewPage(filename2, picker1.SelectedItem.ToString(), CurrentPray);
                await Navigation.PushModalAsync(ImagePage);
            }
            
            imgButton1.Source = filename2;
            imgButton1.VerticalOptions = LayoutOptions.Start;
            imgButton1.HorizontalOptions = LayoutOptions.StartAndExpand;
            imgButton1.Aspect = Aspect.AspectFit;
            //imgButton1.Scale = 1;
            //canvasview.InvalidateSurface();
        }
        void ImageButton_Pressed(object b, EventArgs e)
        {
            imgButton1.Scale = 1;
            imgButton1.VerticalOptions = LayoutOptions.Start;
            imgButton1.HorizontalOptions = LayoutOptions.Center;
            //imgButton1.HorizontalOptions = LayoutOptions.StartAndExpand;
            imgButton1.Aspect = Aspect.AspectFill;
            //imgButton1.Aspect = Aspect.AspectFit;

        }



    }

}
