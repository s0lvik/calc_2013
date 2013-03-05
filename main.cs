using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calc
{
    class main
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****************************************************");
            Console.WriteLine("********* MiniCalc, Writen by S0lv1k ****************");
            Console.WriteLine("*****************************************************");
            Console.WriteLine("");
            Console.Write("Введите выражение: ");
            readKeys();        
        }

        public static string recounter(char symbol, string numberString)
        {
            string result = "";

            int starter = 0;
            int ender = numberString.Length - 1;
            int sign = 0;
            int sign2 = 0;

            bool firstMinus = false;

            string substr = numberString;
            if (numberString[0] == '-') { substr = numberString.Substring(1); firstMinus = true; }

            while (substr.IndexOf(symbol) > 0)
            {
                
                if (substr.IndexOf(symbol) > 0)
                {
                    sign = substr.IndexOf(symbol);

                    if (firstMinus) sign = sign + 1;

                    for (var i = sign - 1; i >= 0; i--)
                    {                    
                        if ((numberString[i] == '*') ||
                            (numberString[i] == '/') ||
                            (numberString[i] == '+') ||
                            (numberString[i] == '-') ||
                            (numberString[i] == '^'))
                        {
                            if (numberString[i] == '-') starter = i;
                            else starter = i + 1;
                            break;
                        }
                    }

                    for (var i = sign + 1; i < numberString.Length - 1; i++)
                    {
                        if (numberString[sign + 1] == '-')
                        {
                            sign2 = sign + 1;
                        }
                        else
                        if ((numberString[i] == '*') ||
                            (numberString[i] == '/') ||
                            (numberString[i] == '+') ||
                            (numberString[i] == '-') ||
                            (numberString[i] == '^'))
                        {
                            ender = i - 1;
                            break;
                        }
                    }
                }

                string strNumber1 = numberString.Substring(starter, (sign - starter));

                string strNumber2 = numberString.Substring((sign + 1), ender - sign);

                if (sign2 != 0) strNumber2 = "-" + numberString.Substring((sign2 + 1), ender - sign2);

                //Console.WriteLine("Первое число: " + strNumber1 + ", а второе: " + strNumber2);

                if ((strNumber1 == "") || (strNumber2 == "")) { Console.WriteLine("Допущена ошибка!"); break; }



                if (symbol == '^')
                    result = Convert.ToString(Math.Pow(Convert.ToDouble(strNumber1), Convert.ToDouble(strNumber2)));
                else if (symbol == '*')
                    result = Convert.ToString(Convert.ToDouble(strNumber1) * Convert.ToDouble(strNumber2));
                else if (symbol == '/')
                    result = Convert.ToString(Convert.ToDouble(strNumber1) / Convert.ToDouble(strNumber2));
                else if (symbol == '+')
                    result = Convert.ToString(Convert.ToDouble(strNumber1) + Convert.ToDouble(strNumber2));
                else if (symbol == '-')
                    result = Convert.ToString(Convert.ToDouble(strNumber1) - Convert.ToDouble(strNumber2));


                Console.WriteLine(strNumber1 + symbol + strNumber2 + " = " + result);

                if ((Convert.ToDouble(result) > 0) && (Convert.ToDouble(strNumber1) < 0) && (symbol == '^'))
                    result = "-" + result; 

                //Console.WriteLine("Заменяем " + strNumber1 + symbol + strNumber2 + " на " + result);
                numberString = numberString.Replace(strNumber1 + symbol + strNumber2, result);


                starter = 0;
                ender = numberString.Length - 1;
                sign = 0;
                sign2 = 0;

                substr = numberString;
                if (numberString[0] == '-') { substr = numberString.Substring(1); firstMinus = true; }

            }

            return numberString;
        }

        public static string tryToCalc(string numberString)
        {
            numberString = recounter('^', numberString);
            numberString = recounter('*', numberString);
            numberString = recounter('/', numberString);
            numberString = recounter('+', numberString);
            numberString = recounter('-', numberString);

            Console.WriteLine();

            return numberString;
        }

        public static void workWithThisString(string numberString)
        {
            int starter = 1;
            int ender = 1;

            //Console.WriteLine("Происходит рассчет...");

            Console.WriteLine();
            Console.WriteLine();

            while (numberString.IndexOf(')') > 0)
            {
                if (numberString.IndexOf(')') > 0)
                {

                    ender = numberString.IndexOf(')');


                    for (var i = ender; i >= 0; i--)
                    {
                        if (numberString[i] == '(')
                        {
                            starter = i;
                            break;
                        }
                    }
                }

                //Console.WriteLine("Значение из последних вложенных скобок: {0}", numberString.Substring(starter + 1, (ender - starter - 1)));

                string innerNumber = numberString.Substring(starter + 1, (ender - starter - 1));

                //Console.WriteLine("Результат выражения в скобках = {0}", tryToCalc(innerNumber));

                // Преобразовывваем выражения, путем избавления его от лишних скобок
                numberString = numberString.Replace(numberString.Substring(starter, (ender - starter + 1)), tryToCalc(innerNumber));

                //Console.WriteLine("Начальное выражение преобразовано в следующее: {0}", numberString);
            }

            // финальный рассчет
            Console.WriteLine("Результат выражения = {0}", tryToCalc(numberString));

            Console.WriteLine();
            Console.Write("Введите выражение: ");
            readKeys();
        }

        public static string readKeys()
        {
            string result = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        if (result.Length > 0)
                        {
                            result = result.Remove(result.Length - 1, 1);
                            Console.Write(key.KeyChar + " " + key.KeyChar);
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (result != "") workWithThisString(result);
                        return result;
                    default:
                        if ((char.IsDigit(key.KeyChar)) || (key.KeyChar == '^') || (key.KeyChar == '*') || (key.KeyChar == '/')
                             || (key.KeyChar == '+') || (key.KeyChar == '-') || (key.KeyChar == '(') || (key.KeyChar == ')'))
                        {
                            Console.Write(key.KeyChar);
                            result += key.KeyChar;
                        }
                        break;
                }
            }
        }
    }
}
