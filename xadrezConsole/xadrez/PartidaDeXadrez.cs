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
    public bool xeque { get; private set; }
    public Peca vulneravelEnpassant { get; private set; }
    
    public PartidaDeXadrez()
    {
        //aq fica como fica qnd uma partida inicia. turno e jogador devem ir mudando durante a partida, naturalmente.
        tab = new Tabuleiro(8, 8);
        turno = 1;
        jogadorAtual = Cor.Branca;
        terminada = false;
        xeque = false;
        vulneravelEnpassant = null;
        pecas = new HashSet<Peca>();
        capturadas = new HashSet<Peca>();
        colocarPecas();
    }
    
    public Peca executaMovimento(Posicao origem, Posicao destino)
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
        
        // #jogadaespecial roque pequeno
        if (p is Rei && destino.coluna == origem.coluna + 2)//+2 pq é pra direita
        {
            //se a Peca p que foi movida é Rei e se a coluna da posicao de destino for igual a coluna da posicao de origem +2, ou seja, significa q eu movi esse rei duas casas, ENTAO É UM ROQUE. Se for um roque eu tbm tenho q mexer a torre.

            Posicao origemT = new Posicao(origem.linha, origem.coluna + 3); //origem da Torre é msm linha do rei e 3 casas pro lado em relacao a origem do rei
            Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1); //destino da Torre é msm linha do rei e 1 casa pro lado em relacao a origem do rei.

            Peca T = tab.retirarPeca(origemT);
            T.incrementarQteMovimentos();
            tab.colocarPeca(T, destinoT);
        }
        
        // #jogadaespecial roque grande
        if (p is Rei && destino.coluna == origem.coluna - 2)//agr o rei vai andar p esquerda, por isso eh -2
        {
            //se a Peca p que foi movida é Rei e se a coluna da posicao de destino for igual a coluna da posicao de origem +2, ou seja, significa q eu movi esse rei duas casas, ENTAO É UM ROQUE. Se for um roque eu tbm tenho q mexer a torre.

            Posicao origemT = new Posicao(origem.linha, origem.coluna - 4); //origem da Torre é msm linha do rei e 4 casas pro lado em relacao a origem do rei
            Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1); //destino da Torre é msm linha do rei e 1 casa pro lado em relacao a origem do rei.

            Peca T = tab.retirarPeca(origemT);
            T.incrementarQteMovimentos();
            tab.colocarPeca(T, destinoT);
        }
       
        // #jogadaespecial en passant
        //qnd eu executar o movimento eu tenho q ver se esse movimento é um en passant, se for eu vou ter q capturar a peca vulneravel na mao.
        //a peca movida é a Peca p, vamos aproveita-la aq
        //qnd a gnt faz o en passant eu movo a peca branca uma coluna antes da propria origem e uma linha acima da peca q sera capturada.
        if (p is Peao)
        {
            if (origem.coluna != destino.coluna && pecaCapturada == null)
            {
                //vai significar q o peao mexeu na diagonal. Diagonal é movimento de captura do peao.
                Posicao posP;
                if (p.cor == Cor.Branca)
                {
                    posP = new Posicao(destino.linha + 1,
                        destino.coluna); //esse posP é a posicao do Peao q vai ser capturado "na mao
                }
                else
                {
                    posP = new Posicao(destino.linha - 1, destino.coluna);
                }

                pecaCapturada = tab.retirarPeca(posP);
                capturadas.Add(pecaCapturada);
            }
        } 
        return pecaCapturada;
    }

    public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca p = tab.retirarPeca(destino); //tira a peca do destino q eu tinha colocado
        p.decrementarQteMovimentos(); 
        if (pecaCapturada != null) //ou seja, se for diferente de null é pq teve uma peca capturada
        {
            tab.colocarPeca(pecaCapturada, destino); //vou no tabuleiro e coloco a peca de volta no destino
            capturadas.Remove(pecaCapturada); //tira a pecaCapturada do conjunto
        }
        tab.colocarPeca(p, origem);
        
        //ja que la no executaMovimento() eu incrementei codigo pra alterar a posicao da torre na jogada especial, agr eu tbm tenho q ter codigo pra desfazer isso caso nao tenha cumprido as condicoes impostas aqui.
        
        // #jogadaespecial roque pequeno
        if (p is Rei && destino.coluna == origem.coluna + 2)
        {
            //se a Peca p que foi movida é Rei e se a coluna da posicao de destino for igual a coluna da posicao de origem +2, ou seja, significa q eu movi esse rei duas casas, ENTAO É UM ROQUE. Se for um roque eu tbm tenho q mexer a torre.

            Posicao origemT = new Posicao(origem.linha, origem.coluna + 3); 
            Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1); 

            Peca T = tab.retirarPeca(destinoT);//vou tirar o rei desse destinoT que eu coloquei ali
            T.decrementarQteMovimentos();//vou decrementar a qte
            tab.colocarPeca(T, origemT);//volto ele pra origem dessa torre
        }
        
        // #jogadaespecial roque grande
        if (p is Rei && destino.coluna == origem.coluna - 2)
        {
            Posicao origemT = new Posicao(origem.linha, origem.coluna - 4); 
            Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1); 

            Peca T = tab.retirarPeca(destinoT);
            T.decrementarQteMovimentos();
            tab.colocarPeca(T, origemT);
        }
        
        //pela mecanica geral, na hr de desfazer ele volta a peca capturada pra posicao de destino da Peca p que eu to movendo agr. So q a posicao de origem da peca capturada nesse caso n é a posicao de destino da peca q ta se movimentando. A mecanica geral n funciona aq pq no en passant a peca q se move nao toma o lugar da peca capturada.
        //dessa forma, é necessario fazer outra regra aq exclusiva do en passant.
        
        // #jogadaespecial en passant
        if (p is Peao)
        {
            if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnpassant)//significa q foi um movimento de captura e n de apenas movimento
            {
                Peca peao = tab.retirarPeca(destino);//eu tiro o peao q tinha voltado pro lugar errado
                Posicao posP;
                if (p.cor == Cor.Branca)//se a cor do peao q moveu for branca, entao
                {
                    posP = new Posicao(3, destino.coluna);
                }
                else //caso contrario o peao q se moveu foi a preta
                {
                    posP = new Posicao(4, destino.coluna);
                }
                tab.colocarPeca(peao, posP);
            }
        }
    }

    public void realizaJogada(Posicao origem, Posicao destino)
    {
        //qnd eu for realizar uma jogada eu tenho q pegar essa peca capturada do executaMovimento(). 
        Peca pecaCapturada = executaMovimento(origem, destino);
        //Se eu executei um movimento e capturei uma peca, eu tenho q ver antes de mais nd se com esse movimento eu fiquei em xeque. 
        if (estaEmXeque(jogadorAtual))
        {
            desfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }
        
        Peca p = tab.peca(destino); //qual foi a peca q foi movida
        //se cheguei aq a jogada foi efetivada.
        
        // #jogadaespecial promocao
        //tenho q ver se a peca q foi movida eh um peao, se for um peao
        if (p is Peao)
        {
            if (p.cor == Cor.Branca && destino.linha == 0 || (p.cor == Cor.Preta && destino.linha == 7))
            {
                //se acontecer isso significa q é uma jogada de promocao
                p = tab.retirarPeca(destino);
                pecas.Remove(p);
                Peca dama = new Dama(tab, p.cor);//no jogo oficial o jogador poderia escolher qual peca ele quer, mas aq vamos transformar diretamente em dama (geralmente escolhem dama mesmo)
                tab.colocarPeca(dama, destino);
                pecas.Add(dama);
            }
        }
        
        

        
        
        
        if (estaEmXeque(adversaria(jogadorAtual)))
        {
            xeque = true;
        }
        else
        {
            xeque = false;
        }

        if (testeXequemate(adversaria(jogadorAtual)))
        {
            //se eu realizei uma jogada e o meu adversario esta em xequemate, significa que o jogo terminou.
            terminada = true;
        }
        else
        {
            turno++;
            mudaJogador();
        }
        
        // #jogadaespecial en passant
        
        //caso essa peca q foi movida seja um Peao e andou duas casas, quer dizer q foi a primeira vez e ela ta vulneravel(????)-> eu acho q talvez o peao so pode andar 2 casas se for a primeira vez dele, por isso fica subentendido
        if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
        {
            //se isso for vdd significa q ta vulneravel a tomar um enpassant no proximo turno. 
            vulneravelEnpassant = p;
        }
        else
        {
            vulneravelEnpassant = null;
        }
        
        
        
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
        if (!tab.peca(origem).movimentoPossivel(destino))
        {
            //se a peca de origem NAO(!) pode mover para a posicao de destino, ent eu lanço uma exceçao.
            throw new TabuleiroException("Posicao de destino inválida.");
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

    private Cor adversaria(Cor cor)
    {
        //se a cor q entra como parametro for igual a cor branca, retorne cor preta para o metodo adversaria. A cor que for retornada é a cor adversaria da cor que entrar como parametro.
        if (cor == Cor.Branca)
        {
            return Cor.Preta;
        }
        else
        {
            return Cor.Branca;
        }
    }

    private Peca rei(Cor cor)
    {
        //vai me devolver quem é o rei de uma dada cor.
        
        //lembrando q Peca é uma superclasse e Rei é uma subclasse.
        foreach (Peca x in pecasEmJogo(cor))
        {
            if (x is Rei)
            {
                //se a Peca x é uma peça instanciada como Rei
                return x;
            }
        }
        //se esgotar o for e n encontrar nenhum rei, retorna null q vai significar q n tem rei (isso n deve acontecer)
        return null;
    }

    public bool estaEmXeque(Cor cor)
    {
        Peca R = rei(cor);
        if (R == null)
        {
            throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");
        }
        
        //para cada peca adversaria eu vou testar se na matriz q recebeu os movimentos possiveis da peca adversaria x na posicao onde tá o rei estiver verdadeiro, significa q essa peça adversaria pode capturar o rei, ai eu retorno o true (indicando que o rei esta em xeque)
        foreach (Peca x in pecasEmJogo(adversaria(cor)))
        {
            bool[,] mat = x.movimentosPossiveis();
            if (mat[R.posicao.linha, R.posicao.coluna])
            {
                return true;
            }
        }
        //se varrer tds pecas adversarias e nenhuma cortar o metodo no true, significa que nao esta em xeque.
        return false;
    }

    public bool testeXequemate(Cor cor)
    {
        //o rei de uma certa cor esta em xequemate?
        if (!estaEmXeque(cor))
        {
            return false;
            //se o rei de uma cor n esta em xeque ent com ctz tbm n ta em xequemate.
        }

        foreach (Peca x in pecasEmJogo(cor))
        {
            //vai percorrer toda peca x no conjunto de pecasemjogo dessa cor. Dentre as pecas desse conjunto, quero achar alguma peca q movendo, tira do xeque.

            bool[,] mat = x.movimentosPossiveis(); //vou pegar a matriz de movimentos possiveis dessa peca x e vou colocar em uma q vai servir como auxiliar. Ai pra cada movimento eu vou ver se tira do xeque ou n
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j])//se tiver marcado como true
                    {
                        Posicao origem = x.posicao;
                        Posicao destino = new Posicao(i, j);
                        //significa q é uma posicao possivel pra essa peca x.
                        Peca pecaCapturada = executaMovimento(origem, destino);
                        //vou tentar executar um movimento da posicao de origem q ela ja ta pra essa posicao i, j. (mas so se for true).
                        bool testeXeque = estaEmXeque(cor); //executei o movimento acima e agr testo se ainda estaEmXeque.
                        desfazMovimento(origem, destino, pecaCapturada); //fez o movimento, testou se ainda tava em xeque, agr desfaz o movimento.

                        if (!testeXeque)
                        {
                            //se n esta mais em xeque, significa q o movimento q eu fiz, tira do xeque. Entao existe um movimento q tira do xeque, logo nao ta em xequemate.
                            return false;
                        }
                    }
                }
            }
        }
        //fez isso para todas as pecas e nenhuma tirou do xeque, significa para essa cor que ela ta em Xequemate e perdeu o jogo.
        return true;
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
        colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
        colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
        colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
        colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
        colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));//como eu ja to aq dentro da propria partida, entao eu coloco um "this" pra fazer referencia a ela msm.
        colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
        colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
        colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
        
        colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this)); //como eu ja to aq dentro da propria partida, entao eu coloco um "this" pra fazer referencia a ela msm.
        colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
        colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
        colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
        colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
        colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
        colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
        colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));
        
        
        colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
        colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
        colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
        colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
        colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));//como eu ja to aq dentro da propria partida, entao eu coloco um "this" pra fazer referencia a ela msm.
        colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
        colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
        colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
        
        colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
        colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
    }
}