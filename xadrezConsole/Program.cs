using System;
using tabuleiro;
using xadrez;

namespace xadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                
                //o ! na frente significa enquanto "partida.terminada NAO for verdadeira".
                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);


                        //dps q a pessoa digitar a posicao de origem eu vou limpar a tela e vou imprimir o tabuleiro com as posicoes possiveis daquela peca se movimentar, MARCADAS.
                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis(); //a partir da posicao de origem q o usuario digitou eu vou acessar essa peca de origem e vou pegar quais sao os movimentos possiveis e vou guardar na matriz posicoesPossiveis.
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao(); //vai ler a posiçao e transformar ela em posicao de matriz, por isso tem o to.Posicao().
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e) //captura a excecao com o apelido e
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine(); //aq é pra esperar o cara digitar enter pra eu repetir a jogada.
                    }
                    
                }
                
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            
            
            
        }
    }
}