using System;
using tabuleiro;
namespace xadrez;

class PartidaDeXadrez
{
    public Tabuleiro tab { get; private set; }
    public int turno { get; private set; }
    public Cor jogadorAtual { get; private set; }
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

    public void realizaJogada(Posicao origem, Posicao destino)
    {
        //esse é o metodo q vou ter q chamar no meu programa pra realizar uma jogada, nao será mais o executaMovimento();
        executaMovimento(origem, destino);
        turno++;
        mudaJogador();
    }

    private void mudaJogador()
    {
        //metodo criado pra ser colocado no realizaJogada();
        //se quem tiver jogando agr for o jogador da cor branca, assim q ele fizer a jogada dele eu vou mudar de cor, ai o jogadoratual vai receber a cor preta, ou seja, qm joga agr é o da cor preta. Se nao (se o jogador atual n for o da cor branca, ent é o da cor preta), ai o jogador atual vai passar a ser o da cor branca.
        if (jogadorAtual == Cor.Branca)
        {
            jogadorAtual = Cor.Preta;
        }
        else
        {
            jogadorAtual = Cor.Branca;
        }
    }

    public void validarPosicaoDeOrigem(Posicao pos)
    {
        if (tab.peca(pos) == null) 
        {
            //se no tabuleiro a peça que ta nessa posicao for igual a nulo, significa q n tem peça
            //ent eu vou lançar uma nova TabuleiroException falando isso.
            throw new TabuleiroException("Não existe peça na posição de origem escolhida.");
        }

        if (jogadorAtual != tab.peca(pos).cor)
        {
            //se no tabuleiro a peça que ta nessa posicao tiver a cor diferente da cor do jogadorAtual, ent lanço uma nova exceçao.
            throw new TabuleiroException("A peça de origem escolhida não é sua.");
        }

        if (!tab.peca(pos).existeMovimentosPossiveis())
        {
            //se no tabuleiro nao(o nao é indicado pelo ! ali na frente) existir movimentos possiveis pra peça que ta nessa posicao.
            //eu acho q seria o msm q dizer q tab.peca(pos).existeMovimentosPossiveis() == false
            throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida.");
        }
    }

    public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        //precisa da origem pq o destino é validado em relaçao a origem. Confere se pode ir de uma posicao p outra.
        if (!tab.peca(origem).podeMoverPara(destino))
        {
            //se a peca de origem NAO(!) pode mover para a posicao de destino, ent eu lanço uma exceçao.
            throw new TabuleiroException("Posicao de destino inválida.");
        }
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