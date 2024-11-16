namespace DualExerciseSolver;

public static class Program
{
    public static void Main() {
        // var target = new TargetFunction("108x1 + 112x2 + 126x3", "F", StrivesTo.Max);
        // System.Console.WriteLine(target);

        var limitation = new Limitation("0,8X1+-0,5X2 +0.6x3<= 800");
        Console.WriteLine(limitation);
    }
}