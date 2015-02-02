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
            RadioList r = new RadioList("RadioList.txt");
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ConsoleMediaPlayer CMP = new ConsoleMediaPlayer(10, 2);
            Song s = new Song(@"http://o.tavrmedia.ua:9561/get/?k=kiss&callback=?");
            s.mut = CMP.mut;
            Thread t = new Thread(CMP.ShowStatus);
            t.Start();
            s.NewSong += CMP.ShowMusic;
            CMP.Show();
            int X = 0;
            int Y = 0;
            while (true)
            {              
                int click = ReCord(ref X, ref Y);
                CMP.mut.WaitOne();
                CMP.Event(X, Y, click);
                int index = r.Event(X, Y, click);
                if (index != -1)
                {
                    CMP.m_WMP.controls.stop();
                    CMP.URL = r.GetRadio(index).UrlStream;
                    s.Address = r.GetRadio(index).WapPageAddress;
                    CMP.Show();
                }
                CMP.mut.ReleaseMutex();
            }
        }
    }
}
