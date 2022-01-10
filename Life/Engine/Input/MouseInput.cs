using System;
using System.Runtime.InteropServices;

namespace Life.Engine.Input
{
    public class MouseInput
    {
        private const int LEFT_OFFSET = 8;
        private const int TOP_OFFSET = 31;
        private const int RIGHT_OFFSET = 35;//40;
        private const int BOTTOM_OFFSET = 55;//60;

        private readonly IntPtr _window;

        private Point _position;
        private Rect _rect;
        private float _kX;
        private float _kY;

        private Structs.Vector2 _cursorPosition;

        public MouseInput()
        {
            _window = GetConsoleWindow();
            _cursorPosition = new Structs.Vector2();
        }

        public Structs.Vector2 CursorPosition => _cursorPosition;

        public void UpdateCursorPosition()
        {
            _cursorPosition = GetCursorPosition().ToVector2();
        }

        public static Point GetCursorPosition()
        {
            Point position = GetMousePosition();

            Rect rect = new Rect();
            GetWindowRect(GetConsoleWindow(), ref rect);

            position.X -= (LEFT_OFFSET+ rect.left);
            position.Y -= (TOP_OFFSET+ rect.top);

            float kX = Console.WindowWidth / (float)(rect.right - rect.left - RIGHT_OFFSET);
            float kY = Console.WindowHeight / (float)(rect.bottom - rect.top - BOTTOM_OFFSET);

            position.X = (int)(position.X * kX);
            position.Y = (int)(position.Y * kY);

            return position;
        }

        public static Point GetMousePosition()
        {
            Point currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new Point(0, 0); }
            return currentMousePoint;
        }


        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, ref Rect rc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out Point lpMousePoint);



        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;


            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Structs.Vector2 ToVector2() => new Structs.Vector2(X, Y);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
