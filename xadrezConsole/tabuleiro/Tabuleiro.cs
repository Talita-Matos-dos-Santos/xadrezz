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
    
    public Peca peca(Posicao pos)
    {
        //aqui temos uma melhoria do método acima
        return pecas[pos.linha, pos.coluna];
    }

    public void colocarPeca(Peca p, Posicao pos)
    {
        //vamos colocar uma Peca p na Posicao pos. Mas só vamos colocar se nao tiver nenhuma peca nessa posicao pos. Para isso:
        if (existePeca(pos))
        {
            //se ja existe uma peça dada uma posicao, ent vou passar uma exceçao com essa frase.
            throw new TabuleiroException("Já existe uma peça nessa posição!");
        }
        
        //vou entrar na matriz pecas aqui do tabuleiro, na posicao pos.linha, pos.coluna (ou seja, acessa a matriz pela linha e coluna do "objeto" pos que foi instanciado no tipo Posicao) e nessa posicao eu vou colocar uma Peca p (tabuleiro e cor).
        pecas[pos.linha, pos.coluna] = p;
        p.posicao = pos;
        //lembrando que posicao é um atributo da class Peca.
    }

    public Peca retirarPeca(Posicao pos)
    {
        //retirar uma peça ele vai me retornar uma peça, pois eventualmente eu posso precisar daquela peça. Por isso nao é void. Retiramos uma peca de uma dada Posicao pos
        if (peca(pos) == null)
        {
            //se a peca que tiver na posicao pos for igual a nulo, significa que n tem peca nessa posicao, ai o metodo vai retornar nulo, ou seja, n vai retirar nenhuma peca e so retorna nulo.
            return null;
        }
        //se passou do if acima significa que tem uma peça nessa posicao.
        
        Peca aux = peca(pos); //agr eu quero passar ent essa peca pra uma var auxiliar do tipo Peca.
        
        aux.posicao = null; //tira ela pra fora-> a posicao dessa peça aux vai receber null, ou seja, n vai mais ter peca nessa posicao, ela foi retirada.
        
        pecas[pos.linha, pos.coluna] = null; //marca a posicao dela como nulo, marca a posicao do tabuleiro q ela tava como nulo, ou seja, nao tem peça la mais
        
        return aux; //ai eu retorno essa peça
    }

    public bool existePeca(Posicao pos)
    {
        //aqui é pra testar se existe uma peça dada uma Posicao pos. Ou seja, se tem uma peça nessa posiçao. Para isso, pede pra retornar a peça na posicao caso seja diferente de null. E se retornar msm, significa que é verdadeiro.
        
        //chama o validar posicao pq se der algum erro o método ja é cortado. 
        validarPosicao(pos);
        return peca(pos) != null;
    }
    
    public bool posicaoValida(Posicao pos)
    {
        //metodo pra testar se o pos é valido ou nao, considerando que a posicao começa em 0 e vai ter um numero antes de linhas e colunas dados ao instanciar um Tabuleiros.

        if (pos.linha < 0 || pos.linha >= linhas || pos.coluna <0 || pos.coluna >= colunas)
        {
            return false;
        }

        return true;
    }
    
    public void validarPosicao(Posicao pos)
    {
        //esse metodo vai receber uma posicao e caso a posicao nao seja valida, ele vai lançar uma exceçao personalizada.
        
        //se a posicao nao for valida, eu lanço a exceçao com essa mensagem.
        //a exclamacao na frente significa "nao for"/se for false. Ou seja, se no método acima posicaoValida() tiver um return false, entao:
        if (!posicaoValida(pos))
        {
            throw new TabuleiroException("Posição inválida!");
        }
    }
}