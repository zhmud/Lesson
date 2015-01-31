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
            double value;
            value  = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(CommonFactor(25,10));
        }
        static int CommonFactor(int first, int second)
        {
            int divisor;
            if (first < second)
                divisor = first;
            else
                divisor = second;
            while (divisor > 0)
            {
                if ((first % divisor) == 0 && (second % divisor) == 0)
                    return divisor;
                divisor--;
            }
            return 1;
        }
        static int CounterDigit(double value)
        {
            int multiplier = 1;
            for (int i = 0; (int)value != value; i++, multiplier *= 10)
                value *= 10;
            return multiplier;
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
            int divisor = result.CommonFactor(first.GetDenominator(), second.GetDenominator());            
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
            Console.WriteLine("{0} {1}//{2}", (int)(m_Numerator / m_Denominator), m_Numerator, m_Denominator);
        }
        public void PrintFraction()
        {
            Console.WriteLine(m_Numerator / m_Denominator);
        }
        public double ReturnValueInDouble()
        {
            return m_Numerator / m_Denominator;
        }
        public void Reduction()
        {
            int temp = CommonFactor(m_Numerator, m_Denominator);
            m_Numerator /= temp;
            m_Denominator /= temp;
        }
        private int CommonFactor(int first, int second)
        {
            int divisor;
            if (first < second)
                divisor = first;
            else
                divisor = second;
            while (divisor > 0)
            {
                if ((first % divisor) == 0 && (second % divisor) == 0)
                    return divisor;
                divisor--;
            }
            return 1;
        }
        private int CounterDigit(double value)
        {
            int multiplier = 10;
            for (int i = 0; (int)value == value; i++, multiplier *= 10) ;
            return multiplier;
        }
    }
}
