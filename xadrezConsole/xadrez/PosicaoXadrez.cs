using tabuleiro;
namespace xadrez;

class PosicaoXadrez
{
    //essa class serve só pra auxiliar a pensar no contexto do xadrez, pois as posicoes do xadrez ficam escrito diferente das posicoes da matriz do C#.
    //so que internamente eu quero converter a posicao aqui do xadrez para uma posicao interna la da matriz -> A,8 terá que ser 0,0
    public char coluna { get; set; }
    public int linha { get; set; }

    public PosicaoXadrez(char coluna, int linha)
    {
        this.coluna = coluna;
        this.linha = linha;
    }

    public Posicao toPosicao()
    {
        //obs: internamente o caractere 'a' é um numero inteiro, ai se for 'a'- 'a' vai dar 0, se for 'b' - 'a', considerando que b é a proxima letra dps de a, ent b - a da 1. da pra pensar assim: se eu to na letra d, qnts casas eu ando de a ate d? 3, entao coluna 3
        return new Posicao(8 - linha, coluna - 'a');
    }

    public override string ToString()
    {
        return coluna + ", " + linha;
    }
    
}