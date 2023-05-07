namespace tabuleiro;
//aqui vamos colocar as coisas que uma peça tem. Por exemplo, ela tem uma posiçao e uma cor.
class Peca
{
    public Posicao posicao { get; set; }
    //a cor pode ser alterada somente por ela msm e as subclasses dela. Pelo programa principal nao pode, se nao poderia ficar mudando as cores das peças livremente.
    public Cor cor { get; protected set; }
    //isso aq se usa por ex qnd quer controlar se o peao ta mexendo a primeira vez ou nao. Pode ser acessada por qualquer class mas so pode ser alterada por ela msm e suas subclasses.
    public int qteMovimentos { get;  protected set; }
    public Tabuleiro tab { get; protected set; }

    public Peca(Posicao posicao, Tabuleiro tab, Cor cor)
    {
        this.posicao = posicao;
        this.tab = tab;
        this.cor = cor;
        //a quantidade de movimentos será iniciada com 0, pois obviamente qnd o jogo acaba de ser criado nenhuma peça se moveu ainda. Por isso ele nao foi usado como parametro de iniciaçao.
        this.qteMovimentos = 0;
    }
}