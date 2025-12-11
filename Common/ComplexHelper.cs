using System.Numerics;

public static class ComplexHelper
{
    private static readonly Complex Down = new Complex(1, 0);
    private static readonly Complex Left = new Complex(0, -1);
    private static readonly Complex Right = new Complex(0, 1);

    public static Complex MoveDown(this Complex point) => point + Down;
    public static Complex MoveLeft(this Complex point) => point + Left;
    public static Complex MoveRight(this Complex point) => point + Right;
}