using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2.OOP_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter(2, 11);
            c.SetMin(2);
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("     ");
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(c.GetCurrent());
                Console.ReadKey();
                c.Increase();
            }
        }
    }
    class Counter
    {
        private int m_current;
        private int m_Min;
        private int m_Max;
        private int m_step;
        public Counter()
        {
            m_current = 0;
            m_Min = 0;
            m_Max = 10;
            m_step = 1;
        }
        public Counter(int step, int max)
        {
            m_current = 0;
            m_Min = 0;
            m_Max = max;
            m_step = step;
        }
        public Counter(int current, int min, int max, int step)
        {
            if (current >= min && current <= max)
                 m_current = current;
            else
                 m_current = 0;
            m_Min = min;
            m_Max = max;
            m_step = step;

        }
        public void Increase()
        {
            m_current = (m_current + m_step) % m_Max;
            if (m_current < m_Min)
                m_current += m_Min;
        }
        public void Increment()
        {
            m_current = (m_current + 1) % m_Max;
            if (m_current < m_Min)
                m_current += m_Min;
        }
        public void Reset()
        {
            m_current = 0;
        }
        public void SetMax(int value)
        {
            if (value > m_Min)
            {
                m_Max = value;
                if (m_current > m_Max)
                    m_current = m_Max;
            }
            else
                Console.WriteLine("Error");
        }
        public void SetMin(int value)
        {
            if (value < m_Max)
            {
                m_Min = value;
                if (m_current < m_Min)
                    m_current = m_Min;
            }
            else
                Console.WriteLine("Error");
        }
        public int GetCurrent()
        {
            return m_current;
        }
    }
}
