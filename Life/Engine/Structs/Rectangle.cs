using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Engine.Structs
{
    public struct Rectangle
    {
        public Vector2 TopLeft;
        public Vector2 RightBottom;
        public Vector2 Size => RightBottom - TopLeft;

        public Vector2 Center => Size / 2;

        public Rectangle(Vector2 size)
        {
            TopLeft = Vector2.Zero;
            RightBottom = size;
        }

        public Rectangle(Vector2 topLeft, Vector2 size)
        {
            TopLeft = topLeft;
            RightBottom = topLeft+size;
        }

        public Rectangle(int left, int top, int width, int height)
        {
            TopLeft = new Vector2(left, top);
            RightBottom = TopLeft + new Vector2(width, height);
        }

        public bool Contain(Vector2 v)
        {
            return
                v.X >= TopLeft.X &&
                v.X <= RightBottom.X &&
                v.Y >= TopLeft.Y &&
                v.Y <= RightBottom.Y;
        }

        public bool ContainExcludeBorder(Vector2 v)
        {
            return
                v.X > TopLeft.X &&
                v.X < RightBottom.X &&
                v.Y > TopLeft.Y &&
                v.Y < RightBottom.Y;
        }
        public bool ContainExcludeRightBottomBorder(Vector2 v)
        {
            return
                v.X >= TopLeft.X &&
                v.X < RightBottom.X &&
                v.Y >= TopLeft.Y &&
                v.Y < RightBottom.Y;
        }
    }
}
