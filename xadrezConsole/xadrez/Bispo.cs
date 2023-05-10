using tabuleiro;
namespace xadrez;

class Bispo : Peca
{
    public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)
    {
    }
    
    private bool podeMover(Posicao pos)
    {
        Peca p = tab.peca(pos);
        return p == null || p.cor != cor;
    }

    public override bool[,] movimentosPossiveis()
    {
        //override pra indicar que eu to sobrescrevendo aquele metodo da superclasse Peca aqui.

        bool[,] mat = new bool[tab.linhas, tab.colunas]; //a matriz vai ser o msm nro de linhas e colunas do tabuleiro que ta associado.

        Posicao pos = new Posicao(0, 0); 
        
        //Noroeste
        pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
        
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
            
            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }
            pos.definirValores(pos.linha - 1, pos.coluna - 1);
        }
        
        //Nordeste
        pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
        //eu tenho q ir marcando ate chegar no fim do tabuleiro
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
            
            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }

            pos.definirValores(pos.linha -1, pos.coluna +1); 
        }
        
        //Sudeste
        pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
        //eu tenho q ir marcando ate chegar no fim do tabuleiro
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;

            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }
            pos.definirValores(pos.linha + 1, pos.coluna + 1);
        }
        
        //Sudoeste
        pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
        //eu tenho q ir marcando ate chegar no fim do tabuleiro
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;

            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }

            pos.definirValores(pos.linha + 1, pos.coluna - 1);
        }
        return mat;
    }
    
    
    
    public override string ToString()
    {
        return "B";
    }
}