using System;
using System.Linq;

namespace Project_01_NumeralSystems
{
    class Program
    {
        public static string ToDecimal(string userInput, int b) // b - base
        {
            string fraction = "";
            double number = 0, digit = 0, power;
            var abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

            if (userInput.Contains(","))
            {
                fraction = userInput.Substring(userInput.IndexOf(','));
                userInput = userInput.Remove(userInput.IndexOf(','));
            }
            if (b == 8 && (userInput.Contains("8") || (userInput.Contains("9") || !userInput.All(char.IsDigit))))
            {
                throw new Exception("Invalid number");
            }


            power = userInput.Length - 1;
            userInput += fraction;
            for (int i = 0; i < userInput.Length; i++)
            {
                if(char.IsDigit(userInput[i]))
                {
                    digit = double.Parse(userInput[i].ToString());
                }
                else if(userInput[i] == ',')
                {
                    continue;
                }
                else
                {
                    digit = 10 + Array.IndexOf(abc, char.ToUpper(userInput[i]));
                    if(digit >= b)
                    {
                        throw new Exception("Incorrect number");
                    }
                }

                number += digit * Math.Pow(b, power);
                power--;
            }

            return number.ToString();
        }

        public static int BaseDetector(string num)      // 1 - for decimal, 2 - for octal, 3 - for hexadecimal;
        {
            char[] numArray = num.ToCharArray();

            if (numArray[0] == '0')
            {
                if (numArray[1] == 'x')
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
            }
        }

        public static string ConvertDecimal(string userInput, int b) // b - base
        {
            string answer = "", fraction = "";
            int whole, remain;
            var abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
            bool addFraction = false;

            if(userInput.Contains(","))
            {
                addFraction = true;
                fraction = "0" + userInput.Substring(userInput.IndexOf(','));
                userInput = userInput.Remove(userInput.IndexOf(','));
                if (!fraction.Substring(2).All(char.IsDigit))
                {
                    throw new Exception("Invalid number");
                }
            }

            if (!userInput.All(char.IsDigit))
            {
                throw new Exception("Invalid number");
            }

            whole = int.Parse(userInput);
            while (whole != 0)
            {
                remain = whole % b;
                whole /= b;
                if (remain > 9)
                {
                    answer += abc[remain - 10];
                }
                else 
                {
                    answer += remain; 
                }
            }

            answer = Reverse(ref answer);
            if (addFraction)
            {
                answer += "," + ConvertDecimalFraction(double.Parse(fraction), b);
            }

            return answer;
        }

        public static string ConvertDecimalFraction(double userInput, int b) // b -base
        {
            string answer = "";
            var abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

            while (!(userInput == 0))
            {
                userInput *= b;
                if (userInput >= 10)
                {
                    answer += abc[Convert.ToInt32(userInput)-10].ToString();
                }
                else
                {
                    answer += Math.Floor(userInput).ToString();
                }
                if(userInput > 0)
                {
                    userInput %= 1;
                }
            }

            return answer;

        }

        public static string Reverse(ref string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static void Main(string[] args)
        {
            string dec="", bin="", oct="", hex="";

            Console.WriteLine("Type a number(use coma(,) between integer and fraction part): ");
            string userInput = Console.ReadLine();

            switch(BaseDetector(userInput)) // 1 - for decimal, 2 - for octal, 3 - for hexadecimal;
            {
                case 1:
                    {
                        dec = userInput;
                        bin = ConvertDecimal(dec, 2);
                        oct = ConvertDecimal(dec, 8);
                        hex = ConvertDecimal(dec, 16);
                        break;
                    }
                case 2:
                    {
                        dec = ToDecimal(userInput.Substring(1), 8);
                        bin = ConvertDecimal(dec, 2);
                        oct = userInput.Substring(1);
                        hex = ConvertDecimal(dec, 16);
                        break;
                    }
                case 3:
                    {
                        dec = ToDecimal(userInput.Substring(2), 16);
                        bin = ConvertDecimal(dec, 2);
                        oct = ConvertDecimal(dec, 8);
                        hex = userInput.Substring(2);
                        break;
                    }
            }

            Console.WriteLine("Decimal: " + dec);
            Console.WriteLine("Binary: " + bin);
            Console.WriteLine("Octal: 0" + oct);
            Console.WriteLine("Hexadecimal: 0x" + hex);

        }
    }
}
