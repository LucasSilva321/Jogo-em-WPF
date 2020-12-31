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
        Fase fase;
        Personagem personagem;

        public MainWindow()
        {
            InitializeComponent();

            var obstaculos = new ObstaculosFase(imgObstaculoUp, imgObstaculoRight, imgObstaculoLeft, Width);
            var placar = new Placar(btnIniciar, lblPontuacao, lblRecorde, lblFimDeJogo);
            var cenario = new Cenario(imgCenario, Width, Height);

            personagem = new Personagem(imgPersonagem);
            fase = new Fase(personagem, obstaculos, placar, cenario);
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            fase.Iniciar();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                personagem.Direcionar(Direcao.Direita);
            }
            if (e.Key == Key.Left)
            {
                personagem.Direcionar(Direcao.Esquerda);
            }

            if (e.Key == Key.Up && personagem.Pulando)
            {
                personagem.Direcionar(Direcao.Vertical);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            personagem.PararMovimento();
        }
    }
}
