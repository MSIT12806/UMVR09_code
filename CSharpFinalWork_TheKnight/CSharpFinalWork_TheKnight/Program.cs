using Console2048;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFinalWork_TheKnight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("HIHI");
            var x = Console.CursorLeft;
            var y = Console.CursorTop;
            Console.SetCursorPosition(x-1, y);
            Console.Write("\b");
            Console.Read();
            //UiGenerate.DefaultPattern();
        }

        static void Game()
        {
            var gm = Singalton<GameManager>.SingletonObj;
        }

    }
}
