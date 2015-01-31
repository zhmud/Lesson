using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radio_Player
{
    class RadioList
    {
        private string m_Path;
        private string m_Text;
        private Radio[] m_Radio;
        private int m_Counter;

        public string Path
        {
            get { return m_Path; }
            set { m_Path = value; }
        }

    }
}
