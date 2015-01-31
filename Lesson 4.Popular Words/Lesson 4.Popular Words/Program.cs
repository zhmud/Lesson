using System;
using System.IO;
using System.Text;
using System.Collections;

namespace Lesson_4.Popular_Words
{
    class Program
    {
        static void Main(string[] args)
        {
            PopularWords master = new PopularWords(@"master.txt"); // lt1.txt - война и мир, master.txt - мастер и маргарита
            master.ReadText();
            master.DeleteSymbol();
            master.Analize();
            master.Print();
        }

        class PopularWords
        {
            private string m_Path;
            private string m_Text;
            SortedList m_words;

            public string Path
            {
                get { return m_Path; }
                set { m_Path = value; }
            }

            public string Text
            {
                get { return m_Text; }
                set { m_Text = value; }
            }

            public PopularWords(string path)
            {
                m_Path = path;
                m_words = new SortedList();
            }

            public void ReadText()
            {
                FileStream fs = new FileStream(m_Path, FileMode.Open, FileAccess.Read);
                byte[] readBytes = new byte[fs.Length];
                fs.Read(readBytes, 0, readBytes.Length);
                m_Text = Encoding.Default.GetString(readBytes);
                fs.Close();
            }

            public void DeleteSymbol()
            {
                string[] symb = {",", "-", "(", ")", ".", "!", "?", "%", ":", ";", "\"", "*", "'", "<", ">" };
                for (int i = 0; i < symb.Length; i++)
                {
                    m_Text = m_Text.Replace(symb[i], " ");
                }
            }
            public void Analize()
            {
                m_Text = m_Text.ToLower();
                m_Text = System.Text.RegularExpressions.Regex.Replace(m_Text, @"\s+", " ");
                string[] words = m_Text.Split(' ');              
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length >= 3)
                    {
                        if(!m_words.ContainsKey(words[i]))
                        {
                            m_words.Add(words[i], 1);
                        }
                        else
                        {
                            int index = m_words.IndexOfKey(words[i]);
                            int value = Convert.ToInt32(m_words.GetByIndex(index)) + 1;
                            m_words.SetByIndex(index, value);
                        }
                    }
                }
            }

            public void Print()
            {
                for (int k = 0; k < 100; k++)
                {
                    int max = 0;
                    int index = 0;
                    for (int i = 0; i < m_words.Count; i++)
                        if (Convert.ToInt32(m_words.GetByIndex(i)) > max)
                        {
                            max = Convert.ToInt32(m_words.GetByIndex(i));
                            index = i;
                        }
                    Console.WriteLine("Key : {0}  \tvalue : {1}", m_words.GetKey(index), m_words.GetByIndex(index));
                    m_words.RemoveAt(index);
                }
            }
        }
    }
}
