using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radio_Player
{
    class Tab
    {
        public delegate void Action();
        public event Action Show; 

        private string[] m_Title;
        private int m_Counter;
        private int m_number;

        public void Add(string title)
        {
            m_Counter++;
            m_Title[m_Counter] = title + (int)9474;
        }

        public Tab()
        {
            m_Title = new string[8];
            m_Counter = 0;
            m_number = 0;
        }

        private void Separator()
        {
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < 80; i++)
                Console.Write((int)9472);
        }

    }
}
