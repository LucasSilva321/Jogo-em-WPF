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
        protected Thickness PosicaoInicial { get; set; }
        protected Image Imagem { get; }

        public double MargemEsquerda => Imagem.Margin.Left;
        public double MargemDireita => MargemEsquerda + Imagem.Width;
        public double MargemTopo => Imagem.Margin.Top;
        public double MargemInferior => MargemTopo + Imagem.Height;

        public Obstaculo(Image imagem)
        {
            Imagem = imagem;
            PosicaoInicial = imagem.Margin;
        }

        public bool TeveColisao(Personagem personagem)
        {
            if (personagem.MargemTopo <= MargemInferior && personagem.MargemInferior >= MargemTopo)
            {
                if (personagem.MargemEsquerda <= MargemDireita && personagem.MargemDireita >= MargemEsquerda)
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
