using tabuleiro;
namespace xadrezConsole;

class Tela
{
    public static void imprimirTabuleiro(Tabuleiro tab)
    {
        //aqui nao retorna nada(é void), tem só a responsabilidade de receber um tabuleiro como argumento e imprimir ele na tela.
        for (int i = 0; i < tab.linhas; i++)
        {
            for (int j = 0; j < tab.colunas; j++)
            {
                //se tiver alguma peça na posicao [i, j] eu vou imprimí-la, se nao tiver eu vou imprimir um tracinho.
                //eu vou imprimir a peça(ou tracinho), um espaço em branco, uma peça(ou tracinho), um espaço em branco. No final de cada linha (na ultima coluna) eu tenho que dar um console.writeline() pra que quebre a linha.
                
                //Provavelmente no program principal sera instanciado um objeto do tipo Tabuleiro com o nome tab. Esse objeto tab sera colocado dentro do parametro do metodo imprimirTabuleiro().
                //vou chamar o metodo peca no objeto tab. 
                if (tab.peca(i, j) == null)
                {
                    //se a peca do tab na posicao i, j nao tiver nada, vou imprimir somente o tracinho e o espaço.
                    Console.Write("- ");
                }
                else
                {
                    //se tiver alguma peça na posicao i, j, ai eu vou imprimir oq tem nessa peca + um espaço.
                    Console.Write(tab.peca(i, j)+ " ");
                }
            }
            //se n tivesse esse console.writeline() aq o formato n ficaria direitinho, agr tendo ele o programa imprime todas as pecas ou tracinhos numa linha inteira (passando por todas as colunas), dps q acaba a coluna esse console faz com que a linha quebre e o processo de imprimir seja feito na linha seguinte, e assim vai ate acabar a quantidade de linhas.
            Console.WriteLine();
            //abaixo tem a chave do forzao, acima tem a chave do forzinho
        }
    }
}