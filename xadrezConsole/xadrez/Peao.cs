using tabuleiro;
namespace xadrez;

class Peao : Peca
{
    private PartidaDeXadrez partida;//coloquei por conta da jogadaespecial que necessita ver as info da partida pra olhar qm ta vulneravel
    public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
    {
        this.partida = partida;
    }

    private bool existeInimigo(Posicao pos)
    {
        Peca p = tab.peca(pos);
        return p != null && p.cor != cor;
    }

    private bool livre(Posicao pos)
    {
        return tab.peca(pos) == null;
    }

    public override bool[,] movimentosPossiveis()
    {
        bool[,] mat = new bool[tab.linhas, tab.colunas];

        Posicao pos = new Posicao(0, 0);

        //essa logica é de acordo com as regras do xadrez pra peca peao.
        if (cor == Cor.Branca)
        {
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && livre(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha - 2, posicao.coluna);
            if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            // #jogadaespecial en passant
            //qnd eu posso dar um en passant com a branca?
            if (posicao.linha == 3)
            {
                //so vou ter um en passant da cor branca se ao lado dele tiver alguma peca vulneravel
                Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnpassant)//pra saber se o cara ao lado ta vulneravel eu tenho q acessar a partida, pois ela q guarda essa informacao.
                {
                    //significa q eu tenho q marcar essa posicao dessa mat como uma posicao possivel pra o Peao mexer.
                    mat[esquerda.linha - 1, esquerda.coluna] = true;
                }
                
                Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnpassant)
                {
                    mat[direita.linha - 1, direita.coluna] = true;
                }
            }
        }
        else
        {
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && livre(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha + 2, posicao.coluna);
            if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            // #jogadaespecial en passant
            //qnd eu posso dar um en passant com a preta?
            if (posicao.linha == 4)
            {
                //so vou ter um en passant da cor preta se ao lado dele tiver alguma peca vulneravel
                Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnpassant)//pra saber se o cara ao lado ta vulneravel eu tenho q acessar a partida, pois ela q guarda essa informacao.
                {
                    //significa q eu tenho q marcar essa posicao dessa mat como uma posicao possivel pra o Peao mexer.
                    mat[esquerda.linha + 1, esquerda.coluna] = true;
                }
                
                Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnpassant)
                {
                    mat[direita.linha + 1, direita.coluna] = true;
                }
            }
        }

        return mat;
    }

    public override string ToString()
    {
        return "P";
    }
}