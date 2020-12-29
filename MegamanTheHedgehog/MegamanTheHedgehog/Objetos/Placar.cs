using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MegamanTheHedgehog.Objetos
{
    public class Placar
    {
        bool novoRecorde;

        Button btnIniciar;
        Label lblPontuacao, lblRecorde, lblFimDeJogo;

        public bool BotaoIniciarVisivel => btnIniciar.Visibility == Visibility.Visible;
        public bool TextoPontuacaoVisivel => lblPontuacao.Visibility == Visibility.Visible;
        public bool TextoRecordeVisivel=> lblRecorde.Visibility == Visibility.Visible;
        public bool TextoFimDeJogoVisivel => lblFimDeJogo.Visibility == Visibility.Visible;

        public int Pontuacao { get; private set; }
        public int Recorde { get; private set; }

        public string TextoPontuacao => "Pontuação: " + Pontuacao;
        public string TextoRecorde => "Recorde: " + Recorde;
        public string TextoFimDeJogo
        {
            get
            {
                if (novoRecorde)
                    return "Novo Recorde: " + Recorde;
                else
                    return "Pontuação: " + Pontuacao;
            }
        }

        public Placar(Button btnIniciar, Label lblPontuacao, Label lblRecorde, Label lblFimDeJogo)
        {
            this.btnIniciar = btnIniciar;
            this.lblPontuacao = lblPontuacao;
            this.lblRecorde = lblRecorde;
            this.lblFimDeJogo = lblFimDeJogo;

            this.lblFimDeJogo.Visibility = Visibility.Hidden;
        }

        public void Iniciar()
        {
            novoRecorde = false;
            Pontuacao = 0;
            lblRecorde.Content = TextoRecorde;
            lblPontuacao.Content = TextoPontuacao;
            lblFimDeJogo.Visibility = Visibility.Hidden;
            btnIniciar.Visibility = Visibility.Hidden;
        }

        public void Finalizar()
        {
            lblFimDeJogo.Content = TextoFimDeJogo;

            lblFimDeJogo.Visibility = Visibility.Visible;
            btnIniciar.Visibility = Visibility.Visible;
        }

        public void MarcarPontuacao(int pontos)
        {
            Pontuacao += pontos;
            if (Recorde <= Pontuacao)
            {
                novoRecorde = true;
                Recorde = Pontuacao;
            }
            lblRecorde.Content = TextoRecorde;
            lblPontuacao.Content = TextoPontuacao;
        }
    }
}
