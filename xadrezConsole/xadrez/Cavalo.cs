using tabuleiro;
namespace xadrez;

class Cavalo : Peca
{
    public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
    {
    }

    private bool podeMover(Posicao pos)
    {
        Peca p = tab.peca(pos);
        return p == null || p.cor != cor;
    }
    
    public override bool[,] movimentosPossiveis()
    {
        bool[,] mat = new bool[tab.linhas, tab.colunas];

        Posicao pos = new Posicao(0, 0); 
        
        //parecido com o do rei, mas eu smp testo ou com 2 na linha e 1 na coluna ou com 1 na linha e 2 na coluna
        pos.definirValores(posicao.linha - 1, posicao.coluna - 2);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        pos.definirValores(posicao.linha - 2, posicao.coluna - 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        pos.definirValores(posicao.linha - 2, posicao.coluna + 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        pos.definirValores(posicao.linha - 1, posicao.coluna + 2);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        pos.definirValores(posicao.linha + 1, posicao.coluna + 2);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        pos.definirValores(posicao.linha + 2, posicao.coluna + 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        pos.definirValores(posicao.linha + 2, posicao.coluna - 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        pos.definirValores(posicao.linha + 1, posicao.coluna - 2);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        return mat;
    }

    public override string ToString()
    {
        return "C";
    }
}