using System;
using System.Runtime.InteropServices; 

namespace Radio_Player
{
    class Program
    {
        [DllImport(@"MouseInConsole.dll")]
        static extern int ReCord(ref int X, ref int Y);
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ConsoleMediaPlayer CMP = new ConsoleMediaPlayer(10, 2);
            CMP.Show();
            int X = 0;
            int Y = 0;
            while (true)
            {
                int click = ReCord(ref X, ref Y);
                CMP.Event(X, Y, click);
            }
        }
    }
}
