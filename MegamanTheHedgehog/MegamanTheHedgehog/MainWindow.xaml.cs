using MegamanTheHedgehog.Enumeradores;
using MegamanTheHedgehog.Objetos;
using System;
using System.Collections.Generic;
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
        ObstaculosPrimeiraFase obstaculos;
        Background background;
        Personagem personagem;

        DispatcherTimer timer;
        SoundPlayer somdeFundo;
        
        Acao acao;
        bool moverDireita, moverEsquerda;
        int pontuacao = 0, recorde = 0;
        bool novoRecorde;

        public MainWindow()
        {
            InitializeComponent();

            obstaculos = new ObstaculosPrimeiraFase(imgObstaculoUp, imgObstaculoUp, imgObstaculoLeft);
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
            ReiniciarPontuação();

            personagem.ReiniciarPosicao(Width);
            obstaculos.ReiniciarPosicoes();
            AtualizarAcaoDoObstaculo();

            moverDireita = moverEsquerda = false;

            btnIniciar.Visibility = Visibility.Hidden;
            lblFimDeJogo.Visibility = Visibility.Hidden;

            timer.Start();
            MoverObstaculo();
        }

        private void ReiniciarPontuação()
        {
            novoRecorde = false;
            pontuacao = 0;
            lblRecord.Content = "Record: " + recorde;
            lblScore.Content = "Score: 0";
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
            if (personagem.TeveColisao(obstaculos.ToList())){
                FinalizarJogo();
            }
        }

        void FinalizarJogo()
        {
            personagem.Parar();
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

        private void AtualizarAcaoDoObstaculo()
        {
            var random = new Random();
            acao = (Acao)random.Next(0, 3);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            moverEsquerda = moverDireita = false;
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
    }
}
