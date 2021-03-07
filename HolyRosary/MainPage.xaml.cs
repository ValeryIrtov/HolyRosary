using System;
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
        int ci = 0; //счетчик бусинок
        bool NextPressed = false; //button Next is not pressing
        int Nexti = 0; //счетчик нажатий
        float cx = 0, cy = 0; //canvas size 
        float d = 0; // diametr
        private static System.Timers.Timer prayTimer;
        public int[,] roll = new int[60, 3];
        public int currentsymbol = 0; //счетчик в тексте молитвы
        char[] tmp = new char[41]; //отображаемый текст молитвы
       
        static CancellationTokenSource cts = new CancellationTokenSource();
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

        
        string pray1 = "    Во имя Отца и Сына и Святого Духа. Аминь.";
        string pray2 = "    Я верю в Бога, Отца Всемогущего, Творца неба и земли. И в Иисуса Христа, единого Его Сына, Господа нашего," +
            "который был зачат от Духа Святого, родился от Девы Марии, страдал при Понтии Пилате, был распят,умер и погребен; сошел в ад;" +
            " в третий день воскрес из мертвых, восшел на небеса и сидит одесную Бога, Отца Всемогущего, оттуда придет судить живых и мертвых." +
            " Верую в духа святого, святую католическую Церьковь, святых общение, оставление грехов, воскрешение плоти, жизнь вечную. Аминь.";
        string pray3 = "    Отче наш,сущий на небесах!Да святится Имя Твое, да придет царство Твое, да будет воля Твоя как на небе, так и на земле; " +
            "хлеб наш насущный дай нам на сей день; и прости нам долги наши, как и мы прощаем должникам нашим; и не введи нас в искушение, но " +
            "избавь нас от лукавого. Аминь.";
        string pray4 = "       Радуйся, Мария благодати полная; Господь с тобою! благословенна Ты между женами и благословен плод чрева Твоего Иисус. " +
            "Святая Мария, Матерь Божия, молись о нас грешных теперь и в час смерти нашей.Аминь.";
        string pray5 = "       Слава Отцу и Сыну, и Святому Духу, как было вначале,и ныне, и всегда, и во веки веков.Аминь.";
        string pray6 = "       О Мария, без первородного греха зачатая! Моли Бога о нас, к Тебе прибегающих.";
        string pray7 = "       О мой Иисус, прости нам наши прегрешения, избавь нас от огня адского и приведи все души на небо, особенно те, которые" +
            " наиболее нуждаются в Твоем милосердии.";
        string pray8 = "       Под Твою защиту прибегаем, Пресвятая Богородица. Не презри молений наших в скорбях наших, но от всех опасностей " +
            "избавляй нас всегда, Дева преславная и благословенная. Владычица наша, Защитница наша, Заступница наша, Утешительница наша!" +
            " С Сыном Твоим примири нас, Сыну Твоему поручи нас, Сыну Твоему отдай нас.";


        public MainPage()
        {
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

            //3-4
            roll[37, 0] = 21; roll[37, 1] = 37; roll[37, 2] = 1;
            ////4
            roll[38, 0] = 19; roll[38, 1] = 39; roll[38, 2] = 2;
            roll[39, 0] = 17; roll[39, 1] = 39; roll[39, 2] = 2;
            roll[40, 0] = 15; roll[40, 1] = 39; roll[40, 2] = 2;
            roll[41, 0] = 13; roll[41, 1] = 39; roll[41, 2] = 2;
            roll[42, 0] = 11; roll[42, 1] = 38; roll[42, 2] = 2;
            roll[43, 0] = 10; roll[43, 1] = 36; roll[43, 2] = 2;
            roll[44, 0] = 10; roll[44, 1] = 34; roll[44, 2] = 2;
            roll[45, 0] = 10; roll[45, 1] = 32; roll[45, 2] = 2;
            roll[46, 0] = 11; roll[46, 1] = 30; roll[46, 2] = 2;
            roll[47, 0] = 13; roll[47, 1] = 29; roll[47, 2] = 2;
            ////4-5
            roll[48, 0] = 16; roll[48, 1] = 29; roll[48, 2] = 1;
            ////5
            roll[49, 0] = 17; roll[49, 1] = 26; roll[49, 2] = 2;
            roll[50, 0] = 17; roll[50, 1] = 24; roll[50, 2] = 2;
            roll[51, 0] = 17; roll[51, 1] = 22; roll[51, 2] = 2;
            roll[52, 0] = 17; roll[52, 1] = 20; roll[52, 2] = 2;
            roll[53, 0] = 17; roll[53, 1] = 18; roll[53, 2] = 2;
            roll[54, 0] = 15; roll[54, 1] = 17; roll[54, 2] = 2;
            roll[55, 0] = 13; roll[55, 1] = 17; roll[55, 2] = 2;
            roll[56, 0] = 11; roll[56, 1] = 18; roll[56, 2] = 2;
            roll[57, 0] = 10; roll[57, 1] = 20; roll[57, 2] = 2;
            roll[58, 0] = 9; roll[58, 1] = 22; roll[58, 2] = 2;
            roll[59, 0] = 6; roll[59, 1] = 23; roll[59, 2] = 1;
            //
            InitializeComponent();
        }

        async void runningLine(string pray, CancellationToken token)
        {
             
            int L = 28;
            currentsymbol = 0;
            if (pray.Length <= L)
            {
                pray.CopyTo(currentsymbol, tmp, 0, pray.Length);
                canvasview2.InvalidateSurface();
            }
            else
            {
                //await Task.Run(() =>
                //{
                    do
                    {
                        if (token.IsCancellationRequested) return;
                        pray.CopyTo(currentsymbol, tmp, 0, L);
                        canvasview2.InvalidateSurface();
                        await Task.Delay(50);
                        currentsymbol++;
                    }
                    while (currentsymbol + L < pray.Length);
                //});

            }
        
        }
        private void OnPainting2(object sender, SKPaintSurfaceEventArgs e)
        {
            // получаем текущую поверхность из аргументов
            surface2 = e.Surface;
            // Получаем холст на котором будет рисовать
            canvas2 = surface2.Canvas;
            //var surfaceize = e.Info.Size;
            int x2 = e.Info.Width;
            int y2 = e.Info.Height;
            canvas2.Clear();
            var textPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black,
                TextSize = 30
            };

            // Рисуем текст
            canvas2.DrawText(new string(tmp), 10, y2-5, textPaint);
           
        }
        private void picker1_change(object sender, EventArgs e)
        {
            //удалить старые записи, если они есть
            while (picker2.Items.Count != 0)
            {
                picker2.Items.Clear();
            }
            if (picker1.SelectedIndex == 0) //тайны славные
            {
                picker2.Items.Add("Таинство первое. Воскресение");
                picker2.Items.Add("Таинство второе. Вознесение");
                picker2.Items.Add("Таинство третье. Сошествие Святого Духа");
                picker2.Items.Add("Таинство четвертое. Успение");
                picker2.Items.Add("Таинство пятое. Коронация");
            }
            else if (picker1.SelectedIndex == 1) //тайны радостные
            {
                picker2.Items.Add("Таинство первое. Благовествование");
                picker2.Items.Add("Таинство второе. Посещение Елисаветы");
                picker2.Items.Add("Таинство третье. Рождение Иисуса Христа");
                picker2.Items.Add("Таинство четвертое. Сретение");
                picker2.Items.Add("Таинство пятое. Обретение Иисуса в Храме");
            }
            else if (picker1.SelectedIndex == 2) //тайны скорбные
            {
                picker2.Items.Add("Таинство первое. Борение в Гефсиманском саду");
                picker2.Items.Add("Таинство второе. Бичевание в колоннаде");
                picker2.Items.Add("Таинство третье. Возложение тернового венка");
                picker2.Items.Add("Таинство четвертое. Крестный путь");
                picker2.Items.Add("Таинство пятое. Распятие");
            }
            else  //тайны светлые
            {
                picker2.Items.Add("Таинство первое. Крещение в Иордане");
                picker2.Items.Add("Таинство второе. Свадьба в Канне");
                picker2.Items.Add("Таинство третье. Нагорная проповедь");
                picker2.Items.Add("Таинство четвертое. Преображение");
                picker2.Items.Add("Таинство пятое. Установление Эвхористии");
            }
            picker2.SelectedIndex = 0;
        }
        private void Button1_Clicked(object b, EventArgs e)
        {
            
            cts.Cancel();
            NextPressed = true;
            int pic1, pic2;
            string filename;
            var APray3 = new[] { 9, 24, 39, 54, 69 };
            var APray5 = new[] { 7, 20, 35, 50, 65, 80};
            var APray6 = new[] { 21, 36, 51, 66, 81};
            var APray7 = new[] { 22, 37, 52, 67, 82};

            if ((APray3.Contains(Nexti)))
            {
                token = cts.Token;
                runningLine(pray3, token);
                canvasview.InvalidateSurface();
                Nexti++;
            }
            else if (APray5.Contains(Nexti))
            {
                token = cts.Token;
                runningLine(pray5, token);
                canvasview.InvalidateSurface();
                Nexti++;
            }
            else
                if (APray6.Contains(Nexti))
            {
                token = cts.Token;
                runningLine(pray6, token);
                canvasview.InvalidateSurface();
                Nexti++;
            }
            else if (APray7.Contains(Nexti))
            {
                token = cts.Token;
                runningLine(pray7, token);
                canvasview.InvalidateSurface();
                Nexti++;
            }
            else {
                switch (Nexti)
                {
                    case 1:
                        {
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            runningLine(pray1, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 2:
                        {
                            //Label1.Text = pray2;
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            runningLine(pray2, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 3:
                        {
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            runningLine(pray3, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 4:
                        {
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            runningLine(pray4, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 5:
                        {
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            runningLine(pray4, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 6:
                        {
                            cts = new CancellationTokenSource();
                            token = cts.Token;
                            runningLine(pray4, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    //case 7:
                    //    {
                    //        runningLine(pray5);
                    //        canvasview.InvalidateSurface();
                    //        Nexti++;
                    //    }
                    //    break;
                    case 8:
                        {
                            BoxViev1.Color = Color.AliceBlue;
                            picker2.SelectedIndex = 0;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            img1.Source = filename;
                            //runningLine(pray3);
                            canvasview.InvalidateSurface();
                            Nexti++;

                        }
                        break;
                    case 24:
                        {
                            picker2.SelectedIndex = 1;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            img1.Source = filename;
                            //cts.Cancel();
                           // runningLine(pray3, token);
                            canvasview.InvalidateSurface();
                            Nexti++;

                        }
                        break;
                    case 38:
                        {
                            picker2.SelectedIndex = 2;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            img1.Source = filename;
                            //runningLine(pray3);
                            canvasview.InvalidateSurface();
                            Nexti++;

                        }
                        break;
                    case 53:
                        {
                            picker2.SelectedIndex = 3;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            img1.Source = filename;
                            //img1.Source = "img14.jpg";
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 68:
                        {
                            picker2.SelectedIndex = 4;
                            pic1 = picker1.SelectedIndex + 1;
                            pic2 = picker2.SelectedIndex + 1;
                            filename = String.Concat("img", pic1.ToString(), pic2.ToString(), ".jpg");
                            img1.Source = filename;
                            //img1.Source = "img15.jpg";
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;
                    case 83:
                        {
                            //img1.Source = "img15.jpg";
                           //cts.Cancel();
                            runningLine(pray8, token);
                            canvasview.InvalidateSurface();
                            Nexti++;
                        }
                        break;

                    default:
                        {
                            if (Nexti != 0 & Nexti != 8 & Nexti != 23 & Nexti != 38 & Nexti != 53 & Nexti != 68)
                            {
                                //cts.Cancel();
                                cts = new CancellationTokenSource();
                                token = cts.Token;
                                runningLine(pray4, token);
                            }
                            canvasview.InvalidateSurface();
                            Nexti++;
                            
                        }
                        break;
                }
               
            }
                 
        }
        private void SetingButton_Cliked(object b, EventArgs e)
        {
            
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {

            //Здесь можно рисовать
           
                // получаем текущую поверхность из аргументов
                surface = e.Surface;
                // Получаем холст на котором будет рисовать
                canvas = surface.Canvas;    
                //var surfaceize = e.Info.Size;
                cx = e.Info.Width;
                cy = e.Info.Height;

            if (!NextPressed & ci == 0) // Netx did not press
            {
                if (ci == 0)
                {
                    d = 1 + (int)cx / 25;
                    //r = (int) d / 2;
                    // Очищаем холст
                    //canvas.Clear(SKColors.AliceBlue);
                    canvas.Clear(SKColors.AliceBlue);
                    //canvas.Save();
                }
                else canvas.Restore();
            }
            else
            if (Nexti == 1) //первый раз нажата Next
            {
                //canvas.Save();
                //кисть для медальона рисуем 2 раза

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
                //1
                var pathStroke = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.DarkBlue,
                    StrokeWidth = 3
                };
                var path1 = new SKPath();
                path1.MoveTo(6 * d, 35 * d);
                path1.LineTo(6 * d, 23 * d);
                path1.LineTo(6 * d, 23 * d);
                path1.LineTo(3 * d, 22 * d);
                path1.LineTo(3 * d, 4 * d);
                path1.LineTo(5 * d, 3 * d);
                path1.LineTo(20 * d, 3 * d);
                path1.LineTo(21 * d, 4 * d);
                path1.LineTo(21 * d, 37 * d);
                path1.LineTo(19 * d, 39 * d);
                path1.LineTo(12 * d, 39 * d);
                path1.LineTo(10 * d, 37 * d);
                path1.LineTo(10 * d, 31 * d);
                path1.LineTo(12 * d, 29 * d);
                path1.LineTo(16 * d, 29 * d);
                path1.LineTo(17 * d, 26 * d);
                path1.LineTo(17 * d, 18 * d);
                path1.LineTo(15 * d, 17 * d);
                path1.LineTo(13 * d, 17 * d);
                path1.LineTo(11 * d, 18 * d);
                path1.LineTo(9 * d, 22 * d);
                path1.LineTo(6 * d, 23 * d);
                // Рисуем путь
                canvas.DrawPath(path1, pathStroke);

                // 
                for (int i = 0; i < 59; i++)
                {
                    if (roll[i, 2] == 2)
                        canvas.DrawCircle(roll[i, 0] * d, roll[i, 1] * d, d, circleFill);
                    else
                        canvas.DrawCircle(roll[i, 0] * d, roll[i, 1] * d, d + 1, circleFill2);
                }
                //canvas.Save();
            }//END Nexti == 0
            else
            {
                if (Nexti == 2)
                {
                    //canvas.Save();
                    //рисуем контур медальона
                    // Создаем путь
                    var pathCroc = new SKPaint
                    {
                        IsAntialias = true,
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Yellow,
                        StrokeWidth = 10
                    };
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
                }
                else 
                {
                    if (ci < 60)
                    {
                        // canvas.Restore();
                        var Nexiarray = new[] { 3, 9, 10, 22, 23, 24, 25, 37, 38, 39, 40, 52, 53, 54, 55, 67, 68, 69, 70 };
                        // bool contains = Array.TrueForAll(Nexiarray, Nexti)
                        if (!Nexiarray.Contains(Nexti))//на больших бусинках ci не увеличиваем, 
                        {


                            if (ci > 0)
                            {
                                if (roll[ci - 1, 2] == 2) canvas.DrawOval(roll[ci - 1, 0] * d, roll[ci - 1, 1] * d, d, d, circleFill);
                                else canvas.DrawOval(roll[ci - 1, 0] * d, roll[ci - 1, 1] * d, d, d, circleFill2);
                            }
                                canvas.DrawOval(roll[ci, 0] * d, roll[ci, 1] * d, d, d, circleFill3);
                            ci++;
                        }
                    }
                        
                }

            }
            NextPressed = false;
            canvas.Save();
        }
       
    }
}
