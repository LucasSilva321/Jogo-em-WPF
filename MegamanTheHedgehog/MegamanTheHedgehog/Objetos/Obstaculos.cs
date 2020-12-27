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
    public class Obstaculos
    {
        public ObstaculoTopo Topo { get; }
        public ObstaculoDireita Direita { get; }
        public ObstaculoEsquerda Esquerda { get; }

        public Obstaculos(Image topo, Image direita, Image esquerda)
        {
            Topo = new ObstaculoTopo(topo);
            Direita = new ObstaculoDireita(direita);
            Esquerda = new ObstaculoEsquerda(esquerda);
        }

        public void ReiniciarPosicoes()
        {
            Topo.ReiniciarPosicao();
            Direita.ReiniciarPosicao();
            Esquerda.ReiniciarPosicao();
        }

        public Movimento Mover(Acao acao, double larguraJanela)
        {
            if (acao == Acao.MoverObstaculoDoTopo)
            {
                return Topo.Mover(larguraJanela);
            }
            else if (acao == Acao.MoverObstaculoDaDireita)
            {
                return Direita.Mover(larguraJanela);
            }
            else
            {
                return Esquerda.Mover(larguraJanela);
            }
        }
    }
}
