using System;
using MegamanTheHedgehog.Objetos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Controls;

namespace MegamanTheHedgehog.Tests.Objetos
{
    [TestClass]
    public class CenarioTests
    {
        Cenario cenario;
        double largura = 1000;
        
        [TestInitialize]
        public void Setup()
        {
            var altura = 500;
            cenario = new Cenario(new Image(), largura, altura);
        }

        [TestMethod]
        public void Cenario_DeslocamentoParaDireitaInferiorAoLimite_Deslocar()
        {
            var deslocamento = largura / 32;
            var centroEsperado = cenario.Centro + deslocamento;

            cenario.DeslocarParaDireita(deslocamento);

            Assert.AreEqual(centroEsperado, cenario.Centro);
        }

        [TestMethod]
        public void Cenario_DeslocamentoParaEsquerdaInferiorAoLimite_Deslocar()
        {
            var deslocamento = largura / 32;
            var centroEsperado = cenario.Centro - deslocamento;

            cenario.DeslocarParaEsquerda(deslocamento);

            Assert.AreEqual(centroEsperado, cenario.Centro);
        }

        [TestMethod]
        public void Cenario_DeslocamentoParaDireitaSuperiorAoLimite_Centralizar()
        {
            var deslocamento = largura * 2;
            var centroEsperado = cenario.Centro;

            cenario.DeslocarParaDireita(deslocamento);

            Assert.AreEqual(centroEsperado, cenario.Centro);
        }

        [TestMethod]
        public void Cenario_DeslocamentoParaEsquerdaSuperiorAoLimite_Centralizar()
        {
            var deslocamento = largura * 2;
            var centroEsperado = cenario.Centro;

            cenario.DeslocarParaEsquerda(deslocamento);

            Assert.AreEqual(centroEsperado, cenario.Centro);
        }

    }
}
