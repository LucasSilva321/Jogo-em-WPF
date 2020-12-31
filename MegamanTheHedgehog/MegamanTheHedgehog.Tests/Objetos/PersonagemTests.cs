using System;
using System.Collections.Generic;
using System.Windows.Controls;
using MegamanTheHedgehog.Enumeradores;
using MegamanTheHedgehog.Objetos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MegamanTheHedgehog.Tests.Objetos
{
    [TestClass]
    public class PersonagemTests
    {
        double larguraJanela = 1000;
        Personagem personagem;

        [TestInitialize]
        public void Setup()
        {
            var imagemPersonagem = new Image();
            imagemPersonagem.Width = imagemPersonagem.Height = larguraJanela / 10;
            personagem = new Personagem(imagemPersonagem);
        }

        [TestMethod]
        public void Personagem_ReiniciarPosicao_ImagemCentralizada()
        {
            var margemEsquerdaEsperada = larguraJanela / 2;

            personagem.ReiniciarPosicao(larguraJanela);

            Assert.AreEqual(margemEsquerdaEsperada, personagem.MargemEsquerda);
        }

        [TestMethod]
        public void Personagem_MoverParaDireitaInferiorAoLimite_ImagemMovimentada()
        {
            personagem.ReiniciarPosicao(larguraJanela);
            var deslocamento = larguraJanela / 10;
            var margemEsquerdaEsperada = personagem.MargemEsquerda + deslocamento;

            personagem.ReiniciarPosicao(larguraJanela);
            personagem.MoverParaDireita(deslocamento, larguraJanela);

            Assert.AreEqual(margemEsquerdaEsperada, personagem.MargemEsquerda);
        }

        [TestMethod]
        public void Personagem_MoverParaDireitaSuperiorAoLimite_MargemDireitaLimite()
        {
            personagem.ReiniciarPosicao(larguraJanela);
            var deslocamento = larguraJanela * 2;
            var margemEsquerdaEsperada = larguraJanela;

            personagem.MoverParaDireita(deslocamento, larguraJanela);

            Assert.AreEqual(margemEsquerdaEsperada, personagem.MargemEsquerda);
        }

        [TestMethod]
        public void Personagem_MoverParaEsquerdaInferiorAoLimite_ImagemMovimentada()
        {
            personagem.ReiniciarPosicao(larguraJanela);
            var deslocamento = larguraJanela / 10;
            var margemEsquerdaEsperada = personagem.MargemEsquerda - deslocamento;

            personagem.MoverParaEsquerda(deslocamento, larguraJanela);

            Assert.AreEqual(margemEsquerdaEsperada, personagem.MargemEsquerda);
        }

        [TestMethod]
        public void Personagem_MoverParaEsquerdaSuperiorAoLimite_MargemEsquedaLimite()
        {
            personagem.ReiniciarPosicao(larguraJanela);
            var deslocamento = larguraJanela * 2;
            var margemEsquerdaEsperada = 0;

            personagem.MoverParaEsquerda(deslocamento, larguraJanela);

            Assert.AreEqual(margemEsquerdaEsperada, personagem.MargemEsquerda);
        }

        [TestMethod]
        public void Personagem_PularPrimeiraVez_MargemTopoDiminui()
        {
            var margemTopoAntes = personagem.MargemTopo;

            personagem.PularOuCair();

            Assert.IsTrue(personagem.MargemTopo < margemTopoAntes);
        }

        [TestMethod]
        public void Personagem_ObstaculoTeveColisao_CondicaoValida()
        {
            var imagemObstaculo = new Image();
            imagemObstaculo.Width = imagemObstaculo.Height = larguraJanela / 5;
            var obstaculo = new ObstaculoDireita(imagemObstaculo, larguraJanela);
            personagem.ReiniciarPosicao(larguraJanela);

            obstaculo.MoverHorizontalmente(obstaculo.MargemEsquerda - personagem.MargemEsquerda);
            var listaObstaculos = new List<Obstaculo>() { obstaculo };
            var teveColisao = personagem.TeveColisao(listaObstaculos);

            Assert.IsTrue(teveColisao);
        }

        [TestMethod]
        public void Personagem_PararMovimento_PersonagemParado()
        {
            var direcaoEsperada = Direcao.Parado;

            personagem.PararMovimento();

            Assert.AreEqual(direcaoEsperada, personagem.Direcao);
        }

        [TestMethod]
        [DataRow(Direcao.Direita)]
        [DataRow(Direcao.Esquerda)]
        [DataRow(Direcao.Vertical)]
        [DataRow(Direcao.Parado)]
        public void Personagem_Direcionar_DirecaoDefinida(Direcao direcao)
        {
            personagem.Direcionar(direcao);

            Assert.AreEqual(direcao, personagem.Direcao);
        }
    }
}
