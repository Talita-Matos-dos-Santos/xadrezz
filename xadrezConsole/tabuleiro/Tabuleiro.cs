namespace tabuleiro;

class Tabuleiro
{
    public int linhas { get; set; }
    public int colunas { get; set; }
    private Peca[,] pecas;
    //a matriz das peças sao privativas pois nao podem ser acessadas por fora, so o tabuleiro que mexe nelas.

    public Tabuleiro(int linhas, int colunas)
    {
        this.linhas = linhas;
        this.colunas = colunas;
        //na hora que for criar um novo tabuleiro tem que dizer quantas linha e colunas tem.
        //As peças vao receber uma nova matriz de peças. Ela terá esse numero de linhas por esse nro de colunas, o tanto que for determinado como argumento.
        pecas = new Peca[linhas, colunas];
    }
    
    //como o programador nao pode acessar a matriz Peca[,] em outras classes e eu preciso pra q possa imprimir na tela, eu vou criar um método que me retorne uma peça em especifico.

    public Peca peca(int linha, int coluna)
    {
        //o metodo vai receber uma linha e uma coluna como parametro e vai retornar a matriz peças na posicao linha e coluna.
        //Esse método sim é publico e pode acessar uma peça na linha e coluna necessario.
        
        //note, o nome do método é PECA no singular. O nome da matriz que será retornada é relativa ao atributo criado chamado PECAS no plural. Sao duas coisas diferentes!
        return pecas[linha, coluna];
    }
    
    //agora o tabuleiro vai ter que oferecer uma operacao pra colocar uma peca nele.

    public void colocarPeca(Peca p, Posicao pos)
    {
        //vamos colocar uma Peca p na Posicao pos.
        //vou entrar na matriz pecas aqui do tabuleiro, na posicao pos.linha, pos.coluna (ou seja, acessa a matriz pela linha e coluna do "objeto" pos que foi instanciado no tipo Posicao) e nessa posicao eu vou colocar uma Peca p (esse p é instanciada com posicao, tabuleiro e cor).
        pecas[pos.linha, pos.coluna] = p;
        p.posicao = pos;
        //lembrando que posicao é um atributo da class Peca.
    }
}