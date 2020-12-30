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
    public class ObstaculoDireita : Obstaculo
    {
        public ObstaculoDireita(Image imagem) : base(imagem)
        {
        }

        public Movimento MoverHorizontalmente(double deslocamento, double larguraJanela)
        {
            if (Imagem.Margin.Left + Imagem.Width <= 0)
            {
                ReiniciarPosicao();
                return Movimento.Finalizado;
            }
            else
            {
                Imagem.Margin = new Thickness(Imagem.Margin.Left - deslocamento, Imagem.Margin.Top, 0, 0);
                return Movimento.EmAndamento;
            }
        }

    }
}
