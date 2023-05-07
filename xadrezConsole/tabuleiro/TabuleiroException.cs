using System;
namespace tabuleiro;

class TabuleiroException : Exception
{
    //o construtor abaixo vai receber a mensagem em forma de string e simplesmente repassar essa mensagem pra class Exception do C#
    public TabuleiroException(string message) : base(message)
    {
        
    }
}