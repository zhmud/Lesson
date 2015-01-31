using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2.OOP_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction one = new Fraction(4, 5);
            Fraction two = new Fraction(2, 3);
            Fraction rez = new Fraction();
            rez = one - two;
            rez.PrintMixedFraction();
        }
    }

    class Fraction
    {
        private int m_Denominator;
        private int m_Numerator;
        public Fraction()
        {
            m_Denominator = 1;
            m_Numerator = 1;
        }
        public Fraction(double value)
        {
            m_Numerator = (int)value * CounterDigit(value);
            m_Denominator = CounterDigit(value);
        }
        public Fraction(int numerator, int denominator)
        {
            m_Numerator = numerator;
            if (denominator != 0)
                m_Denominator = denominator;
            else
                Console.WriteLine("Denominator != 0");
        }

        public static bool operator <(Fraction first, Fraction second)
        {
            if (first.ReturnValueInDouble() < second.ReturnValueInDouble())
                return true;
            else
                return false;
        }
        public static bool operator <(Fraction first, double second)
        {
            if (first.ReturnValueInDouble() < second)
                return true;
            else
                return false;
        }
        public static bool operator <(double first, Fraction second)
        {
            if (first < second.ReturnValueInDouble())
                return true;
            else
                return false;
        }

        public static bool operator >(Fraction first, Fraction second)
        {
            if (first.ReturnValueInDouble() > second.ReturnValueInDouble())
                return true;
            else
                return false;
        }
        public static bool operator >(Fraction first, double second)
        {
            if (first.ReturnValueInDouble() > second)
                return true;
            else
                return false;
        }
        public static bool operator >(double first, Fraction second)
        {
            if (first > second.ReturnValueInDouble())
                return true;
            else
                return false;
        }

        public static bool operator <=(Fraction first, Fraction second)
        {
            if (first.ReturnValueInDouble() <= second.ReturnValueInDouble())
                return true;
            else
                return false;
        }
        public static bool operator <=(Fraction first, double second)
        {
            if (first.ReturnValueInDouble() <= second)
                return true;
            else
                return false;
        }
        public static bool operator <=(double first, Fraction second)
        {
            if (first <= second.ReturnValueInDouble())
                return true;
            else
                return false;
        }

        public static bool operator >=(Fraction first, Fraction second)
        {
            if (first.ReturnValueInDouble() >= second.ReturnValueInDouble())
                return true;
            else
                return false;
        }
        public static bool operator >=(Fraction first, double second)
        {
            if (first.ReturnValueInDouble() >= second)
                return true;
            else
                return false;
        }
        public static bool operator >=(double first, Fraction second)
        {
            if (first >= second.ReturnValueInDouble())
                return true;
            else
                return false;
        }

        public static bool operator ==(Fraction first, Fraction second)
        {
            if (first.ReturnValueInDouble() == second.ReturnValueInDouble())
                return true;
            else
                return false;
        }
        public static bool operator ==(Fraction first, double second)
        {
            if (first.ReturnValueInDouble() == second)
                return true;
            else
                return false;
        }
        public static bool operator ==(double first, Fraction second)
        {
            if (first == second.ReturnValueInDouble())
                return true;
            else
                return false;
        }

        public static bool operator !=(Fraction first, Fraction second)
        {
            if (first.ReturnValueInDouble() != second.ReturnValueInDouble())
                return true;
            else
                return false;
        }
        public static bool operator !=(Fraction first, double second)
        {
            if (first.ReturnValueInDouble() != second)
                return true;
            else
                return false;
        }
        public static bool operator !=(double first, Fraction second)
        {
            if (first != second.ReturnValueInDouble())
                return true;
            else
                return false;
        }

        public static Fraction operator +(Fraction first, Fraction second)
        {
            Fraction result = new Fraction();
            result.m_Denominator = result.CommonFactor(first.GetDenominator(), second.GetDenominator());         
            result.m_Numerator = first.m_Numerator * (result.m_Denominator / first.m_Denominator) + second.m_Numerator * (result.m_Denominator / second.m_Denominator);
            result.Reduction();
            return result;
        }

        public static Fraction operator -(Fraction first, Fraction second)
        {
            Fraction result = new Fraction();
            result.m_Denominator = result.CommonFactor(first.GetDenominator(), second.GetDenominator());
            result.m_Numerator = first.m_Numerator * (result.m_Denominator / first.m_Denominator) - second.m_Numerator * (result.m_Denominator / second.m_Denominator);
            result.Reduction();
            return result;
        }

        public static Fraction operator *(Fraction first, Fraction second)
        {
            Fraction result = new Fraction();
            result.m_Denominator = first.m_Denominator * second.m_Denominator;
            result.m_Numerator = first.m_Numerator * second.m_Numerator;
            result.Reduction();
            return result;
        }
        public static Fraction operator /(Fraction first, Fraction second)
        {
            Fraction result = new Fraction();
            result.m_Denominator = first.m_Denominator * first.m_Numerator;
            result.m_Numerator = first.m_Numerator * first.m_Denominator;
            result.Reduction();
            return result;
        }

        public int GetDenominator()
        {
            return m_Denominator;
        }
        public int GetNumerator()
        {
            return m_Numerator;
        }
        public void SetDenominator(int value)
        {
            if (value != 0)
                m_Denominator = value;
        }
        public void SetNumerator(int value)
        {
            m_Numerator = value;
        }

        public void PrintMixedFraction()
        {
            int temp = (int)(m_Numerator / m_Denominator);
            if (temp == 0)
                Console.WriteLine("{0} {1}/{2}", 1, m_Numerator, m_Denominator);
            else 
                Console.WriteLine("{0} {1}/{2}", temp , m_Numerator % temp, m_Denominator);
        }
        public void PrintFraction()
        {
            Console.WriteLine(m_Numerator + "/" + m_Denominator);
        }
        public double ReturnValueInDouble()
        {
            return (double)m_Numerator / m_Denominator;
        }

        public void Reduction()
        {
            int divisor;
            if (m_Numerator < m_Denominator)
                divisor = m_Denominator;
            else
                divisor = m_Numerator;
            while (divisor > 0)
            {
                if ((m_Numerator % divisor) == 0 && (m_Denominator % divisor) == 0)
                {
                     m_Numerator /= divisor;
                     m_Denominator /= divisor;
                    return;
                }
                divisor--;
            }
        }
        private int CommonFactor(int first, int second)
        {
            int divisor = 1;
            while(true)
            {
                if (divisor % first == 0 && divisor % second == 0)
                    return divisor;
                else
                    divisor++;
            }
        }
        private int CounterDigit(double value)
        {
            int multiplier = 10;
            for (int i = 0; (int)value == value; i++, multiplier *= 10) ;
            return multiplier;
        }
    }
}
