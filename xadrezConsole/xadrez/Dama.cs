using tabuleiro;
namespace xadrez;

class Dama : Peca
{
    public Dama(Tabuleiro tab, Cor cor) : base(tab, cor)
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

        bool[,] mat = new bool[tab.linhas, tab.colunas]; 

        Posicao pos = new Posicao(0, 0); 
        
        //esquerda
        pos.definirValores(posicao.linha, posicao.coluna - 1);
        
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
            
            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }
            pos.definirValores(pos.linha, pos.coluna - 1);
        }
        
        //direita
        pos.definirValores(posicao.linha, posicao.coluna + 1);
        
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
            
            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }

            pos.definirValores(pos.linha, pos.coluna +1); 
        }
        
        //acima
        pos.definirValores(posicao.linha - 1, posicao.coluna);
        //eu tenho q ir marcando ate chegar no fim do tabuleiro
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;

            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }
            pos.definirValores(pos.linha - 1, pos.coluna);
        }
        
        //abaixo
        pos.definirValores(posicao.linha + 1, posicao.coluna);
        
        while (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;

            if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
            {
                break;
            }

            pos.definirValores(pos.linha + 1, pos.coluna);
        }
        
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
        return "D";
    }
}