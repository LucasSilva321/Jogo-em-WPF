using System;
using System.Windows.Controls;
using MegamanTheHedgehog.Enumeradores;
using MegamanTheHedgehog.Objetos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MegamanTheHedgehog.Tests.Objetos
{
    [TestClass]
    public class ObstaculoDireitaTests
    {
        ObstaculoDireita obstaculo;
        double larguraJanela = 1000;
        

        [TestInitialize]
        public void Setup()
        {
            var imagemObstaculo = new Image();
            imagemObstaculo.Width = larguraJanela / 10;
            obstaculo = new ObstaculoDireita(imagemObstaculo, larguraJanela);
        }

        [TestMethod]
        public void ObstaculoDireita_MoverHorizontalmenteInferiorAoLimite_MovimentoEmAndamento()
        {
            var movimentoEsperado = Movimento.EmAndamento;
            double deslocamento = larguraJanela / 10;
            var margemEsquerdaEsperada = obstaculo.MargemEsquerda - deslocamento;

            var movimento = obstaculo.MoverHorizontalmente(deslocamento);

            Assert.AreEqual(movimentoEsperado, movimento);
            Assert.AreEqual(margemEsquerdaEsperada, obstaculo.MargemEsquerda);
        }

        [TestMethod]
        public void ObstaculoDireita_MoverHorizontalmenteSuperiorAoLimite_MovimentoFinalizado()
        {
            var movimentoEsperado = Movimento.Finalizado;
            double deslocamento = larguraJanela * 2;
            var margemEsquerdaEsperada = obstaculo.MargemEsquerda;

            var movimento = obstaculo.MoverHorizontalmente(deslocamento);

            Assert.AreEqual(movimentoEsperado, movimento);
            Assert.AreEqual(margemEsquerdaEsperada, obstaculo.MargemEsquerda);
        }
    }
}
