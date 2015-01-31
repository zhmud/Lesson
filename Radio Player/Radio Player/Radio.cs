using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radio_Player
{
    class Radio : Window
    {
        string m_title;
        string m_urlStream;
        string m_wapPageAddress;
        int m_status = 0; // 0 - normal, 1 - move, 2 - click

        public string UrlStream
        {
            get { return m_urlStream; }
            set { m_urlStream = value; }
        }

        public string WapPageAddress
        {
            get { return m_wapPageAddress; }
            set { m_wapPageAddress = value; }
        }

        public Radio()
        {
            m_title = "title";
            Left = 0;
            Top = 0;
            Width = 16;
            Height = 3;
        }
        public Radio(int left, int top)
        {
            m_title = "title";
            Left = left;
            Top = top;
            Width = 20;
            Height = 3;       
        }

        public void Show(ModeButton Mode = ModeButton.Normal)
        {
            if (Mode == ModeButton.Normal)
                Console.ForegroundColor = ConsoleColor.White;
            else if (Mode == ModeButton.Move)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (Mode == ModeButton.Click)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            WindowShow();
            int space = (20 - m_title.Length) / 2;
            Console.SetCursorPosition(Left + 1 + space, Top + 1);
            Console.WriteLine(m_title);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public bool Event(int x, int y, int click = 4)
        {
            if (x >= Left && x <= Left + Width && y >= Top && y < Top + Height)
            {
                if (click == 1 && m_status != 2)
                {
                    Show(ModeButton.Click);
                    m_status = 2;
                    return true;
                }
                else if (m_status == 0)
                {
                    Show(ModeButton.Move);
                    m_status = 1;
                }
            }
            else if (m_status == 1)
            {
                Show(ModeButton.Normal);
                m_status = 0;
            }
            return false;
        }
    }

}
