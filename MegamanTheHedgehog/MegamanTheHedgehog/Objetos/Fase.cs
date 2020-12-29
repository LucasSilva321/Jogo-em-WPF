using MegamanTheHedgehog.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MegamanTheHedgehog.Objetos
{
    public class Fase
    {
        Personagem personagem;
        ObstaculosFase obstaculos;
        Placar placar;
        Cenario cenario;

        DispatcherTimer timer;
        SoundPlayer somdeFundo;

        Acao acao;
        double deslocamentoHorizontal;
        int incrementoPontuacao = 100;

        public Fase(Personagem personagem, ObstaculosFase obstaculos, Placar placar, Cenario cenario)
        {
            this.personagem = personagem;
            this.obstaculos = obstaculos;
            this.placar = placar;
            this.cenario = cenario;

            deslocamentoHorizontal = cenario.Largura / 32;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick;


            somdeFundo = new SoundPlayer();
        }

        public void Iniciar()
        {
            placar.Iniciar();
            personagem.ReiniciarPosicao(cenario.Largura);
            obstaculos.ReiniciarPosicoes();

            TocarMusicaDeFundo();
            AtualizarAcaoDoObstaculo();

            timer.Start();
            MoverObstaculo();
        }

        private void TocarMusicaDeFundo()
        {
            //Ao criar o executavel, o arquivo de audio muda de localização
            try
            {
                somdeFundo.SoundLocation = "Audio/Robotnik.wav";
                somdeFundo.PlayLooping();
            }
            catch
            {

                somdeFundo.SoundLocation = "Robotnik.wav";
                somdeFundo.PlayLooping();
            }
        }

        void MoverPersonagem()
        {
            if (personagem.MoverDireita)
            {
                personagem.MoverParaDireita(cenario.Largura);
                obstaculos.Topo.DeslocarParaEsqueda();
                cenario.DeslocarParaEsquerda(deslocamentoHorizontal);
            }
            else if (personagem.MoverEsquerda)
            {
                personagem.MoverParaEsquerda(cenario.Largura);
                obstaculos.Topo.DeslocarParaDireita();
                cenario.DeslocarParaDireita(deslocamentoHorizontal);
            }

            if (!personagem.Pulando)
            {
                personagem.PularOuCair();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            VerificarColisao();
            MoverPersonagem();
            MoverObstaculo();
        }

        void VerificarColisao()
        {
            if (personagem.TeveColisao(obstaculos.ToList()))
            {
                Finalizar();
            }
        }

        void Finalizar()
        {
            personagem.Parar();
            timer.Stop();
            placar.Finalizar();
            somdeFundo.Stop();
        }

        void MoverObstaculo()
        {
            var movimento = obstaculos.Mover(acao, cenario.Largura);

            if (movimento == Movimento.Finalizado)
            {
                AtualizarAcaoDoObstaculo();
                placar.MarcarPontuacao(incrementoPontuacao);
            }
        }

        private void AtualizarAcaoDoObstaculo()
        {
            var random = new Random();
            acao = (Acao)random.Next(0, 3);
        }

    }
}
