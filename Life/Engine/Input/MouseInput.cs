using System;
using System.Runtime.InteropServices;

namespace Life.Engine.Input
{
    public class MouseInput
    {
        private const int LeftOffset = 8;
        private const int TopOffset = 31;
        private const int RightOffset = 40;
        private const int BottomOffset = 60;

        private readonly IntPtr _window;

        private Point _position;
        private Rect _rect;
        private float _kX;
        private float _kY;

        public MouseInput()
        {
            _window = GetConsoleWindow();
        }

        public Point CursorPosition { get
            {
                if (!GetCursorPos(out _position)) { _position = new Point(0, 0); }

                GetWindowRect(_window, ref _rect);

                _position.X -= (LeftOffset + _rect.left);
                _position.Y -= (TopOffset + _rect.top);

                _kX = Console.WindowWidth / (float)(_rect.right - _rect.left - RightOffset);
                _kY = Console.WindowHeight / (float)(_rect.bottom - _rect.top - BottomOffset);

                _position.X = (int)(_position.X * _kX);
                _position.Y = (int)(_position.Y * _kY);

                return _position;
            }
        }


        public static Point GetCursorPosition()
        {
            Point position = GetMousePosition();

            Rect rect = new Rect();
            GetWindowRect(GetConsoleWindow(), ref rect);

            position.X -= (LeftOffset+ rect.left);
            position.Y -= (TopOffset+ rect.top);

            float kX = Console.WindowWidth / (float)(rect.right - rect.left - RightOffset);
            float kY = Console.WindowHeight / (float)(rect.bottom - rect.top - BottomOffset);

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
