using System;
using System.IO;
using System.Text;

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

        public RadioList(string path)
        {
            m_Path = path;
            FileRead();
            AnalysisFile();
        }

        public Radio GetRadio(int index)
        {
            return m_Radio[index];
        }

        private void FileRead()
        {
            FileStream fs = new FileStream(m_Path, FileMode.Open, FileAccess.Read);
            byte[] readBytes = new byte[fs.Length];
            fs.Read(readBytes, 0, readBytes.Length);
            m_Text = Encoding.Default.GetString(readBytes);
            fs.Close();
        }

        private void AnalysisFile()
        {
            string[] separators = { "Radio", ";", "title = ", "urlStream = ", "wapPageAddress = ", "\r\n" };
            string[] radio = m_Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            m_Counter = radio.Length / 3;
            m_Radio = new Radio[m_Counter];
            for(int i = 0; i < radio.Length; )
            {
                int index = (int)i / 3;
                m_Radio[index] = new Radio(9 + 21 * (index % 3), 10 + 3 * (index / 3));
                m_Radio[index].Title = radio[i]; i++;
                m_Radio[index].UrlStream = radio[i]; i++;
                m_Radio[index].WapPageAddress = radio[i]; i++;
                m_Radio[index].Show();
            }
        }

        public int Event(int x, int y, int click = 4)
        {
            for (int i = 0; i < m_Counter; i++)
            {
                if (m_Radio[i].Event(x, y, click))
                {
                    for (int j = 0; j < m_Counter; j++)
                        if (m_Radio[j].Status == 2 && j != i)
                            m_Radio[j].Status = 0;
                    return i;
                }
            }
            return -1;
        }
    }
}
