using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace DualExerciseSolver;

public class Limitation
{
    public string Sign { get; set; } = ">=";
    public decimal[] Coefficients { get; set; } = [];
    public decimal RightLimitation { get; set; }

    public Limitation()
    {
        InputLimit();
    }

    public void InputLimit()
    {
        bool convertedRight = false;
        do
        {
            try
            {

                Console.WriteLine("Введите коэфициенты ограничения через пробел:");
                string[] line = Console.ReadLine()!.Replace('.', ',').Split(' ');
                Coefficients = new decimal[line.Length];
                for (int i = 0; i < line.Length; i++)
                {
                    Coefficients[i] = Convert.ToDecimal(line[i]);
                }
                convertedRight = true;
            }
            catch (Exception ex) when (ex is OverflowException or FormatException)
            {
                Console.WriteLine("Строка введена неверно");
            }
        } while (!convertedRight);

        string sign = "";
        do
        {
            Console.WriteLine("Введите знак ограничения:");
            sign = Console.ReadLine()!;
        } while (sign != ">=" && sign != "<=" && sign != "=");
        Sign = sign;

        convertedRight = false;
        do
        {
            Console.WriteLine("Введите свободный коэффициент:");
            try
            {
                RightLimitation = Convert.ToDecimal(Console.ReadLine());
                convertedRight = true;
            }
            catch (Exception ex) when (ex is FormatException or OverflowException)
            {
                Console.WriteLine("Значение введено неверно");
            }
        } while (!convertedRight);
    }

    public void MakeStandart()
    {
        if (Sign == ">=")
        {
            for (int i = 0; i < Coefficients.Length; i++)
            {
                Coefficients[i] *= -1;
            }
            RightLimitation *= -1;
            Sign = "<=";
        }
    }
}
