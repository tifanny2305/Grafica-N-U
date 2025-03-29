using System;

namespace MyOpenTKApp
{
    class Program
    {
        static void Main()
        {
            using (FigureGame game = new FigureGame())
            {
                game.Run();
            }
        }
    }
}