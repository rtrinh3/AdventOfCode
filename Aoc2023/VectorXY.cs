using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal readonly record struct VectorXY(int X, int Y)
    {
        public static VectorXY operator +(VectorXY left, VectorXY right)
        {
            return new VectorXY(left.X + right.X, left.Y + right.Y);
        }
        public static VectorXY operator -(VectorXY left, VectorXY right)
        {
            return new VectorXY(left.X - right.X, left.Y - right.Y);
        }
        public VectorXY Scale(int factor)
        {
            return new VectorXY(X * factor, Y * factor);
        }
        public int Dot(VectorXY that)
        {
            return this.X * that.X + this.Y * that.Y;
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
    }
}
