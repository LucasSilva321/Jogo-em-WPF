using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MegamanTheHedgehog.Objetos
{
    public class Background
    {
        Image imagemDireita, imagemEsquerda;

        public Background(Image imagemDireita, Image imagemEsquerda, double larguraJanela)
        {
            this.imagemDireita = imagemDireita;
            this.imagemEsquerda = imagemEsquerda;

            imagemEsquerda.Width = larguraJanela;
            imagemEsquerda.Margin = new Thickness(0, 0, 0, 0);
            imagemDireita.Width = larguraJanela;
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
