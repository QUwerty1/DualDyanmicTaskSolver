namespace DualExerciseSolver;

public static class Program
{
    public static void Main() {
        // var target = new TargetFunction("108x1 + 112x2 + 126x3", "F", StrivesTo.Max);
        // System.Console.WriteLine(target);

        // var limits = new Limitation[] {
        //     new("0.8x1+0.5x2+0.6x3<=800"),
        //     new("0.4x1+0.4x2+0.3x3<=600"),
        //     new("0x1+0.1x2+0.1x3<=120")
        // };
        var limits = new Limitation[] {
            new ("2x1 + 5x2 <= 20"),
            new ("8x1 + 5x2 <= 40"),
            new ("5x1 + 6x2 <= 30"),
        };
        // var target = new TargetFunction("108x1+112x2+126x3", StrivesTo.Max);
        var target = new TargetFunction("50x1 + 40x2", "F", StrivesTo.Max);
        var solver = new Solver(target, limits);
        solver.Solve();
    }
}