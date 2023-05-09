using System;
using System.Collections.Generic;
using tabuleiro;
namespace xadrez;

class PartidaDeXadrez
{
    public Tabuleiro tab { get; private set; }
    public int turno { get; private set; }
    public Cor jogadorAtual { get; private set; }
    public bool terminada { get; private set; }
    private HashSet<Peca> pecas; //esse conjunto vai guardar todas as minhas pecas da partida.
    private HashSet<Peca> capturadas;

    public PartidaDeXadrez()
    {
        //aq fica como fica qnd uma partida inicia. turno e jogador devem ir mudando durante a partida, naturalmente.
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branca;
        terminada = false;
        pecas = new HashSet<Peca>();
        capturadas = new HashSet<Peca>();
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
        if (pecaCapturada != null)
        {
            capturadas.Add(pecaCapturada);
            //se tiver uma peca em pecaCapturada, ent vou adicionar essa pecaCapturada no meu conjunto capturadas.
        }
    }

    public HashSet<Peca> pecasCapturadas(Cor cor)
    {
        //conjunto Peca chamado pecasCapturadas de uma dada cor que entrou como parametro. Ou seja, quem sao as pecas capturadas Brancas, e quem sao as pecas capturadas Pretas. No caso estao misturadas pq la no if do metodo executaMovimentos elas foram adicionadas ao conjunto "capturadas" sem analise de cor.
        //esse metodo é justamente pra eu ver quais sao as Pecas capturadas de uma cor em especifico.
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca x in capturadas)
        {
            //vou percorrer todas as pecas x no conjunto dos capturadas
            if (x.cor == cor)//se a cor do meu obj x do conjunto capturadas for igual a cor q entrei como parametro
            {
                aux.Add(x); //entao eu vou add em aux as q derem true
            }
        }

        return aux;
    }

    public HashSet<Peca> pecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();
        foreach (Peca x in pecas)
        {
            //vou percorrer todas as pecas x no conjunto dos pecas -> aq vai percorrer todas as pecas do jogo
            if (x.cor == cor)//se a cor do meu obj x do conjunto pecas for igual a cor q entrei como parametro
            {
                aux.Add(x); //entao eu vou add em aux as q derem true
            }
        }
        aux.ExceptWith(pecasCapturadas(cor)); //agr eu vou pegar esse meu conjunto q add pecas e retirar (ExceptWith) todas as pecas capturadas dessa cor que entrei no parametro.
        //agr sim eu vou ter tds as pecas dessa cor, exceto as pecas capturadas dessa cor.
        //assim eu vou ficar com as pecas q ainda estao em jogo e q é da cor em especifico q eu quero.
        return aux;
    }

    public void colocarNovaPeca(char coluna, int linha, Peca peca)
    {
        //vai colocar no tabuleiro essa peca, em qual posicao? numa new posicaoxadrez.
        //dado uma coluna e linha e uma peca, eu vou no tabuleiro da partida e coloco.
        tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
        pecas.Add(peca); //alem de colocar a peca no tabuleiro eu tenho que dizer q no conjunto pecas da minha partida eu vou adicionar essa peça nesse conjunto, p dizer q essa peca faz parte da minha partida. 
    }
    private void colocarPecas()
    {
        colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
        colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
        colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
        colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
        colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
        colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
        
        
        colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
        colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
        colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
        colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
        colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
        colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
    }
    
}