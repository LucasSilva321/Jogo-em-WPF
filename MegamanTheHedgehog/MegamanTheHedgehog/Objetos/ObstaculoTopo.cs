using MegamanTheHedgehog.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MegamanTheHedgehog.Objetos
{
    public class ObstaculoTopo : Obstaculo
    {
        bool podePosicionarObstaculoTopo = true;

        public ObstaculoTopo(Image imagem) : base(imagem)
        {
        }

        public Movimento MoverVerticalmente(double deslocamento, double alturaJanela, double larguraJanela)
        {
            if (podePosicionarObstaculoTopo)
            {
                double left = ObterPosicaoHorizontalAleatoria(larguraJanela);
                podePosicionarObstaculoTopo = false;
            }

            Imagem.Margin = new Thickness(Imagem.Margin.Left, Imagem.Margin.Top + deslocamento, 0, 0);
            if (Imagem.Margin.Top > alturaJanela)
            {
                ReiniciarPosicao();
                podePosicionarObstaculoTopo = true;
                return Movimento.Finalizado;
            }

            return Movimento.EmAndamento;
        }

        public void DeslocarParaEsqueda(double deslocamento)
        {
            Imagem.Margin = new Thickness(Imagem.Margin.Left - deslocamento, Imagem.Margin.Top, 0, 0);
        }

        public void DeslocarParaDireita(double deslocamento)
        {
            Imagem.Margin = new Thickness(Imagem.Margin.Left + deslocamento, Imagem.Margin.Top, 0, 0);
        }

        private int ObterPosicaoHorizontalAleatoria(double larguraJanela)
        {
            var random = new Random();
            var limiteHorizontal = (int)larguraJanela - (int)Imagem.Width;
            return random.Next(0, limiteHorizontal);
        }

    }
}
