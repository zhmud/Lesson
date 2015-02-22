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
            Tab ListTab = new Tab("Скачанные песни", 16);
            MouseEvent += RadioTab.Event;
            MouseEvent += TextTab.Event;
            MouseEvent += ListTab.Event;

            Tab Download = new Tab("Скачать", 71);
            MouseEvent += Download.Event; ;
            

            Console.SetWindowSize(80, 50);
            GlobalMutex mutex = GlobalMutex.CreateMutex();
            Console.CursorVisible = false;

            TextList textlist = new TextList();
            Radio radio = new Radio();
            textlist.m_Song.NewSong += radio.m_CMP.ShowMusic;
            radio.NewAddress += textlist.m_Song.SetAddress;
            textlist.m_Song.downloadTab = Download;
            
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
                    textlist.StopShow();
                    Console.Clear();
                    
                    status = 0;
                    TextTab.Status = 0;
                    ListTab.Status = 0;
                    
                    radio.StartShow();
                    MouseEvent += radio.Event;
                    MouseEvent -= textlist.Event;

                    TextTab.Update((ModeButton)TextTab.Status);
                    RadioTab.Update((ModeButton)RadioTab.Status);
                    ListTab.Update((ModeButton)ListTab.Status);
                    Download.Update((ModeButton)Download.Status);

                }
                else if (TextTab.Status == 2 && status != 1)
                {  
                    radio.StopShow();
                    Console.Clear();

                    status = 1;
                    RadioTab.Status = 0;
                    ListTab.Status = 0;

                    textlist.Updata();
                    MouseEvent += textlist.Event;
                    MouseEvent -= radio.Event;

                    TextTab.Update((ModeButton)TextTab.Status);
                    RadioTab.Update((ModeButton)RadioTab.Status);
                    ListTab.Update((ModeButton)ListTab.Status);
                    Download.Update((ModeButton)Download.Status);                  
                }
                else if (ListTab.Status == 2 && status != 2)
                {
                    radio.StopShow();
                    textlist.StopShow();
                    Console.Clear();

                    status = 2;
                    TextTab.Status = 0;
                    RadioTab.Status = 0;

                    MouseEvent -= radio.Event;
                    MouseEvent -= textlist.Event;

                    TextTab.Update((ModeButton)TextTab.Status);
                    RadioTab.Update((ModeButton)RadioTab.Status);
                    ListTab.Update((ModeButton)ListTab.Status);
                    Download.Update((ModeButton)Download.Status);
                }
                if (Download.Status == 2)
                {
                    if (textlist.m_Song.m_counter > 0)
                        textlist.m_Song.DownloadSong();
                    else
                    {
                        Download.Status = 0;
                        Download.Update(0);
                    }
                }
                GlobalMutex.GetMutex.ReleaseMutex();
            }
        }
    }
}
