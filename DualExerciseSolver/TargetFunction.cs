using System.Text;
using System.Text.RegularExpressions;

namespace DualExerciseSolver;

public class TargetFunction
{
    public string Name { get; set; }
    public decimal[] Coefficients { get; set; }
    public StrivesTo FunctionStrivesTo { get; set; }

    public TargetFunction(string name = "F", StrivesTo strivesTo = StrivesTo.Min) {
        Name = name;
        FunctionStrivesTo = strivesTo;
        Coefficients = [];
    }

    public TargetFunction(string line, string name = "F", StrivesTo strivesTo = StrivesTo.Min) : this(name, strivesTo)
    {
        ParseString(line);
    }

    public void ParseString(string line)
    {
        line = line.Trim();
        if (!IsRightString(line))
            throw new ArgumentException();

        string[] stringCoefficients = new StringBuilder(line)
            .Replace('X', 'x')
            .Replace('.', ',')
            .ToString()
            .Split('+');

        Coefficients = new decimal[Convert.ToInt32(line[(line.LastIndexOf('x') + 1)..])];
        foreach (string stringCoefficient in stringCoefficients)
        {
            string[] parts = stringCoefficient.Split('x');
            Coefficients[Convert.ToInt32(parts[1]) - 1] = Convert.ToDecimal(parts[0]);
        }
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder("F= ");
        for (int i = 0; i < Coefficients.Length; i++)
        {
            result.Append($"{Coefficients[i]}x{i + 1}");
            if (i + 1 < Coefficients.Length)
                result.Append(" + ");
        }
        result.Append($" -> {FunctionStrivesTo}");

        return result.ToString();
    }

    public static bool IsRightString(string line)
    {
        return Regex.Match(line, @"^(( ?\+? ?-?(0(\,|\.))?\d+(x|X)\d+)+)$").Success;
    }
}
