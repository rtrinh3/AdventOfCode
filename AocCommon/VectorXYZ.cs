using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocCommon
{
    public record class VectorXYZ(int X, int Y, int Z)
    {
        public static readonly VectorXYZ Zero = new(0, 0, 0);

        public static VectorXYZ operator +(VectorXYZ left, VectorXYZ right)
        {
            return new VectorXYZ(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }
        public static VectorXYZ operator -(VectorXYZ left, VectorXYZ right)
        {
            return new VectorXYZ(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
        public static VectorXYZ operator -(VectorXYZ vec)
        {
            return new VectorXYZ(-vec.X, -vec.Y, -vec.Z);
        }
        public VectorXYZ Scale(int factor)
        {
            return new VectorXYZ(X * factor, Y * factor, Z * factor);
        }
        public int Dot(VectorXYZ that)
        {
            return this.X * that.X + this.Y * that.Y + this.Z * that.Z;
        }
        public VectorXYZ Cross(VectorXYZ that)
        {
            return new VectorXYZ(
                this.Y * that.Z - this.Z * that.Y,
                this.Z * that.X - this.X * that.Z,
                this.X * that.Y - this.Y * that.X
            );
        }
        public int ManhattanMetric()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }
        public int ChebyshevMetric()
        {
            return Math.Max(Math.Abs(X), Math.Max(Math.Abs(Y), Math.Abs(Z)));
        }
    }
}
