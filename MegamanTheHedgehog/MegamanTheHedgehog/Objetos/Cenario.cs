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

        public Cenario(Image imagemDireita, Image imagemEsquerda, double largura)
        {
            this.imagemDireita = imagemDireita;
            this.imagemEsquerda = imagemEsquerda;
            Largura = largura;

            imagemEsquerda.Width = largura;
            imagemEsquerda.Margin = new Thickness(0, 0, 0, 0);
            imagemDireita.Width = largura;
            imagemDireita.Margin = new Thickness(-imagemDireita.Width, 0, 0, 0);
        }

        public void DeslocarParaDireita()
        {
            imagemEsquerda.Margin = new Thickness(imagemEsquerda.Margin.Left + 20, 0, 0, 0);
            imagemDireita.Margin = new Thickness(imagemDireita.Margin.Left + 20, 0, 0, 0);

            if (imagemEsquerda.Margin.Left >= imagemEsquerda.Width)
                imagemEsquerda.Margin = new Thickness(-imagemEsquerda.Width + 80, 0, 0, 0);
            if (imagemDireita.Margin.Left >= imagemDireita.Width)
                imagemDireita.Margin = new Thickness(-imagemDireita.Width + 80, 0, 0, 0);
        }

        internal void DeslocarParaEsquerda()
        {
            imagemEsquerda.Margin = new Thickness(imagemEsquerda.Margin.Left - 20, 0, 0, 0);
            imagemDireita.Margin = new Thickness(imagemDireita.Margin.Left - 20, 0, 0, 0);

            if (imagemEsquerda.Margin.Left <= -imagemEsquerda.Width)
                imagemEsquerda.Margin = new Thickness(imagemEsquerda.Width - 80, 0, 0, 0);
            if (imagemDireita.Margin.Left <= -imagemDireita.Width)
                imagemDireita.Margin = new Thickness(imagemDireita.Width - 80, 0, 0, 0);
        }
    }
}
