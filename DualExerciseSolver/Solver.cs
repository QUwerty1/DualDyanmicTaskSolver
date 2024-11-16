
namespace DualExerciseSolver;

public class Solver
{
    public TargetFunction Function { get; set; }
    public decimal[,] Matrix { get; set; }

    public Solver()
    {
        Matrix = new decimal[0, 0];
        Function = new TargetFunction();
    }

    public Solver(TargetFunction function, Limitation[] limitations)
    {
        Function = function;
        int maxLength = 0;
        foreach (Limitation limitation in limitations)
        {
            if (maxLength < limitation.Coefficients.Length)
                maxLength = limitation.Coefficients.Length;
        }
        Matrix = new decimal[limitations.Length, maxLength];
        for (int i = 0; i < Matrix.GetLength(0); i++)
        {
            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                Matrix[i, j] = limitations[i].Coefficients[j];
            }
        }
    }

    
}
