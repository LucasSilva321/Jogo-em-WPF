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
    public abstract class Obstaculo
    {
        protected Thickness PosicaoInicial { get; }
        protected Image Imagem { get; }

        public double Left => Imagem.Margin.Left;
        public double Right => Left + Imagem.Width;
        public double Top => Imagem.Margin.Top;
        public double Bottom => Top + Imagem.Height;

        public Obstaculo(Image imagem)
        {
            Imagem = imagem;
            PosicaoInicial = imagem.Margin;
        }

        public abstract Movimento Mover(double larguraJanela);

        public bool TeveColisao(Personagem personagem)
        {
            if (personagem.Top <= Bottom && personagem.Bottom >= Top)
            {
                if (personagem.Left <= Right && personagem.Right >= Left)
                {
                    return true;
                }
            }

            return false;
        }

        public void ReiniciarPosicao()
        {
            Imagem.Margin = PosicaoInicial;
        }
    }
}
