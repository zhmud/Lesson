using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Radio_Player
{
    class Program
    {
        [DllImport(@"MouseInConsole.dll")]
        static extern int ReCord(ref int X, ref int Y);
        static void Main(string[] args)
        {
            Mutex mut = new Mutex();
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ConsoleMediaPlayer CMP = new ConsoleMediaPlayer(10, 2);
            CMP.mut = mut;
            Radio temp = new Radio(10, 10);
            temp.Show();
            CMP.Show();
            int X = 0;
            int Y = 0;
            while (true)
            {
                int click = ReCord(ref X, ref Y);
                CMP.Event(X, Y, click);
                mut.WaitOne();
                temp.Event(X, Y, click);
                mut.ReleaseMutex();
            }
        }
    }
}
