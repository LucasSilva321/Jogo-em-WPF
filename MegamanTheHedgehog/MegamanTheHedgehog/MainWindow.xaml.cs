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
        Personagem personagem;
        ObstaculosPrimeiraFase obstaculos;
        Placar placar;
        Background background;

        DispatcherTimer timer;
        SoundPlayer somdeFundo;
        
        Acao acao;

        public MainWindow()
        {
            InitializeComponent();

            personagem = new Personagem(imgPersonagem);
            obstaculos = new ObstaculosPrimeiraFase(imgObstaculoUp, imgObstaculoRight, imgObstaculoLeft);
            placar = new Placar(btnIniciar, lblScore, lblRecord, lblFimDeJogo);
            background = new Background(imgBackgroundDireita, imgBackgroundEsquerda, Width);

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick;


            somdeFundo = new SoundPlayer();
        }

        void IniciarJogo()
        {
            placar.ReiniciarPontuação();
            personagem.ReiniciarPosicao(Width);
            obstaculos.ReiniciarPosicoes();

            TocarMusicaDeFundo();
            AtualizarAcaoDoObstaculo();

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

        void MoverPersonagem()
        {
            if (personagem.MoverDireita)
            {
                personagem.MoverParaDireita(this.Width);
                obstaculos.Topo.DeslocarParaEsqueda();
                background.DeslocarParaEsquerda();
            }
            else if (personagem.MoverEsquerda)
            {
                personagem.MoverParaEsquerda(this.Width);
                obstaculos.Topo.DeslocarParaDireita();          
                background.DeslocarParaDireita();
            }

            if (!personagem.Pulando)
            {
                personagem.PularOuCair();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            VerificarColisao();
            MoverPersonagem();
            MoverObstaculo();
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
            placar.Finalizar();
            somdeFundo.Stop();
        }

        void MoverObstaculo()
        {
            var movimento = obstaculos.Mover(acao, this.Width);

            if (movimento == Movimento.Finalizado)
            {
                AtualizarAcaoDoObstaculo();
                placar.MarcarPontuacao();
            }
        }

        private void AtualizarAcaoDoObstaculo()
        {
            var random = new Random();
            acao = (Acao)random.Next(0, 3);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            personagem.PararMovimento();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                personagem.DefinirDirecao(Direcao.Direita);
            }
            if (e.Key == Key.Left)
            {
                personagem.DefinirDirecao(Direcao.Esquerda);
            }

            if (e.Key == Key.Up && personagem.Pulando)
            {
                personagem.DefinirDirecao(Direcao.Vertical);
            }
        }
    }
}
