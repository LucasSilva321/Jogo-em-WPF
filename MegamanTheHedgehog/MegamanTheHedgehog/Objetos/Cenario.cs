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
        Image imagem;
        public double Largura { get; set; }
        public double Centro => imagem.Margin.Left + Largura;

        public Cenario(Image imagem, double largura)
        {
            this.imagem = imagem;
            Largura = largura;

            this.imagem.Width = largura * 2;

            AlinharImagens();
        }

        public void DeslocarParaDireita(double deslocamento)
        {
            imagem.Margin = new Thickness(imagem.Margin.Left + deslocamento, 0, 0, 0);

            if (Centro >= Largura)
                AlinharImagens();
        }

        public void DeslocarParaEsquerda(double deslocamento)
        {
            imagem.Margin = new Thickness(imagem.Margin.Left - deslocamento, 0, 0, 0);

            if (Centro <= 0)
                AlinharImagens();
        }

        void AlinharImagens()
        {
            imagem.Margin = new Thickness(-Largura / 2, 0, 0, 0);
        }
    }
}
