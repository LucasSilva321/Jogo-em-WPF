using SonicTheHedgehog.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SonicTheHedgehog.Objetos
{
    public abstract class Obstaculo
    {
        protected Thickness PosicaoInicial { get;}
        protected Image Imagem { get; }

        public Obstaculo(Image imagem)
        {
            Imagem = imagem;
            PosicaoInicial = imagem.Margin;
        }

        public abstract Movimento Mover(double larguraJanela);

        public void ReiniciarPosicao()
        {
            Imagem.Margin = PosicaoInicial;
        }
    }
}
