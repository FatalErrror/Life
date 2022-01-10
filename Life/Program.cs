using System;
using System.Threading;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 100;
            Console.WindowHeight = 40;

            Console.Title = "Life";
            Console.CursorVisible = false;

            while (!Console.CapsLock)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Resize window and then press CAPS key to continue...");
                Console.WriteLine($"Window size is ({Console.WindowWidth};{Console.WindowHeight})");
                Thread.Sleep(100);
            }

            Life.Game.Game game = new Life.Game.Game();
            game.StartMainWhile();
        }
    }
}
