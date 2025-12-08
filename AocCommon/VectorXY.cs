using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocCommon
{
    public readonly record struct VectorXY(int X, int Y)
    {
        public static readonly VectorXY Zero = new(0, 0);
        public static readonly VectorXY Up = new(0, +1);
        public static readonly VectorXY Down = new(0, -1);
        public static readonly VectorXY Left = new(-1, 0);
        public static readonly VectorXY Right = new(+1, 0);

        public static VectorXY operator +(VectorXY left, VectorXY right)
        {
            return new VectorXY(left.X + right.X, left.Y + right.Y);
        }
        public static VectorXY operator -(VectorXY left, VectorXY right)
        {
            return new VectorXY(left.X - right.X, left.Y - right.Y);
        }
        public static VectorXY operator -(VectorXY vec)
        {
            return new VectorXY(-vec.X, -vec.Y);
        }
        public VectorXY Scale(int factor)
        {
            return new VectorXY(X * factor, Y * factor);
        }
        public long Dot(VectorXY that)
        {
            return (long)X * that.X + (long)Y * that.Y;
        }
        public int Cross(VectorXY that)
        {
            return this.X * that.Y - this.Y * that.X;
        }
        public VectorXY RotatedLeft()
        {
            return new VectorXY(-Y, X);
        }
        public VectorXY RotatedRight()
        {
            return new VectorXY(Y, -X);
        }
        public int ManhattanMetric()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
        public int ChebyshevMetric()
        {
            return Math.Max(Math.Abs(X), Math.Abs(Y));
        }
        public long EuclideanSquared()
        {
            return ((long)X * X) + ((long)Y * Y);
        }
        public double EuclideanMetric()
        {
            return Math.Sqrt(EuclideanSquared());
        }

        public readonly VectorXY NextUp()
        {
            return this + Up;
        }
        public readonly VectorXY NextDown()
        {
            return this + Down;
        }
        public readonly VectorXY NextLeft()
        {
            return this + Left;
        }
        public readonly VectorXY NextRight()
        {
            return this + Right;
        }
        public readonly VectorXY[] NextFour()
        {
            return
                [
                    this + Up,
                    this + Down,
                    this + Left,
                    this + Right,
                ];
        }
        public readonly VectorXY[] NextEight()
        {
            return
                [
                    this + Up + Left,
                    this + Up,
                    this + Up + Right,
                    this + Left,
                    this + Right,
                    this + Down + Left,
                    this + Down,
                    this + Down + Right,
                ];
        }
    }
}
