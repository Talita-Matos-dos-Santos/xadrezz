using tabuleiro;

namespace xadrez;

class Torre : Peca
{
    //qnd uma class ta herdando de outra eu ja tenho que confirmar quem é o construtor de um Torre, se nao fica vermelhinho.
    
    //o construtor de um Torre recebendo um Tabuleiro tab e uma Cor cor, só faz repassar essa instruçao recebida pra class Peca.
    public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
    {
        //o construtor de um Torre recebendo um Tabuleiro tab e uma Cor cor, só faz repassar essa instruçao recebida pra class Peca. Ou seja, ele só repassa os argumentos que ele recebeu pro construtor da superclasse.
    }
    
     private bool podeMover(Posicao pos)
    {
        //aq é sobre se a Torre pode mover pra essa posicao pos.
        
        Peca p = tab.peca(pos); //esse peca(pos) é um metodo la do Tabuleiro.
        return p == null || p.cor != this.cor; //se for nula quer dizer q n tem peca nessa posicao, se a cor dessa peca for diferente da cor do rei(this.cor) significa q tem peca do adversario. 
       
    }
    public override bool[,] movimentosPossiveis() 
    {
        //override pra indicar que eu to sobrescrevendo aquele metodo da superclasse Peca aqui.

        bool[,] mat = new bool[tab.linhas, tab.colunas]; //a matriz vai ser o msm nro de linhas e colunas do tabuleiro que ta associado.

        Posicao pos = new Posicao(0, 0); 
        
        //acima
        pos.definirValores(posicao.linha - 1, posicao.coluna);
        //eu tenho q ir marcando ate chegar no fim do tabuleiro
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            //enquanto for uma posicao valida de XADREZ e enquanto estiver casa livre ou peça adversaria eu posso ir movendo pra essa posiçao.
            mat[pos.linha, pos.coluna] = true;
            //qnd eu "bater" em uma peça adversaria e captura-la eu devo parar, ent o if vai servir pra isso. Pra parar o While.
            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }

            pos.linha = pos.linha - 1; //caso isso(oq ta no if) n ocorra, eu tenho que continuar verificando "acima"-> pega a linha desse pos e faz ela receber ela msm -1. Ou seja, toda vez que for voltar no while vai "subtrair uma linha" pra ele ir caminhando pra linha acima. se to na linha 8 nesse while, no proximo vai ficar na linha 7.
        }
        
        //abaixo
        pos.definirValores(posicao.linha + 1, posicao.coluna);
        //eu tenho q ir marcando ate chegar no fim do tabuleiro
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
            //qnd eu "bater" em uma peça adversaria e captura-la eu devo parar, ent o if vai servir pra isso. Pra parar o While.
            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }

            pos.linha = pos.linha + 1; //toda vez q voltar no while eu vou estar uma linha mais abaixo. se tava na linha 4 nesse while, na proxima vou estar na linha 5.
        }
        
        //direita
        pos.definirValores(posicao.linha, posicao.coluna + 1);
        //eu tenho q ir marcando ate chegar no fim do tabuleiro
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
            //qnd eu "bater" em uma peça adversaria e captura-la eu devo parar, ent o if vai servir pra isso. Pra parar o While.
            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }

            pos.coluna = pos.coluna + 1; //smp q voltar no while vai estar uma coluna mais a direita
        }
        
        //esquerda
        pos.definirValores(posicao.linha, posicao.coluna - 1);
        //eu tenho q ir marcando ate chegar no fim do tabuleiro
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
            //qnd eu "bater" em uma peça adversaria e captura-la eu devo parar, ent o if vai servir pra isso. Pra parar o While.
            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }

            pos.coluna = pos.coluna - 1; 
        }
        
        return mat;
    }
    
    public override string ToString()
    {
        return "T";
    }
}