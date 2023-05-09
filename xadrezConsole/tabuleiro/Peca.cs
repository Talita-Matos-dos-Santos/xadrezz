namespace tabuleiro;
//aqui vamos colocar as coisas que uma peça tem. Por exemplo, ela tem uma posiçao e uma cor.
abstract class Peca
{
    public Posicao posicao { get; set; }
    public Cor cor { get; protected set; }
    //a cor pode ser alterada somente por ela msm e as subclasses dela. Pelo programa principal nao pode, se nao poderia ficar mudando as cores das peças livremente.
    
    public int qteMovimentos { get;  protected set; }
    //isso aq se usa por ex qnd quer controlar se o peao ta mexendo a primeira vez ou nao. Pode ser acessada por qualquer class mas so pode ser alterada por ela msm e suas subclasses.
    
    public Tabuleiro tab { get; protected set; }

    public Peca(Tabuleiro tab, Cor cor)
    {
        //quem vai dar a posicao pra Peca é o colocarPeca() la na class Tabuleiro.
        this.posicao = null;
        this.tab = tab;
        this.cor = cor;
        //a quantidade de movimentos será iniciada com 0, pois obviamente qnd o jogo acaba de ser criado nenhuma peça se moveu ainda. Por isso ele nao foi usado como parametro de iniciaçao.
        this.qteMovimentos = 0;
    }

    public void incrementarQteMovimentos()
    {
        qteMovimentos++;
    }

    public bool existeMovimentosPossiveis()
    {
        //ela vai conferir se na matriz bool[,] movimentosPossiveis existe pelo menos algum valor verdadeiro, ou seja, testa se a peça nao ta bloqueada de movimento.

        bool[,] mat = movimentosPossiveis();
        for (int i=0; i<tab.linhas; i++)
        {
            for (int j = 0; j < tab.colunas; j++)
            {
                if (mat[i, j])//lembrando q qnd n coloco nd é o msm q dizer "se (isso aq for true)"
                {
                    //se eu percorrer essa matriz e em alguma posicao tiver um true, entao significa que posso retornar true pro existeMovimentosPossiveis(), pois existe um movimento q é possivel.
                    return true;
                }
            }
        }
        //se percorrer a matriz inteira e n retornar nada, quer dizer que nao tem nenhum movimento possivel na minha matriz, ent retorno false.
        return false;
    }

    public bool podeMoverPara(Posicao pos)
    {
        //vai me dizer se pode mover para uma dada posicao pos, ou seja, se essa posicao é um dos movimentos possiveis.
        //vai pegar os movimentos possiveis dessa peca e vai testar se na matriz na linha e coluna dessa posicao é true;
        return movimentosPossiveis()[pos.linha, pos.coluna];
    }
    
    
    
    
    
    //essa operacao de movimentos possiveis vai ter q me retornar uma matriz de booleanos, pq eu vou querer marcar nessa matriz verdadeiro onde for movimento onde for movimento possivel e falso onde nao for possivel.
    public abstract bool[,] movimentosPossiveis(); //nao tem como eu implementar essa operacao aqui na class Peca pq cada peça tem os seus movimentos possiveis, cada peça tem uma regra. Logo, deve ser colocada em cada peça em específico. A Peca por si só é genérica. 
    //o abstract na frente serve justamente pra isso, pra dizer que o metodo nao tem implementaçao nessa classe. Lembrando que quando se tem um metodo abstrato, a classe tbm deve automaticamente ser abstrata.
}

//observaçoes: um tabuleiro tem varias peças.
//a class Peca é uma class genérica, presente na camada tabuleiro.
//na camada jogo de xadrez eu tenho que criar peças especificas, com uma relaçao de herança com a superclasse PECA.
//o Rei É uma Peca, a Torre É uma Peca, a Dama É uma Peca etc...