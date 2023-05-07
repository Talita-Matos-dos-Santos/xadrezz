namespace tabuleiro;
//o namespace nao é xadrezConsole.Tabuleiro pq assim como posiçao a cor é uma regra básica de tabuleiro, nao necessariamente de xadrez.
enum Cor
{
    //note, muitas cores aqui nao serao usadas no xadrez, mas como estamos determinando caracteristicas de tabuleiro NO GERAL, pode ser que alguma dessas outras cores sejam usadas, como no candcrush
    Branca,
    Preta,
    Amarela,
    Azul,
    Vermelha,
    Verde,
    Laranja
}