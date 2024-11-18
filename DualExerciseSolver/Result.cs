using System;

namespace DualExerciseSolver;

public class Result
{
    public decimal FunctionResult { get; set; }
    public decimal[] X { get; set; } = [];

    public Result(decimal functionResult, decimal[] x)
    {
        FunctionResult = functionResult;
        X = x;
    }
    public Result() {}
}
