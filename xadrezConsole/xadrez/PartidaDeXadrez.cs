using System;
using tabuleiro;
namespace xadrez;

class PartidaDeXadrez
{
    public Tabuleiro tab { get; private set; }
    private int turno;
    private Cor jogadorAtual;
    public bool terminada { get; private set; }

    public PartidaDeXadrez()
    {
        //aq fica como fica qnd uma partida inicia. turno e jogador devem ir mudando durante a partida, naturalmente.
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branca;
        terminada = false;
        colocarPecas();
    }

    public void executaMovimento(Posicao origem, Posicao destino)
    {
        //executa um movimento da posicao q eu to agr(origem) pra posicao tal(destino)
        
        //executar um movimento é ir la no tabuleiro, pegar uma Peca p, retirar a peca p de um lugar(origem) e colocar a peca em outro lugar(destino).

        Peca p = tab.retirarPeca(origem);
        p.incrementarQteMovimentos();
        
        Peca pecaCapturada = tab.retirarPeca(destino); //tenho q retirar a peça do destino onde eu quero colocar a minha peça (caso tenha alguma).
        
        tab.colocarPeca(p, destino); //coloca a Peca p que tava na origem, no destino.
    }

    private void colocarPecas()
    {
        tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao()); //sem o toPosicao() daria erro, pois o colocarPeca(int, int) e o new posicaoxadrez pega char e int, por isso preciss converter pra toPosicao() vai retornar linha + coluna (que la na class Posicao sao inteiros).
        tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
        tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());
        
        tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
        tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
        tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
    }
}