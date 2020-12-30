using System;
using System.Windows.Controls;
using MegamanTheHedgehog.Objetos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MegamanTheHedgehog.Tests.Objetos
{
    [TestClass]
    public class ObstaculosFaseTests
    {
        double larguraJanela = 1000;
        Image topo, direita, esquerda;
        ObstaculosFase obstaculos;

        [TestInitialize]
        public void Setup()
        {
            topo = new Image();
            direita = new Image();
            esquerda = new Image();

            topo.Width = direita.Width = esquerda.Width = larguraJanela / 10;

            obstaculos = new ObstaculosFase(topo, direita, esquerda, larguraJanela);
        }

        [TestMethod]
        public void ObstaculosFase_ToList_ListaComTodosObstaculos()
        {
            var totalExperado = 3;

            var obstaculosList = obstaculos.ToList();

            Assert.AreEqual(totalExperado, obstaculosList?.Count);
        }
    }
}
