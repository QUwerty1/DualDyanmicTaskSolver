using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace DualExerciseSolver;

public class Limitation
{
    public ComparisonSign Sign { get; set; }
    public decimal[] Coefficients { get; set; } = [];
    public int RightLimitation { get; set; }

    public Limitation(string line)
    {
        ParseString(line);
    }

    public void ParseString(string line)
    {
        line = line.Trim();

        if (!IsRightString(line))
            throw new ArgumentException();

        line = new StringBuilder(line.Trim())
            .Replace('X', 'x')
            .Replace('.', ',')
            .ToString();

        string[] parts;

        if (line.Contains(">="))
        {
            Sign = ComparisonSign.EqualOrGreater;
            parts = line.Split(">=");
        }
        else if (line.Contains("<="))
        {
            Sign = ComparisonSign.EqualOrLess;
            parts = line.Split("<=");
        }
        else
            throw new ArgumentException();

        RightLimitation = Convert.ToInt32(parts[1]);
        string[] stringCoefficents = parts[0].Split('+');
        Coefficients = new decimal[Convert.ToInt32(parts[0][(parts[0].LastIndexOf('x') + 1)..])];

        foreach (string stringCoefficent in stringCoefficents)
        {
            string[] coefParts = stringCoefficent.Trim().Split('x');
            Coefficients[Convert.ToInt32(coefParts[1]) - 1] = Convert.ToDecimal(coefParts[0]);
        }
    }

    public void MakeStandart()
    {
        if (Sign == ComparisonSign.EqualOrGreater)
        {
            for (int i = 0; i < Coefficients.Length; i++)
            {
                Coefficients[i] *= -1;
            }
            RightLimitation *= -1;
            Sign = ComparisonSign.EqualOrLess;
        }
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        for (int i = 0; i < Coefficients.Length; i++)
        {
            result.Append($"{Coefficients[i]}x{i + 1}");
            if (i + 1 < Coefficients.Length)
                result.Append(" + ");
        }
        string sign = Sign == ComparisonSign.EqualOrGreater ? ">=" : "<=";
        result.Append($" {sign} {RightLimitation}");

        return result.ToString();
    }

    public static bool IsRightString(string line)
    {
        return Regex.Match(line, @"^(( ?\+? ?-?(0(\,|\.))?\d+(x|X)\d+)+ ?((<=)|(>=)|(=)) ?(-?\d+))$").Success;
    }
}
