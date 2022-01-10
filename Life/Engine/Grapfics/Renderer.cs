using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Engine.Grapfics
{
    public class Renderer
    {
        public void Render(Canvas canvas)
        {
            char[] buffer = canvas.GetBuffer();

            //SetDefaultTheme();
            Console.SetCursorPosition(0, 0);
            int j = canvas.Width;
            for (int i = 0; i < buffer.Length; i++)
            {
                Console.Out.Write(buffer[i]);
                j--;
                if (j == 0)
                {
                    j = canvas.Width;
                    Console.Out.Write('\n');
                }
            }
        }
        public void SetDefaultTheme()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

    }
}
