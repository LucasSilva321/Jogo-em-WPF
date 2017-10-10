using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Jogo_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = 0;
        Storyboard UpDown, LeftRight, RightLeft, Pular;
        Random r = new Random();
        bool podePular = true;
        DispatcherTimer timer;
        double marginTopPersonagem, gravidade = -1.5;

        public MainWindow()
        {
            InitializeComponent();

            imgBackground1.Width = this.Width;
            imgBackground2.Width = this.Width;
            imgBackground1.Margin = new Thickness(0, 0, 0, 0);
            imgBackground2.Margin = new Thickness(-imgBackground2.Width, 0, 0, 0);

            UpDown = FindResource("UpDown") as Storyboard;
            LeftRight = FindResource("LeftRight") as Storyboard;
            RightLeft = FindResource("RightLeft") as Storyboard;
            Pular = FindResource("Pular") as Storyboard;

            UpDown.Completed += UpDown_Completed;
            LeftRight.Completed += LeftRight_Completed;
            RightLeft.Completed += RightLeft_Completed;
            Pular.Completed += Pular_Completed;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick;
            timer.Start();

            marginTopPersonagem = imgPersonagem.Margin.Top;
           

            MoverObstaculo();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!podePular)
            {
                imgPersonagem.Margin = new Thickness(imgPersonagem.Margin.Left, imgPersonagem.Margin.Top + (imgPersonagem.Height * gravidade), 0, 0);
                gravidade += 0.5;
                if(imgPersonagem.Margin.Top >= marginTopPersonagem)
                {
                    imgPersonagem.Margin = new Thickness(imgPersonagem.Margin.Left,marginTopPersonagem, 0, 0);
                    podePular = true;
                    gravidade = -1.5;
                }
            }
        }

        private void Pular_Completed(object sender, EventArgs e)
        {
            podePular = true;
        }

        private void RightLeft_Completed(object sender, EventArgs e)
        {
            MoverObstaculo();
        }

        private void LeftRight_Completed(object sender, EventArgs e)
        {
            MoverObstaculo();
        }

        private void UpDown_Completed(object sender, EventArgs e)
        {
            MoverObstaculo();
        }

        void MoverObstaculo()
        {
            int acao = r.Next(0, 3);
            double left = r.Next(0, (int)this.Width - (int)imgObstaculoUp.Width);
            if(acao == 0)
            {
                imgObstaculoUp.Margin = new Thickness(left, imgObstaculoUp.Margin.Top, 0, 0);
                UpDown.Begin();
            }
            if(acao == 1)
            {
                RightLeft.Begin();
            }
            if(acao == 2)
            {
                LeftRight.Begin();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {


                imgObstaculoUp.Margin = new Thickness(imgObstaculoUp.Margin.Left - 10, imgObstaculoUp.Margin.Top, 0, 0);
                imgBackground1.Margin = new Thickness(imgBackground1.Margin.Left - 10, 0, 0, 0);
                imgBackground2.Margin = new Thickness(imgBackground2.Margin.Left - 10, 0, 0, 0);
                imgPersonagem.Margin = new Thickness(imgPersonagem.Margin.Left+15, imgPersonagem.Margin.Top, 0, 0);
                if (imgBackground1.Margin.Left <= -imgBackground1.Width)
                    imgBackground1.Margin = new Thickness(imgBackground1.Width - 50, 0, 0, 0);
                if (imgBackground2.Margin.Left <= -imgBackground2.Width)
                    imgBackground2.Margin = new Thickness(imgBackground2.Width - 50, 0, 0, 0);
                if (imgPersonagem.Margin.Left >= this.Width -50)
                {
                    imgPersonagem.Margin = new Thickness(0, imgPersonagem.Margin.Top, 0, 0);
                }
                BitmapImage img = new BitmapImage(new Uri("Imagens/andando" + (i) + ".png", UriKind.RelativeOrAbsolute));
                imgPersonagem.Source = img;
                i = (i + 1) % 9;
                ScaleTransform x = new ScaleTransform(1, 1);
                imgPersonagem.RenderTransform = x;

            }
          if (e.Key == Key.Left)
            {
                imgObstaculoUp.Margin = new Thickness(imgObstaculoUp.Margin.Left + 10, imgObstaculoUp.Margin.Top, 0, 0);
                imgPersonagem.Margin = new Thickness(imgPersonagem.Margin.Left - 15, imgPersonagem.Margin.Top, 0, 0);
                imgBackground1.Margin = new Thickness(imgBackground1.Margin.Left + 10, 0, 0, 0);
                imgBackground2.Margin = new Thickness(imgBackground2.Margin.Left + 10, 0, 0, 0);
                
                if (imgPersonagem.Margin.Left <= -imgPersonagem.Width + 80)
                {
                    imgPersonagem.Margin = new Thickness(this.Width, imgPersonagem.Margin.Top, 0, 0);
                }
                if (imgBackground1.Margin.Left >= imgBackground1.Width)
                    imgBackground1.Margin = new Thickness(-imgBackground1.Width + 50, 0, 0, 0);
                if (imgBackground2.Margin.Left >= imgBackground2.Width)
                    imgBackground2.Margin = new Thickness(-imgBackground2.Width + 50, 0, 0, 0);
                
                BitmapImage img = new BitmapImage(new Uri("Imagens/andando" + (i) + ".png", UriKind.RelativeOrAbsolute));
               imgPersonagem.Source = img;
                i = (i + 1) % 9;
                ScaleTransform x = new ScaleTransform(-1, 1);
                imgPersonagem.RenderTransform = x;

            }

           if(e.Key == Key.Up && podePular)
            {
                podePular = false;
                BitmapImage img = new BitmapImage(new Uri("Imagens/parado.png", UriKind.RelativeOrAbsolute));
              //  imgPersonagem.Source = img;
               // Pular.Begin();
            }
        }
    }
}
