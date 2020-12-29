using System;
using System.Windows.Controls;
using MegamanTheHedgehog.Objetos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MegamanTheHedgehog.Tests.Objetos
{
    [TestClass]
    public class PlacarTests
    {
        Placar placar;

        [TestInitialize]
        public void Setup()
        {
            placar = new Placar(new Button(), new Label(), new Label(), new Label());
        }

        [TestMethod]
        public void Placar_Iniciar_ValoresEVisibilidadeDefault()
        {
            var pontuacaoEspearada = 0;
            var textoPontuacaoEsperado = "Pontuação: " + pontuacaoEspearada;

            placar.Iniciar();

            Assert.AreEqual(pontuacaoEspearada, placar.Pontuacao);
            Assert.AreEqual(textoPontuacaoEsperado, placar.TextoPontuacao);
            Assert.IsFalse(placar.BotaoIniciarVisivel);
            Assert.IsFalse(placar.TextoFimDeJogoVisivel);
        }

        [TestMethod]
        public void Placar_Finalizar_PontuacaoEVisibilidadeCorreta()
        {
            var pontuacaoEspearada = 300;
            var textoPontuacaoEsperado = "Pontuação: " + pontuacaoEspearada;

            placar.Iniciar();
            placar.MarcarPontuacao(100);
            placar.MarcarPontuacao(100);
            placar.MarcarPontuacao(100);
            placar.Finalizar();

            Assert.AreEqual(pontuacaoEspearada, placar.Pontuacao);
            Assert.AreEqual(textoPontuacaoEsperado, placar.TextoPontuacao);
            Assert.IsTrue(placar.BotaoIniciarVisivel);
            Assert.IsTrue(placar.TextoFimDeJogoVisivel);
        }

        [TestMethod]
        public void Placar_MarcarPontuacao_PontuacaoEVisibilidadeCorreta()
        {
            var pontuacaoEspearada = 100;
            var textoPontuacaoEsperado = "Pontuação: " + pontuacaoEspearada;

            placar.Iniciar();
            placar.MarcarPontuacao(100);

            Assert.AreEqual(pontuacaoEspearada, placar.Pontuacao);
            Assert.AreEqual(textoPontuacaoEsperado, placar.TextoPontuacao);
            Assert.IsFalse(placar.BotaoIniciarVisivel);
            Assert.IsFalse(placar.TextoFimDeJogoVisivel);
        }
    }
}
