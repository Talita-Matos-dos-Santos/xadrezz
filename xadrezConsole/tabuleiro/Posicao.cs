namespace tabuleiro;
//a posiçao é uma regra basica de tabuleiro, nao necessariamente de xadrez, por isso o namespace nao tá "xadrezConsole.Tabuleiro"

class Posicao
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

    public void definirValores(int linha, int coluna)
    {
        //os parametros sao os msm do construtor aq de Posicao.
        //metodo criado pra poder facilitar a manipulaçao da variavel "pos" lá na class Rei dentro do metodo movimentosPossiveis().
        
        this.linha = linha;
        this.coluna = coluna;
    }

    public override string ToString()
    {
        return linha + ",  " + coluna;
    }
}