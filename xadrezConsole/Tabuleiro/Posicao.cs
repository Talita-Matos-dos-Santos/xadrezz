namespace Tabuleiro;

public class Posicao
{
    //Como o tabuleiro tem linhas e colunas, uma posiçao é dizer qual linha e qual coluna está. 
    //ele criou os atributos iniciando com letra minuscula, nao sei pq.
    public int linha { get; set; }
    public int coluna { get; set; }
    

    public Posicao(int linha, int coluna)
    {
        this.linha = linha;
        this.coluna = coluna;
    }

    public override string ToString()
    {
        return linha + ",  " + coluna;
    }
}