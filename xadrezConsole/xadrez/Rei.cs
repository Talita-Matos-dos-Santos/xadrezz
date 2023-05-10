using tabuleiro;
namespace xadrez;

class Rei : Peca
{
    private PartidaDeXadrez partida; //foi implementado pois o rei p fazer uma jogada especial precisa conhecer a sua partida. Vou incluir no construtor.
    
    
    //qnd uma class ta herdando de outra eu ja tenho que confirmar quem é o construtor de um Rei, se nao fica vermelhinho.
    
    //o construtor de um Rei recebendo um Tabuleiro tab e uma Cor cor, só faz repassar essa instruçao recebida pra class Peca.
    public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
    {
        //o construtor de um Rei recebendo um Tabuleiro tab e uma Cor cor, só faz repassar essa instruçao recebida pra class Peca. Ou seja, ele só repassa os argumentos que ele recebeu pro construtor da superclasse.
        this.partida = partida; //agr eu acrescentei isso aq tbm, pois queria ter acesso a partida a partir do Rei.

    }

    private bool podeMover(Posicao pos)
    {
        //aq é sobre se o rei pode mover pra essa posicao pos.
        //de acordo com a regra, eu posso mover o rei pra qualquer direcao uma casa se nao tiver nenhuma peça ou se tiver uma peça do adversario, sendo q se tiver uma peça do adversario e eu for nela eu vou estar capturando a peca do adversario.
        Peca p = tab.peca(pos); //esse peca(pos) é um metodo la do Tabuleiro.
        return p == null || p.cor != this.cor; //se for nula quer dizer q n tem peca nessa posicao, se a cor dessa peca for diferente da cor do rei(this.cor) significa q tem peca do adversario. 
       
    }

    private bool testeTorreParaRoque(Posicao pos)
    {
        //para testar se uma Torre pode fazer roque eu tenho que pegar a peca que estiver nessa posicao e ai uma torre pode participar de um roque SE essa peca nao for nula e essa peca é uma instancia de torre e a cor dessa peca tem q ser a msm cor desse rei, n pode ser torre adversaria e a quantidade de movimento dessa torre deve ser 0. 
        //isso testara se a peca que ta na posicao q entra como parametro é uma torre elegivel para ROQUE.
        Peca p = tab.peca(pos);
        return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;
    }
    public override bool[,] movimentosPossiveis() 
    {
        //override pra indicar que eu to sobrescrevendo aquele metodo da superclasse Peca aqui.

        bool[,] mat = new bool[tab.linhas, tab.colunas]; //a matriz vai ser o msm nro de linhas e colunas do tabuleiro que ta associado.

        Posicao pos = new Posicao(0, 0); //primeiro vou definir essa posicao como sendo uma posicao acima do rei.
        
        //acima
        pos.definirValores(posicao.linha - 1, posicao.coluna);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //nordeste
        pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //direita
        pos.definirValores(posicao.linha, posicao.coluna + 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //sudeste
        pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //abaixo
        pos.definirValores(posicao.linha + 1, posicao.coluna);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //sudoeste
        pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //esquerda
        pos.definirValores(posicao.linha, posicao.coluna - 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        //noroeste
        pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
        if (tab.posicaoValida(pos) && podeMover(pos))
        {
            mat[pos.linha, pos.coluna] = true;
        }
        
        // #jogadaespecial Roque
        if (qteMovimentos == 0 && !partida.xeque)
        {
            //se o rei n se moveu ainda e se ainda n ta em xeque:

            // #jogadaespecial roque pequeno
            Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3); //essa é a posicao q eu espero que a Torre esteja pra verificar se eu posso fazer o roque.
            if (testeTorreParaRoque(posT1))//se a torre ta elegivel p roque
            {
                Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);//msm linha do rei na coluna ao lado direito
                Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);//msm linha do rei 2 casas dps
                if (tab.peca(p1) == null && tab.peca(p2) == null)
                {
                    //se n tiver nenhuma peca nessas posicoes acima, ent 
                    mat[posicao.linha, posicao.coluna + 2] = true;
                }
            }
            
            // #jogadaespecial roque grande
            Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4); //essa é a posicao q eu espero que a Torre esteja pra verificar se eu posso fazer o roque grande.
            if (testeTorreParaRoque(posT2))//se a torre ta elegivel p roque
            {
                Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);//msm linha do rei na coluna ao lado esquerdo
                Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);//msm linha do rei 2 casas antes
                Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);//msm linha do rei 3 casas antes

                if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                {
                    //se n tiver nenhuma peca nessas posicoes acima, ent 
                    mat[posicao.linha, posicao.coluna - 2] = true;
                }
            }
        }
        
        
        
        
        
        //feito isso, marquei as oito possiveis casas que o Rei pode ir. Agora é só retornar essa matriz q tem os verdadeiro/falso de onde se pode ir.
        return mat;
    }

    public override string ToString()
    {
        return "R";
    }
}