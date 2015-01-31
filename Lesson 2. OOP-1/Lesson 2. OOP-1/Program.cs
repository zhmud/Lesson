using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2.OOP_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point();
            p.ShowPoint("+"); 
            while(true)
            {
                ConsoleKeyInfo keypress = Console.ReadKey();
                p.ShowPoint();
                if (keypress.Key == ConsoleKey.UpArrow)
                    p.MoveUp();
                else if(keypress.Key == ConsoleKey.DownArrow)
                    p.MoveDown();
                else if (keypress.Key == ConsoleKey.LeftArrow)
                    p.MoveLeft();
                else if (keypress.Key == ConsoleKey.RightArrow)
                    p.MoveRight(); 
                p.ShowPoint("+");          
            }  
        }
    }

    class Point
    {
        private int m_X;
        private int m_Y;
        public Point()
        {
            m_X = 0;
            m_Y = 0;
        }
        public Point(int X, int Y)
        {
            m_X = X;
            m_Y = Y;
        }
        public int GetX()
        {
            return m_X;
        }
        public int GetY()
        {
            return m_Y;
        }
        public void SetX(int X)
        {
            m_X = X;
        }
        public void SetY(int Y)
        {
           m_Y = Y;
        }
        public void MoveUp(int value = 1)
        {
            m_Y -= value;
        }
        public void MoveDown(int value = 1)
        {
            m_Y += value;
        }
        public void MoveLeft(int value = 1)
        {
            m_X -= value;
        }
        public void MoveRight(int value = 1)
        {
            m_X += value;
        }
        public void ShowPoint(string str = " ")
        {
            if (m_X >= 0 && m_X <= 79 && m_Y >= 0 && m_Y < 300)
            {
                Console.SetCursorPosition(m_X, m_Y);
                Console.WriteLine(str);
            }
            else
                Console.WriteLine("Error");
        }
    }
}
