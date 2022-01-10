using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Engine.Structs
{
    public struct Vector2
    {
        public int X;
        public int Y;

        /// <summary>
        /// Length of vector in square
        /// </summary>
        public int SqrMagnetude => X * X + Y * Y;
        /// <summary>
        /// Length of vector
        /// </summary>
        public int Magnetude => (int)Math.Sqrt(X * X + Y * Y);

        /// <summary>
        /// Vector wich length equal 1
        /// </summary>
        //public Vector2 Normolized => this/;

        public Vector2(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
        }
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 Zero => new Vector2();
        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Up => new Vector2(0, -1);
        public static Vector2 Down => new Vector2(0, 1);


        public static bool operator ==(Vector2 v1, Vector2 v2) => v1.X == v2.X && v1.Y == v2.Y;
        public static bool operator !=(Vector2 v1, Vector2 v2) => v1.X != v2.X || v1.Y != v2.Y;

        public static Vector2 operator +(Vector2 v1, Vector2 v2) => new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2 operator +(Vector2 v1, int v2) => new Vector2(v1.X + v2, v1.Y + v2);

        public static Vector2 operator -(Vector2 v1, Vector2 v2) => new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2 operator -(Vector2 v1, int v2) => new Vector2(v1.X - v2, v1.Y - v2);
        public static Vector2 operator -(Vector2 v1) => new Vector2(-v1.X, -v1.Y);

        public static Vector2 operator *(Vector2 v1, int v2) => new Vector2(v1.X * v2, v1.Y * v2);
        public static Vector2 operator *(Vector2 v1, Vector2 v2) => new Vector2(v1.X * v2.X, v1.Y * v2.Y);

        public static Vector2 operator /(Vector2 v1, int v2) => new Vector2(v1.X / v2, v1.Y / v2);
        public static Vector2 operator /(Vector2 v1, Vector2 v2) => new Vector2(v1.X / v2.X, v1.Y / v2.Y);

        public override bool Equals(object obj) => obj is Vector2 vector && X == vector.X && Y == vector.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X}, {Y})";
    }
}
