using Jogo_Wpf.Enumeradores;
using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace Jogo_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int imagemAtualIndex = 0;
        Random random = new Random();
        bool podePular = true;
        bool posicionarObstaculoUp = true;
        DispatcherTimer timer;
        double marginTopPersonagem, gravidade = -1.5;
        Acao acao;
        Thickness posObstaculoUp, posObstaculoRight, posObstaculLeft;
        bool moverDireita, moverEsquerda;
        int pontuacao = 0, recorde = 0;
        bool novoRecorde;
        SoundPlayer somdeFundo;


        public MainWindow()
        {
            InitializeComponent();

            posObstaculoUp = imgObstaculoUp.Margin;
            posObstaculoRight = imgObstaculoRight.Margin;
            posObstaculLeft = imgObstaculoLeft.Margin;
            marginTopPersonagem = imgPersonagem.Margin.Top;

            imgBackground1.Width = this.Width;
            imgBackground2.Width = this.Width;
            imgBackground1.Margin = new Thickness(0, 0, 0, 0);
            imgBackground2.Margin = new Thickness(-imgBackground2.Width, 0, 0, 0);

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick;

            lblFimDeJogo.Visibility = Visibility.Hidden;

            somdeFundo = new SoundPlayer();
        }

        void IniciarJogo()
        {
            TocarMusicaDeFundo();

            novoRecorde = false;
            pontuacao = 0;
            lblRecord.Content = "Record: " + recorde;
            lblScore.Content = "Score: 0";

            imgPersonagem.RenderTransform = new ScaleTransform(1, 1);
            imgPersonagem.Width = imgPersonagem.Height = 130;
            imgPersonagem.Margin = new Thickness(this.Width / 2, marginTopPersonagem, 0, 0);

            imgObstaculoUp.Margin = posObstaculoUp;
            imgObstaculoRight.Margin = posObstaculoRight;
            imgObstaculoLeft.Margin = posObstaculLeft;

            moverDireita = moverEsquerda = false;
            AtualizarAcaoDoObstaculo();

            button.Visibility = Visibility.Hidden;
            lblFimDeJogo.Visibility = Visibility.Hidden;

            timer.Start();
            MoverObstaculo();
        }

        private void TocarMusicaDeFundo()
        {
            //Ao criar o executavel, o arquivo de audio muda a sua localização
            try
            {
                somdeFundo.SoundLocation = "Audio/Robotnik.wav";
                somdeFundo.PlayLooping();
            }
            catch
            {

                somdeFundo.SoundLocation = "Robotnik.wav";
                somdeFundo.PlayLooping();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            moverEsquerda = moverDireita = false;
        }

        void MoverPersonagem()
        {
            if (moverDireita)
            {
                imgObstaculoUp.Margin = new Thickness(imgObstaculoUp.Margin.Left - 20, imgObstaculoUp.Margin.Top, 0, 0);
                imgBackground1.Margin = new Thickness(imgBackground1.Margin.Left - 20, 0, 0, 0);
                imgBackground2.Margin = new Thickness(imgBackground2.Margin.Left - 20, 0, 0, 0);
                imgPersonagem.Margin = new Thickness(imgPersonagem.Margin.Left + 30, imgPersonagem.Margin.Top, 0, 0);
                if (imgBackground1.Margin.Left <= -imgBackground1.Width)
                    imgBackground1.Margin = new Thickness(imgBackground1.Width - 80, 0, 0, 0);
                if (imgBackground2.Margin.Left <= -imgBackground2.Width)
                    imgBackground2.Margin = new Thickness(imgBackground2.Width - 80, 0, 0, 0);
                if (imgPersonagem.Margin.Left >= this.Width - 50)
                {
                    imgPersonagem.Margin = new Thickness(0, imgPersonagem.Margin.Top, 0, 0);
                }
                BitmapImage img = new BitmapImage(new Uri("Imagens/andando" + (imagemAtualIndex) + ".png", UriKind.RelativeOrAbsolute));
                imgPersonagem.Source = img;
                imagemAtualIndex = (imagemAtualIndex + 1) % 9;
                ScaleTransform x = new ScaleTransform(1, 1);
                imgPersonagem.RenderTransform = x;
            }
            else if (moverEsquerda)
            {
                imgObstaculoUp.Margin = new Thickness(imgObstaculoUp.Margin.Left + 20, imgObstaculoUp.Margin.Top, 0, 0);
                imgPersonagem.Margin = new Thickness(imgPersonagem.Margin.Left - 30, imgPersonagem.Margin.Top, 0, 0);
                imgBackground1.Margin = new Thickness(imgBackground1.Margin.Left + 20, 0, 0, 0);
                imgBackground2.Margin = new Thickness(imgBackground2.Margin.Left + 20, 0, 0, 0);

                if (imgPersonagem.Margin.Left <= -imgPersonagem.Width + 80)
                {
                    imgPersonagem.Margin = new Thickness(this.Width, imgPersonagem.Margin.Top, 0, 0);
                }
                if (imgBackground1.Margin.Left >= imgBackground1.Width)
                    imgBackground1.Margin = new Thickness(-imgBackground1.Width + 80, 0, 0, 0);
                if (imgBackground2.Margin.Left >= imgBackground2.Width)
                    imgBackground2.Margin = new Thickness(-imgBackground2.Width + 80, 0, 0, 0);

                BitmapImage img = new BitmapImage(new Uri("Imagens/andando" + (imagemAtualIndex) + ".png", UriKind.RelativeOrAbsolute));
                imgPersonagem.Source = img;
                imagemAtualIndex = (imagemAtualIndex + 1) % 9;
                ScaleTransform x = new ScaleTransform(-1, 1);
                imgPersonagem.RenderTransform = x;
            }

            if (!podePular)
            {
                imgPersonagem.Margin = new Thickness(imgPersonagem.Margin.Left, imgPersonagem.Margin.Top + (imgPersonagem.Height * gravidade), 0, 0);
                gravidade += 0.5;
                if (imgPersonagem.Margin.Top >= marginTopPersonagem)
                {
                    imgPersonagem.Margin = new Thickness(imgPersonagem.Margin.Left, marginTopPersonagem, 0, 0);
                    podePular = true;
                    gravidade = -1.5;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MoverPersonagem();
            MoverObstaculo();
            VerificarColisao();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            IniciarJogo();
        }

        void VerificarColisao()
        {
            double personagemLeft = imgPersonagem.Margin.Left;
            double personagemRight = personagemLeft + imgPersonagem.Width;
            double personagemTop = imgPersonagem.Margin.Top;
            double personagemBottom = personagemTop + imgPersonagem.Height;

            if (personagemTop <= imgObstaculoUp.Margin.Top + imgObstaculoUp.Height && personagemBottom >= imgObstaculoUp.Margin.Top)
            {
                if (personagemLeft <= imgObstaculoUp.Margin.Left + imgObstaculoUp.Width && personagemRight >= imgObstaculoUp.Margin.Left)
                {
                    FinalizarJogo();
                }
            }

            if (personagemBottom >= imgObstaculoRight.Margin.Top)
            {
                if (personagemLeft <= imgObstaculoRight.Margin.Left + imgObstaculoRight.Width && personagemRight >= imgObstaculoRight.Margin.Left)
                {
                    FinalizarJogo();
                }
                else if (personagemLeft <= imgObstaculoLeft.Margin.Left + imgObstaculoLeft.Width && personagemRight >= imgObstaculoLeft.Margin.Left)
                {
                    FinalizarJogo();
                }
            }


        }

        void FinalizarJogo()
        {
            imgPersonagem.RenderTransform = new ScaleTransform(1, -1);
            timer.Stop();
            button.Visibility = Visibility.Visible;



            if (novoRecorde)
                lblFimDeJogo.Content = "New Record: " + recorde;
            else
                lblFimDeJogo.Content = "Score: " + pontuacao;

            lblFimDeJogo.Visibility = Visibility.Visible;
            somdeFundo.Stop();
        }

        void MarcarPontuacao()
        {
            pontuacao += 100;
            if (recorde <= pontuacao)
            {
                novoRecorde = true;
                recorde = pontuacao;
            }
            lblRecord.Content = "Record: " + recorde;
            lblScore.Content = "Score: " + pontuacao;
        }

        void MoverObstaculo()
        {
            if (acao == Acao.MoverObstaculoDoTopo)
            {
                if (posicionarObstaculoUp)
                {
                    double left = random.Next(0, (int)this.Width - (int)imgObstaculoUp.Width);
                    imgObstaculoUp.Margin = new Thickness(left, imgObstaculoUp.Margin.Top, 0, 0);
                    posicionarObstaculoUp = false;
                }
                if (imgObstaculoUp.Margin.Top > this.Height)
                {
                    imgObstaculoUp.Margin = posObstaculoUp;
                    AtualizarAcaoDoObstaculo();
                    MarcarPontuacao();
                    posicionarObstaculoUp = true;
                    return;
                }
                imgObstaculoUp.Margin = new Thickness(imgObstaculoUp.Margin.Left, imgObstaculoUp.Margin.Top + 40, 0, 0);

            }
            if (acao == Acao.MoverObstaculoDaDireita)
            {

                if (imgObstaculoRight.Margin.Left + imgObstaculoRight.Width <= 0)
                {
                    imgObstaculoRight.Margin = posObstaculoRight;
                    AtualizarAcaoDoObstaculo();
                    MarcarPontuacao();
                    return;
                }
                imgObstaculoRight.Margin = new Thickness(imgObstaculoRight.Margin.Left - 80, imgObstaculoRight.Margin.Top, 0, 0);
            }
            if (acao == Acao.MoverObstaculoDaEsquerda)
            {

                if (imgObstaculoLeft.Margin.Left >= this.Width)
                {
                    imgObstaculoLeft.Margin = posObstaculLeft;
                    AtualizarAcaoDoObstaculo();
                    MarcarPontuacao();
                    return;
                }
                imgObstaculoLeft.Margin = new Thickness(imgObstaculoLeft.Margin.Left + 80, imgObstaculoLeft.Margin.Top, 0, 0);
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                moverDireita = true;
            }
            if (e.Key == Key.Left)
            {
                moverEsquerda = true;
            }

            if (e.Key == Key.Up && podePular)
            {
                podePular = false;
                BitmapImage img = new BitmapImage(new Uri("Imagens/parado.png", UriKind.RelativeOrAbsolute));
                //  imgPersonagem.Source = img;             
            }
        }

        private void AtualizarAcaoDoObstaculo()
        {
            acao = (Acao)random.Next(0, 3);
        }
    }
}
