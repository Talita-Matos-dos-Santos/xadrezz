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
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao(); //vai ler a posiçao e transformar ela em posicao de matriz, por isso tem o to.Posicao().
                    
                    partida.executaMovimento(origem, destino);


                }

                
                
                
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            
            
            
        }
    }
}