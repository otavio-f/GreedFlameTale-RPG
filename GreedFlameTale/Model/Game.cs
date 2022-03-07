using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GreedFlameTale.Model
{
    class Game
    {
        private enum TextAlignment { LEFT, RIGHT, CENTER };

        // private CharacterBase[] PlayerTeam;
        // private CharacterBase[] EnemyTeam;

        public Game()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void WriteAligned(string text, TextAlignment align)
        {
            string writeable;
            switch (align)
            {
                case TextAlignment.RIGHT:
                    writeable = text.PadLeft(Console.WindowWidth);
                    break;
                case TextAlignment.CENTER:
                    var fill = (Console.WindowWidth / 2) + (text.Length / 2);
                    writeable = text.PadLeft(fill);
                    break;
                default:
                    writeable = text;
                    break;
            }
            Console.WriteLine(writeable);
        }

        public void InitScreen()
        {
            Console.Clear();
            WriteAligned("By Otavio-f https://github.com/otavio-f", TextAlignment.CENTER);
            Console.SetCursorPosition(0, Console.WindowHeight / 2);
            WriteAligned("Loading...", TextAlignment.CENTER);
            Thread.Sleep(1000);
            Console.Clear();
        }

        public void Mainloop()
        {
            InitScreen();
        }
    }
}
