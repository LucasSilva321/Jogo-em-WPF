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
        public ObstaculoEsquerda(Image imagem, double larguraJanela) : base(imagem)
        {
            PosicaoInicial = new Thickness(-imagem.Width, imagem.Margin.Top, larguraJanela, imagem.Margin.Bottom);
            Imagem.Margin = PosicaoInicial;
        }

        public Movimento MoverHorizontalmente(double deslocamento)
        {
            Imagem.Margin = new Thickness(Imagem.Margin.Left + deslocamento, Imagem.Margin.Top, 0, 0);

            if (Imagem.Margin.Left >= PosicaoInicial.Right)
            {
                ReiniciarPosicao();
                return Movimento.Finalizado;
            }
            else
            {
                return Movimento.EmAndamento;
            }
        }

    }
}
