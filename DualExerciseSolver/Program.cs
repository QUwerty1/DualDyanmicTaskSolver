namespace DualExerciseSolver;

public static class Program
{
    public static void Main() {

        var target = new TargetFunction();
        Console.WriteLine("Введите число ограничений: ");
        int limitsCount = Convert.ToInt32(Console.ReadLine());
        var limits = new Limitation[3];
        for (int i = 0; i < limitsCount; i++)
        {
            limits[i] = new();
        }
        
        var solver = new Solver(target, limits);
        var res = solver.Solve(false);
        Console.WriteLine($"F= {res.FunctionResult}");
        for (int i = 0; i < res.X.Length; i++)
        {
            Console.WriteLine($"x{i + 1}= {res.X[i]}");
        }
    }
}