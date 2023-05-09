using tabuleiro;
namespace xadrezConsole;
using xadrez;
using System.Collections.Generic;

class Tela
{
    public static void imprimirPartida(PartidaDeXadrez partida)
    {
        imprimirTabuleiro(partida.tab);
        Console.WriteLine();
        imprimirPecasCapturadas(partida);
        Console.WriteLine();
        Console.WriteLine("Turno: " + partida.turno);
        Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);
    }

    public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
    {
        Console.WriteLine("Peças capturadas: ");
        Console.Write("Brancas: ");
        imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
        Console.WriteLine();
        Console.Write("Pretas: ");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
        Console.ForegroundColor = aux;
        Console.WriteLine();
    }

    public static void imprimirConjunto(HashSet<Peca> conjunto)
    {
        Console.Write("[");
        foreach (Peca x in conjunto)
        {
            Console.Write(x + " "); //imprime uma peca x da um espaco, imprime uma peca x e da um espaco etc
        }
        Console.Write("]");
    }
    
    public static void imprimirTabuleiro(Tabuleiro tab)
    {
        //aqui nao retorna nada(é void), tem só a responsabilidade de receber um tabuleiro como argumento e imprimir ele na tela.
        for (int i = 0; i < tab.linhas; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < tab.colunas; j++)
            {
               //No final de cada linha (na ultima coluna) eu tenho que dar um console.writeline() pra que quebre a linha.
               imprimirPeca(tab.peca(i, j));
            }
            //se n tivesse esse console.writeline() aq o formato n ficaria direitinho, agr tendo ele o programa imprime todas as pecas ou tracinhos numa linha inteira (passando por todas as colunas), dps q acaba a coluna esse console faz com que a linha quebre e o processo de imprimir seja feito na linha seguinte, e assim vai ate acabar a quantidade de linhas.
            Console.WriteLine();
            //abaixo tem a chave do forzao, acima tem a chave do forzinho
        }
        Console.WriteLine("  a b c d e f g h");
    }
    
    public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
    {
        //aq nao somente imprime o tabuleiro como tbm muda a cor do fundo qnd tem posicoes possiveis 
        ConsoleColor fundoOriginal = Console.BackgroundColor;
        ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
        for (int i = 0; i < tab.linhas; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < tab.colunas; j++)
            {
                if (posicoesPossiveis[i, j])
                {
                    //se a posicao estiver marcada como uma posicao possivel de movimento, eu mudo o fundo pra verde
                    Console.BackgroundColor = fundoAlterado;
                }
                else
                {
                    //se n for uma posicaopossivel, ai fica com o fundo original msm
                    Console.BackgroundColor = fundoOriginal;
                }
                imprimirPeca(tab.peca(i, j));
                Console.BackgroundColor = fundoOriginal;
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
        Console.BackgroundColor = fundoOriginal;
    }
    
    public static PosicaoXadrez lerPosicaoXadrez()
    {
        //esse metodo vai ler o teclado em string, ler e guardar numa string. (oq o usuario digitar, sendo q o usuario vai digitar uma posicaoxadrez (uma letra e um numero)). Ai vai guardar a primeira coisa q o usuario digitou, que tem q ser a letra e guardar numa char, bem como pegar a segunda coisa q o usuario digitou e guardar em um int. Lembrando que se oq o usuario digitou ta guardado numa string s, pra pegar a primeira coisa q o usuario digitou eu coloco s[0], a segunda coisa fica s[1].

        string s = Console.ReadLine();
        char coluna = s[0];
        int linha = int.Parse(s[1] + ""); //ele disse q precisou de "" pro compilador pensar q era uma string msm e assim converter pra int

        return new PosicaoXadrez(coluna, linha);
    }

    public static void imprimirPeca(Peca peca)
    {
        //se a Peca Peca q entra como parametro for diferente de null(ou seja, n tem peca), ent
        if (peca == null)
        {
            Console.Write("- ");
        }
        else 
        {
            if (peca.cor == Cor.Branca)
            {
                //se for branca vou só imprimir a peca normal
                Console.Write(peca);
            }
            else
            {
                //senao, vai ser uma peça preta
                //ConsoleColor -> é um tipo do C# q pega a cor do sistema.
                //foregroundcolor é a cor que ja tava, meio acinzentado.
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }
    
}