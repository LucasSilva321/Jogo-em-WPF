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

        public override Movimento Mover(double larguraJanela)
        {
            if (podePosicionarObstaculoTopo)
            {
                double left = ObterPosicaoHorizontalAleatoria(larguraJanela);
                Imagem.Margin = new Thickness(left, Imagem.Margin.Top, 0, 0);
                podePosicionarObstaculoTopo = false;
            }
            else
            {
                Imagem.Margin = new Thickness(Imagem.Margin.Left, Imagem.Margin.Top + 40, 0, 0);
                if (Imagem.Margin.Top > larguraJanela)
                {
                    ReiniciarPosicao();
                    podePosicionarObstaculoTopo = true;
                    return Movimento.Finalizado;
                }
            }

            return Movimento.EmAndamento;
        }

        private int ObterPosicaoHorizontalAleatoria(double larguraJanela)
        {
            var random = new Random();
            var limiteHorizontal = (int)larguraJanela - (int)Imagem.Width;
            return random.Next(0, limiteHorizontal);
        }

        public void DeslocarParaEsqueda()
        {
            Imagem.Margin = new Thickness(Imagem.Margin.Left - 20, Imagem.Margin.Top, 0, 0);
        }

        public void DeslocarParaDireita()
        {
            Imagem.Margin = new Thickness(Imagem.Margin.Left + 20, Imagem.Margin.Top, 0, 0);
        }
    }
}
