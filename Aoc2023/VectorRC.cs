using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal record struct VectorRC(int Row, int Col)
    {
        public static VectorRC operator +(VectorRC left, VectorRC right)
        {
            return new VectorRC(left.Row + right.Row, left.Col + right.Col);
        }
        public static VectorRC operator -(VectorRC left, VectorRC right)
        {
            return new VectorRC(left.Row - right.Row, left.Col - right.Col);
        }
        public static VectorRC operator -(VectorRC val)
        {
            return new VectorRC(-val.Row, -val.Col);
        }

        public readonly int Dot(VectorRC that)
        {
            return this.Row * that.Row + this.Col * that.Col;
        }
        public readonly VectorRC RotatedLeft()
        {
            return new VectorRC(-Col, Row);
        }
        public readonly VectorRC RotatedRight()
        {
            return new VectorRC(Col, -Row);
        }
        public readonly VectorRC[] EightNeighbors()
        {
            return 
                [
                    this + new VectorRC(-1, -1),
                    this + new VectorRC(-1, 0),
                    this + new VectorRC(-1, +1),
                    this + new VectorRC(0, -1),
                    this + new VectorRC(0, +1),
                    this + new VectorRC(+1, -1),
                    this + new VectorRC(+1, 0),
                    this + new VectorRC(+1, +1),
                ];
        }
    }
}
