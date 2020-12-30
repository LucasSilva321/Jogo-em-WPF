using System;
using System.Windows.Controls;
using MegamanTheHedgehog.Enumeradores;
using MegamanTheHedgehog.Objetos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MegamanTheHedgehog.Tests.Objetos
{
    [TestClass]
    public class ObstaculoTopoTests
    {
        ObstaculoTopo obstaculo;
        double alturaELargura = 1000;
        double deslocamento = 50;

        [TestInitialize]
        public void Setup()
        {
            var imagemObstaculo = new Image();
            imagemObstaculo.Width = alturaELargura / 10;
            obstaculo = new ObstaculoTopo(imagemObstaculo);
        }

        [TestMethod]
        public void ObstaculoTopo_MoverVerticalmente_TopoDeslocado()
        {
            var movimentoEsperado = Movimento.EmAndamento;

            var movimento =  obstaculo.MoverVerticalmente(deslocamento, alturaELargura, alturaELargura);

            Assert.AreEqual(movimentoEsperado, movimento);
            Assert.AreEqual(deslocamento, obstaculo.MargemTopo);
        }

        [TestMethod]
        public void ObstaculoTopo_DeslocarParaEsquerda_MargemHorizontalDeslocada()
        {
            var margemEsquerdaEsperada = -deslocamento;

            obstaculo.DeslocarParaEsqueda(deslocamento);

            Assert.AreEqual(margemEsquerdaEsperada, obstaculo.MargemEsquerda);
        }

        [TestMethod]
        public void ObstaculoTopo_DeslocarParaDireita_MargemHorizontalDeslocada()
        {
            obstaculo.DeslocarParaDireita(deslocamento);

            Assert.AreEqual(deslocamento, obstaculo.MargemEsquerda);
        }
    }
}
