using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Radio_Player
{
    class Program
    {
        [DllImport(@"MouseInConsole.dll")]
        static extern int ReCord(ref int X, ref int Y);
        public delegate bool MethodContainer(int x, int y, int click = 4);
        static event MethodContainer MouseEvent;
        static void Main(string[] args)
        {
            Tab RadioTab = new Tab("Радио", 0);
            Tab TextTab = new Tab("Текст", 8);
            Tab ListTab = new Tab("Скаченные песни", 16);
            MouseEvent += RadioTab.Event;
            MouseEvent += TextTab.Event;
            MouseEvent += ListTab.Event;

            Console.SetWindowSize(80, 50);
            GlobalMutex mutex = GlobalMutex.CreateMutex();
            Console.CursorVisible = false;
            
            Radio radio = new Radio();
            Song s = new Song(@"http://o.tavrmedia.ua:9561/get/?k=kiss&callback=?");
            s.NewSong += radio.m_CMP.ShowMusic;
            radio.NewAddress += s.SetAddress;
            
            int status = -1;
            int X = 0;
            int Y = 0;
            bool firstRun = true;
            while (true)
            {              
                int click = ReCord(ref X, ref Y);
                GlobalMutex.GetMutex.WaitOne();

                MouseEvent(X, Y, click);
                if ((RadioTab.Status == 2 && status != 0) || firstRun)
                {
                    if (firstRun)
                    {
                        firstRun = false;
                        RadioTab.Status = 2;
                    }
                    Console.Clear();
                    
                    status = 0;
                    TextTab.Status = 0;
                    ListTab.Status = 0;
                    
                    radio.StartShow();
                    MouseEvent += radio.Event;

                    TextTab.Update((ModeButton)TextTab.Status);
                    RadioTab.Update((ModeButton)RadioTab.Status);
                    ListTab.Update((ModeButton)ListTab.Status);

                }
                else if (TextTab.Status == 2 && status != 1)
                {  
                    radio.StopShow();
                    Console.Clear();

                    status = 1;
                    RadioTab.Status = 0;
                    ListTab.Status = 0;

                    MouseEvent -= radio.Event;

                    TextTab.Update((ModeButton)TextTab.Status);
                    RadioTab.Update((ModeButton)RadioTab.Status);
                    ListTab.Update((ModeButton)ListTab.Status);
                    
                }
                else if (ListTab.Status == 2 && status != 2)
                {
                    radio.StopShow();
                    Console.Clear();

                    status = 2;
                    TextTab.Status = 0;
                    RadioTab.Status = 0;

                    MouseEvent -= radio.Event;

                    TextTab.Update((ModeButton)TextTab.Status);
                    RadioTab.Update((ModeButton)RadioTab.Status);
                    ListTab.Update((ModeButton)ListTab.Status);
                }
                GlobalMutex.GetMutex.ReleaseMutex();
            }
        }
    }
}
