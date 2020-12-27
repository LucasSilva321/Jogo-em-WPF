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
        int pontuacao = 0, recorde = 0;
        bool novoRecorde;

        Button btnIniciar;
        Label lblScore, lblRecord, lblFimDeJogo;

        public Placar(Button btnIniciar, Label lblScore, Label lblRecord, Label lblFimDeJogo)
        {
            this.btnIniciar = btnIniciar;
            this.lblScore = lblScore;
            this.lblRecord = lblRecord;
            this.lblFimDeJogo = lblFimDeJogo;

            this.lblFimDeJogo.Visibility = Visibility.Hidden;
        }

        public void ReiniciarPontuação()
        {
            novoRecorde = false;
            pontuacao = 0;
            lblRecord.Content = "Record: " + recorde;
            lblScore.Content = "Score: 0";
            lblFimDeJogo.Visibility = Visibility.Hidden;
            btnIniciar.Visibility = Visibility.Hidden;
        }

        public void Finalizar()
        {
            if (novoRecorde)
                lblFimDeJogo.Content = "New Record: " + recorde;
            else
                lblFimDeJogo.Content = "Score: " + pontuacao;

            lblFimDeJogo.Visibility = Visibility.Visible;
            btnIniciar.Visibility = Visibility.Visible;
        }

        public void MarcarPontuacao()
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
    }
}
