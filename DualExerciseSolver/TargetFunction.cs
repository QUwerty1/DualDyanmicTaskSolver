namespace DualExerciseSolver;

public class TargetFunction
{
    public decimal[] Coefficients { get; set; } = [];
    public bool IsStrivesToMax { get; set; }

    public TargetFunction()
    {
        InputArray();
    }

    public void InputArray() {
        bool convertedRight = false;
        do
        {
            try
            {

                Console.WriteLine("Введите коэфициенты целевой функции через пробел:");
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
        Console.WriteLine("Функция стремится к максимуму? (Y/n):");
        string isMax = Console.ReadLine() ?? "";
        if (isMax == "n")
            IsStrivesToMax = false;
        else
            IsStrivesToMax = true;
    }

    public void MakeStandart()
    {
        if (IsStrivesToMax == false)
        {
            IsStrivesToMax = true;
            for (int i = 0; i < Coefficients.Length; i++)
            {
                Coefficients[i] *= -1;
            }
        }
    }

}
