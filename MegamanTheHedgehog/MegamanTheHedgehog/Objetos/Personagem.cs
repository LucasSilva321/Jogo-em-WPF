using MegamanTheHedgehog.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MegamanTheHedgehog.Objetos
{
    public class Personagem
    {
        int imagemAtualIndex = 0;
        double marginTopPersonagem, gravidade = -1.5;
        Image imagem;

        public double MargemEsquerda => imagem.Margin.Left;
        public double MargemDireita => MargemEsquerda + imagem.Width;
        public double MargemTopo => imagem.Margin.Top;
        public double MargemInferior => MargemTopo + imagem.Height;

        public bool Pulando { get; set; } = true;

        public Direcao Direcao { get; private set; }

        public Personagem(Image imagem)
        {
            this.imagem = imagem;
            marginTopPersonagem = imagem.Margin.Top;
        }

        public void ReiniciarPosicao(double larguraJanela)
        {
            imagem.RenderTransform = new ScaleTransform(1, 1);
            imagem.Margin = new Thickness(larguraJanela / 2, marginTopPersonagem, 0, 0);
            PararMovimento();
        }

        public void MoverParaDireita(double deslocamento, double larguraJanela)
        {
            imagem.Margin = new Thickness(imagem.Margin.Left + deslocamento, imagem.Margin.Top, 0, 0);

            if (imagem.Margin.Left >= larguraJanela)
            {
                imagem.Margin = new Thickness(larguraJanela, imagem.Margin.Top, 0, 0);
            }

            AtualizarFrame(1);
        }

        public void MoverParaEsquerda(double deslocamento, double larguraJanela)
        {
            imagem.Margin = new Thickness(imagem.Margin.Left - deslocamento, imagem.Margin.Top, 0, 0);

            if (imagem.Margin.Left <= 0)
            {
                imagem.Margin = new Thickness(0, imagem.Margin.Top, 0, 0);
            }

            AtualizarFrame(-1);
        }

        public void PularOuCair()
        {
            imagem.Margin = new Thickness(imagem.Margin.Left, imagem.Margin.Top + (imagem.Height * gravidade), 0, 0);
            gravidade += 0.5;
            if (imagem.Margin.Top >= marginTopPersonagem)
            {
                imagem.Margin = new Thickness(imagem.Margin.Left, marginTopPersonagem, 0, 0);
                Pulando = true;
                gravidade = -1.5;
            }
        }

        public bool TeveColisao(List<Obstaculo> obstaculos)
        {
            foreach (var obstaculo in obstaculos)
            {
                if (obstaculo.TeveColisao(this))
                {
                    return true;
                }
            }

            return false;
        }

        public void PararMovimento()
        {
            Direcao = Direcao.Parado;
        }

        public void Direcionar(Direcao direcao)
        {
            Direcao = direcao;

            if (direcao == Direcao.Vertical)
            {
                Pulando = false;
            }
        }

        private void AtualizarFrame(double scaleX)
        {
            BitmapImage img = new BitmapImage(new Uri("Imagens/andando" + (imagemAtualIndex) + ".png", UriKind.RelativeOrAbsolute));
            imagem.Source = img;
            imagemAtualIndex = (imagemAtualIndex + 1) % 9;
            ScaleTransform x = new ScaleTransform(scaleX, 1);
            imagem.RenderTransform = x;
        }
    }
}
