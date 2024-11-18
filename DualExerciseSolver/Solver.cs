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

    public void ShowMatix(decimal[,] matrix, decimal[] B, decimal[] C, decimal[] teta, decimal[] delta, int[] basis)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            System.Console.Write($"{i + 1} x{basis[i] + 1} {C[i]} {B[i]} ");
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{matrix[i, j]} ");
            }
            System.Console.WriteLine(teta[i]);
        }
        for (int i = 0; i < delta.Length; i++)
        {
            System.Console.Write($"{delta[i]} ");
        }
    }

    public Result Solve()
    {
        int lenght = 0;
        foreach (var lim in Limitations)
        {
            if (lenght < lim.Coefficients.Length)
                lenght = lim.Coefficients.Length;
        }
        decimal[,] matrix = new decimal[Limitations.Length, lenght + Limitations.Length];
        for (int i = 0; i < Limitations.Length; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrix[i, j] = Limitations[i].Coefficients[j];
            }
            matrix[i, lenght + i] = 1;
        }

        decimal[] coefs = new decimal[matrix.GetLength(1)];
        for (int i = 0; i < Function.Coefficients.Length; i++)
        {
            coefs[i] = Function.Coefficients[i];
        }

        decimal[] B = new decimal[matrix.GetLength(0)];
        for (int i = 0; i < B.Length; i++)
        {
            B[i] = Limitations[i].RightLimitation;
        }
        decimal[] C = new decimal[matrix.GetLength(0)];
        decimal F = 0;
        decimal[] delta = new decimal[matrix.GetLength(1)];

        int minD = 0;
        decimal min = 0;
        for (int i = 0; i < delta.Length; i++)
        {
            delta[i] = -1 * coefs[i];
            if (delta[i] < min)
            {
                min = delta[i];
                minD = i;
            }
        }
        decimal[] teta = new decimal[matrix.GetLength(0)];
        for (int i = 0; i < teta.Length; i++)
        {
            teta[i] = B[i] / matrix[i, minD];
        }
        ShowMatix(matrix, B, C, teta, delta, [3, 4, 5]);

        return new Result();
    }
}
