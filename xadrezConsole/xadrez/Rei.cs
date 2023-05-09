using tabuleiro;
namespace xadrez;

class Rei : Peca
{
    //qnd uma class ta herdando de outra eu ja tenho que confirmar quem é o construtor de um Rei, se nao fica vermelhinho.
    
    //o construtor de um Rei recebendo um Tabuleiro tab e uma Cor cor, só faz repassar essa instruçao recebida pra class Peca.
    public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
    {
        //o construtor de um Rei recebendo um Tabuleiro tab e uma Cor cor, só faz repassar essa instruçao recebida pra class Peca. Ou seja, ele só repassa os argumentos que ele recebeu pro construtor da superclasse.
    }

    private bool podeMover(Posicao pos)
    {
        //aq é sobre se o rei pode mover pra essa posicao pos.
        //de acordo com a regra, eu posso mover o rei pra qualquer direcao uma casa se nao tiver nenhuma peça ou se tiver uma peça do adversario, sendo q se tiver uma peça do adversario e eu for nela eu vou estar capturando a peca do adversario.
        Peca p = tab.peca(pos); //esse peca(pos) é um metodo la do Tabuleiro.
        return p == null || p.cor != this.cor; //se for nula quer dizer q n tem peca nessa posicao, se a cor dessa peca for diferente da cor do rei(this.cor) significa q tem peca do adversario. 
       
    }
    public override bool[,] movimentosPossiveis() 
    {
        //override pra indicar que eu to sobrescrevendo aquele metodo da superclasse Peca aqui.

        bool[,] mat = new bool[tab.linhas, tab.colunas]; //a matriz vai ser o msm nro de linhas e colunas do tabuleiro que ta associado.

        Posicao pos = new Posicao(0, 0); //primeiro vou definir essa posicao como sendo uma posicao acima do rei.
        
        //acima
        pos.definirValores(posicao.linha - 1, posicao.coluna);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //nordeste
        pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //direita
        pos.definirValores(posicao.linha, posicao.coluna + 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //sudeste
        pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //abaixo
        pos.definirValores(posicao.linha + 1, posicao.coluna);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //sudoeste
        pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //esquerda
        pos.definirValores(posicao.linha, posicao.coluna - 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //noroeste
        pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //feito isso, marquei as oito possiveis casas que o Rei pode ir. Agora é só retornar essa matriz q tem os verdadeiro/falso de onde se pode ir.

        return mat;
    }

    public override string ToString()
    {
        return "R";
    }
}