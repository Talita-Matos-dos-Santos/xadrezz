namespace tabuleiro;

class Tabuleiro
{
    public int linha { get; set; }
    public int coluna { get; set; }
    private Peca[,] pecas;
    //a matriz das peças sao privativas pois nao podem ser acessadas por fora, so o tabuleiro que mexe nelas.

    public Tabuleiro(int linha, int coluna)
    {
        this.linha = linha;
        this.coluna = coluna;
        //na hora que for criar um novo tabuleiro tem que dizer quantas linha e colunas tem.
        //As peças vao receber uma nova matriz de peças. Ela terá esse numero de linhas por esse nro de colunas, o tanto que for determinado como argumento.
        pecas = new Peca[linha, coluna];
    }
}