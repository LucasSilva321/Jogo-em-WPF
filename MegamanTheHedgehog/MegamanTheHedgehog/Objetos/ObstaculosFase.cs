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
    public class ObstaculosFase
    {
        public ObstaculoTopo Topo { get; }
        public ObstaculoDireita Direita { get; }
        public ObstaculoEsquerda Esquerda { get; }

        public ObstaculosFase(Image topo, Image direita, Image esquerda, double larguraJanela)
        {
            Topo = new ObstaculoTopo(topo);
            Direita = new ObstaculoDireita(direita, larguraJanela);
            Esquerda = new ObstaculoEsquerda(esquerda, larguraJanela);
        }

        public void ReiniciarPosicoes()
        {
            Topo.ReiniciarPosicao();
            Direita.ReiniciarPosicao();
            Esquerda.ReiniciarPosicao();
        }

        public List<Obstaculo> ToList()
        {
            return new List<Obstaculo> { Direita, Esquerda, Topo };
        }
    }
}
