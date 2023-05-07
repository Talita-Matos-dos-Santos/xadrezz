using tabuleiro;

namespace xadrez;

class Torre : Peca
{
    //qnd uma class ta herdando de outra eu ja tenho que confirmar quem é o construtor de um Torre, se nao fica vermelhinho.
    
    //o construtor de um Torre recebendo um Tabuleiro tab e uma Cor cor, só faz repassar essa instruçao recebida pra class Peca.
    public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
    {
        //o construtor de um Torre recebendo um Tabuleiro tab e uma Cor cor, só faz repassar essa instruçao recebida pra class Peca. Ou seja, ele só repassa os argumentos que ele recebeu pro construtor da superclasse.
    }

    public override string ToString()
    {
        return "T";
    }
}