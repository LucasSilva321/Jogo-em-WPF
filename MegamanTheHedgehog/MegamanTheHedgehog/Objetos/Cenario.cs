using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MegamanTheHedgehog.Objetos
{
    public class Cenario
    {
        Image imagemDireita, imagemEsquerda;
        public double Largura { get; set; }
        public double Centro => imagemEsquerda.Margin.Left + Largura;

        public Cenario(Image imagemDireita, Image imagemEsquerda, double largura)
        {
            this.imagemDireita = imagemDireita;
            this.imagemEsquerda = imagemEsquerda;
            Largura = largura;

            imagemEsquerda.Width = largura;
            imagemDireita.Width = largura;

            AlinharImagens();
        }

        public void DeslocarParaDireita(double deslocamento)
        {
            imagemEsquerda.Margin = new Thickness(imagemEsquerda.Margin.Left + deslocamento, 0, 0, 0);
            imagemDireita.Margin = new Thickness(imagemDireita.Margin.Left + deslocamento, 0, 0, 0);

            if (Centro >= (2 *Largura))
                AlinharImagens();
        }

        public void DeslocarParaEsquerda(double deslocamento)
        {
            imagemEsquerda.Margin = new Thickness(imagemEsquerda.Margin.Left - deslocamento, 0, 0, 0);
            imagemDireita.Margin = new Thickness(imagemDireita.Margin.Left - deslocamento, 0, 0, 0);

            if (Centro <= Largura)
                AlinharImagens();
        }

        void AlinharImagens()
        {
            imagemEsquerda.Margin = new Thickness(Largura / 2, 0, 0, 0);
            imagemDireita.Margin = new Thickness(-Largura / 2, 0, 0, 0);
        }
    }
}
