using System;
using WMPLib;
using System.Threading;

namespace Radio_Player
{
    class ConsoleMediaPlayer: Window
    {
        private string m_RadioStation;
        private string m_Singer;
        private string m_Song;
        private string m_URL;
        private string m_Path;
        private int m_Volume;
        private int cout = 0;
        private WindowsMediaPlayer m_WMP;
        private Button m_PlayOrStop;
        private Slider m_Slider;
        private Timer timer;
        public  Mutex mut;

        public string RadioStation
        {
            get { return m_RadioStation; }
            set { m_RadioStation = value; }
        }
        public string Singer
        {
            get { return m_Singer; }
            set { m_Singer = value; }
        }
        public string Song
        {
            get { return m_Song; }
            set { m_Song = value; }
        }
        public string URL
        {
            get { return m_URL; }
            set { m_URL = value; }
        }
        public string Path
        {
            get { return m_Path; }
            set { m_Path = value; }
        }
        public int Volume
        {
            get { return m_Volume; }
            set { m_Volume = value; }
        }

        public ConsoleMediaPlayer(int left, int top)
        {
            cout++; 
            Left = left;
            Top = top;
            Width = 60;
            Height = 6;
            m_RadioStation = "";
            m_Singer = "";
            m_Song = "";
            m_WMP = new WindowsMediaPlayer();
            m_WMP.URL = @"http://online-hitfm.tavrmedia.ua/HitFM"; 
            m_PlayOrStop = new Button(Left + 1, Top + 1, (char)9608 + "");
            m_Slider = new Slider(Left + 16, Top + 4, 40, 30);
            m_WMP.settings.autoStart = true;
            ShowVolue();         
        }

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WindowShow();
            m_PlayOrStop.Show();
            m_Slider.Show();
            ShowVolue();
            ShowRadioStation();
            ShowMusic();
            timer = new Timer(ShowStatus, null, 0, 500);
            
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void ShowVolue()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(Left + 12, Top + 4);
            Console.Write("    ");
            Console.SetCursorPosition(Left + 1, Top + 4);
            m_Volume = Convert.ToInt32((double)(m_Slider.Position - 1)  / (m_Slider.Length - 1) * 100);
            Console.Write("Громкость : {0}", m_Volume);
        }
        private void ShowRadioStation()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(Left + 8, Top + 1);
            Console.Write("Радио станция : {0}", m_WMP.currentMedia.name);
        }
        private void ShowStatus(object data)
        {
            mut.WaitOne();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(Left + 8, Top + 3);
            Console.Write("                                                 ");
            Console.SetCursorPosition(Left + 8, Top + 3);
            Console.Write(m_WMP.status);
            if (m_WMP.status == "Буферизация")
            {
                m_WMP.controls.stop();
                Thread.Sleep(500);
                m_WMP.controls.play();
            }
            mut.ReleaseMutex();
        }

        private void ShowMusic()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(Left + 8, Top + 2);
            Console.Write("Музыка : {0} - {1}", m_Singer, m_Song);
        }

        public void Event(int x, int y, int click = 4)
        {
            mut.WaitOne();
            if (m_Slider.Event(x, y, click))
            {
                ShowVolue();
                m_WMP.settings.volume = m_Volume;
            }
            if (m_PlayOrStop.Event(x, y, click))
            {
                if (m_PlayOrStop.Caption == (char)9608 + "")
                {
                    m_PlayOrStop.Caption = (char)9658 + "";
                    m_WMP.controls.stop();
                   // m_WMP.controls.pause();
                }
                else
                {
                    m_PlayOrStop.Caption = (char)9608 + "";
                    m_WMP.controls.play();
                }
            }
            mut.ReleaseMutex();
        }

    }
}
