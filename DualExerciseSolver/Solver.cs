namespace DualExerciseSolver;

public class Solver
{
    public TargetFunction Function { get; set; }
    public Limitation[] Limitations { get; set; }

    public Solver()
    {
        Limitations = [];
        Function = new TargetFunction();
    }

    public Solver(TargetFunction function, Limitation[] limitations)
    {
        function.MakeStandart();
        Function = function;
        Function = function;
        Limitations = new Limitation[limitations.Length];
        for (int i = 0; i < limitations.Length; i++)
        {
            var limit = limitations[i];
            limit.MakeStandart();
            Limitations[i] = limit;
        }
    }

    private Tuple<int, int>? GetResultLC(decimal[,] matrix, int[] basis, bool isDouble)
    {
        if (!isDouble)
        {

            int? minCI = null;
            decimal minC = 0;
            decimal[] deltas = new decimal[matrix.GetLength(1) - 1];

            for (int i = 0; i < matrix.GetLength(1) - 1; i++)
            {
                decimal el = matrix[matrix.GetLength(0) - 1, i];
                if (!basis.Contains(i + 1) && el < 0 && el < minC)
                {
                    minCI = i;
                    minC = el;
                }
                deltas[i] = el;
            }

            int? minRI = null;
            decimal minR = decimal.MaxValue;
            decimal[] tetas = new decimal[matrix.GetLength(1) - 1];
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                decimal el = matrix[i, matrix.GetLength(1) - 1];
                if (el > 0 && matrix[i, minCI ?? 0] != 0)
                {
                    decimal c = el / matrix[i, minCI ?? 0];
                    if (c < minR)
                    {
                        minR = c;
                        minRI = i;
                    }
                    tetas[basis[i] - 1] = c;
                }
            }
            ShowSolavtion(matrix, basis, deltas, tetas);

            if (minCI is not null && minRI is not null)
                return new(minCI ?? 0, minRI ?? 0);
            return null;
        }
        else
        {
            int? minCI = null;
            decimal minC = 0;
            int? minRi = null;
            decimal minR = 0;
            return null;
        }
    }

    public Result Solve(bool isDouble)
    {
        int lenght = 0;
        foreach (var lim in Limitations)
        {
            if (lenght < lim.Coefficients.Length)
                lenght = lim.Coefficients.Length;
        }
        decimal[,] matrix = new decimal[Limitations.Length + 1, lenght + Limitations.Length + 1];
        for (int i = 0; i < Limitations.Length; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrix[i, j] = Limitations[i].Coefficients[j];
            }
            matrix[i, lenght + i] = 1;
            matrix[i, matrix.GetLength(1) - 1] = Limitations[i].RightLimitation;
        }
        for (int i = 0; i < Function.Coefficients.Length; i++)
        {
            matrix[matrix.GetLength(0) - 1, i] = -1 * Function.Coefficients[i];
        }
        int[] basis = new int[matrix.GetLength(0) - 1];
        for (int i = 0; i < matrix.GetLength(1) - lenght - 1; i++)
        {
            basis[i] = i + lenght + 1;
        }

        if (isDouble == true)
        {
            decimal[,] newMatrix = new decimal[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[j, i] = matrix[i, j];
                }
            }
            matrix = newMatrix;
        }

        var res = GetResultLC(matrix, basis, isDouble);

        while (res is not null)
        {
            int r, c;
            (c, r) = res;
            basis[r] = c + 1;

            decimal resEl = matrix[r, c];
            decimal[,] newMatrix = new decimal[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i == r)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        newMatrix[i, j] = matrix[i, j] / resEl;
                    }
                }
                else
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        newMatrix[i, j] = matrix[i, j] - matrix[i, c] * matrix[r, j] / resEl;
                    }
                }
            }
            matrix = newMatrix;
            res = GetResultLC(matrix, basis, isDouble);
        }

        decimal[] x = new decimal[Function.Coefficients.Length];
        for (int i = 0; i < x.Length; i++)
        {
            int ind = Array.FindIndex(basis, (el) => el == i + 1);
            x[i] = ind != -1 ? matrix[ind, matrix.GetLength(1) - 1] : 0; 
        }
        decimal f = 0;
        for (int i = 0; i < x.Length; i++)
        {
            f += Function.Coefficients[i] * x[i];
        }

        return new Result(f, x);
    }

    public void ShowSolavtion(decimal[,] matrix, int[] basis, decimal[] deltas, decimal[] tetas)
    {
        Console.Write("|  i | Bx | A0      |");
        for (int i = 0; i < matrix.GetLength(1) - 1; i++)
        {
            Console.Write($" A{i + 1}      |");
        }
        Console.WriteLine("\n" + new string('-', 81));
        for (int i = 0; i < matrix.GetLength(0) - 1; i++)
        {
            Console.Write(String.Format(
                    "| {0, 2} | x{1, 1} | {2, 7} |",
                    i + 1, basis[i], matrix[i, matrix.GetLength(1) - 1].ToString("0.##")
                    ));
            for (int j = 0; j < matrix.GetLength(1) - 1; j++)
            {
                Console.Write(String.Format(
                    " {0, 7} |", matrix[i, j].ToString("0.##")
                ));
            }
            Console.WriteLine();
        }
        Console.Write(String.Format("|{0,18} |", "delta"));
        for (int i = 0; i < deltas.Length; i++)
        {
            Console.Write(String.Format(" {0, 7} |", deltas[i].ToString("0.##")));
        }
        Console.WriteLine();
        Console.Write(String.Format("|{0,18} |", "teta"));
        for (int i = 0; i < deltas.Length; i++)
        {
            Console.Write(String.Format(" {0, 7} |", tetas[i].ToString("0.##")));
        }
        Console.Write("\nБазис: (");
        for (int i = 0; i < matrix.GetLength(1) - 1; i++)
        {
            Console.Write(basis.Contains(i + 1) ? matrix[Array.IndexOf(basis, i + 1), matrix.GetLength(1) - 1] : 0);
            Console.Write(" ");
        }
        Console.WriteLine(")\n\n");
    }
}
