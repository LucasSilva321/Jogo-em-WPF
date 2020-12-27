using MegamanTheHedgehog.Enumeradores;
using MegamanTheHedgehog.Objetos;
using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace MegamanTheHedgehog
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        
        bool moverDireita, moverEsquerda;
        int pontuacao = 0, recorde = 0;
        bool novoRecorde;
        SoundPlayer somdeFundo;

        Acao acao;
        Obstaculos obstaculos;
        Background background;
        Personagem personagem;


        public MainWindow()
        {
            InitializeComponent();

            obstaculos = new Obstaculos(imgObstaculoUp, imgObstaculoUp, imgObstaculoLeft);
            background = new Background(imgBackgroundDireita, imgBackgroundEsquerda, Width);
            personagem = new Personagem(imgPersonagem);

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

            personagem.ReiniciarPosicao(Width);

            obstaculos.ReiniciarPosicoes();

            moverDireita = moverEsquerda = false;
            AtualizarAcaoDoObstaculo();

            btnIniciar.Visibility = Visibility.Hidden;
            lblFimDeJogo.Visibility = Visibility.Hidden;

            timer.Start();
            MoverObstaculo();
        }

        private void TocarMusicaDeFundo()
        {
            //Ao criar o executavel, o arquivo de audio muda de localização
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
                obstaculos.Topo.DeslocarParaEsqueda();
                background.DeslocarParaEsquerda();
                personagem.MoverParaDireita(this.Width);
            }
            else if (moverEsquerda)
            {
                obstaculos.Topo.DeslocarParaDireita();          
                background.DeslocarParaDireita();
                personagem.MoverParaEsquerda(this.Width);
            }

            if (!personagem.Pulando)
            {
                personagem.PularOuCair();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MoverPersonagem();
            MoverObstaculo();
            VerificarColisao();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
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
            btnIniciar.Visibility = Visibility.Visible;

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
            var movimento = obstaculos.Mover(acao, this.Width);

            if (movimento == Movimento.Finalizado)
            {
                AtualizarAcaoDoObstaculo();
                MarcarPontuacao();
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

            if (e.Key == Key.Up && personagem.Pulando)
            {
                personagem.Pulando = false;
            }
        }

        private void AtualizarAcaoDoObstaculo()
        {
            var random = new Random();
            acao = (Acao)random.Next(0, 3);
        }
    }
}
