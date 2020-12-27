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
    public class ObstaculoEsquerda : Obstaculo
    {
        public ObstaculoEsquerda(Image imagem) : base(imagem)
        {
        }

        public override Movimento Mover(double larguraJanela)
        {
            if (Imagem.Margin.Left >= larguraJanela)
            {
                ReiniciarPosicao();
                return Movimento.Finalizado;
            }
            else
            {
                Imagem.Margin = new Thickness(Imagem.Margin.Left + 80, Imagem.Margin.Top, 0, 0);
                return Movimento.EmAndamento;
            }
        }
    }
}
