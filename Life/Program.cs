using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector2 ScreenSize = new Vector2(100, 40);

            ConsoleInit(ScreenSize);

            Buffer buffer = new Buffer(ScreenSize);

            Vector2 pos = new Vector2(50, 20);

            ButtonInput SpaceInput = new ButtonInput(KeyboardInput.KeyCode.Space);
            SpaceInput.Mod = ButtonInput.InputMod.Toggle;

            while (true)
            {

                buffer.Fill(' ');

                buffer.DrawRectengle(Vector2.Zero, new Vector2(20, ScreenSize.Y), '+', '-', '|');
                buffer.DrawRectengle(Vector2.Right * 19, new Vector2(ScreenSize.X - 19, ScreenSize.Y), '+', '-', '|');

                buffer.DrawText(Vector2.One, "Pos: " + ConsoleMouseInput.GetMousePosition().ToString());
                buffer.DrawText(Vector2.Right + Vector2.Up * 2, "Caps: " + Console.CapsLock.ToString());
                buffer.DrawText(Vector2.Right + Vector2.Up * 3, "Space: " + SpaceInput.State.ToString());
                buffer.DrawText(Vector2.Right + Vector2.Up * 4, "Space: " + SpaceInput.Pressed().ToString());


                buffer.DrawText(Vector2.Right + Vector2.Up * 38, " > Exit");






                buffer.DrawFillRectengle(new Vector2(40, 10), Vector2.One * 20, '#');
                buffer.DrawFillRectengle(new Vector2(45, 15), Vector2.One * 10, ' ');

                /*if (SpaceInput.Pressed())
                {
                    pos = ConsoleMouseInput.GetMousePosition();
                    if (pos.Y == 38 && pos.X < 19) break;
                }*/
            buffer[pos] = '@';




            buffer.Render();
            /*buffer.ColorRender(new List<Buffer.ColorRenderData>(new Buffer.ColorRenderData[]
                {
                    new Buffer.ColorRenderData(pos, ConsoleColor.White, ConsoleColor.Gray)
                }
            ));*/
            //Thread.Sleep(10);*/
            }
        }



    static void ConsoleInit(Vector2 size)
        {
            Console.WindowWidth = size.X;
            Console.WindowHeight = size.Y + 1;

            //Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Life";
            Console.CursorVisible = false;

            
        }

    }

    public class ButtonInput
    {
        public InputMod Mod
        {
            get => _isToggle ? InputMod.Toggle : InputMod.Button;
            set => _isToggle = value == InputMod.Toggle;
        }

        public bool State => KeyboardInput.IsKeyDown(_key);

        private KeyboardInput.KeyCode _key;

        private bool _isToggle = false;
        private bool _capsUnpressed = true;
        private bool _toggleState = false;

        public ButtonInput(KeyboardInput.KeyCode key)
        {
            _key = key;
        }

        public bool Pressed()
        {
            bool buttoen;
            if (KeyboardInput.IsKeyDown(_key))
            {
                if (_capsUnpressed)
                {
                    _capsUnpressed = false;
                    buttoen = true;
                    _toggleState = !_toggleState;
                }
                else buttoen = false;
            }
            else
            {
                _capsUnpressed = true;
                buttoen = false;
            }

            if (_isToggle) return _toggleState;
            else return buttoen;
        }

        public enum InputMod
        {
            Button,
            Toggle
        }
    }



    public class Buffer
    {
        private char[] buffer;
        public int Width { get; }
        public int Height { get; }

        public Buffer(Vector2 size)
        {
            Width = size.X;
            Height = size.Y;
            buffer = new char[Width * Height];
        }

        public Buffer(int width, int height)
        {
            Width = width;
            Height = height;
            buffer = new char[Width * Height];
        }

        public char this[int x, int y]
        {
            get => buffer[y * Width + x];
            set => buffer[y * Width + x] = value;
        }

        public char this[Vector2 point]
        {
            get => buffer[point.Y * Width + point.X];
            set => buffer[point.Y * Width + point.X] = value;
        }


        public void Fill(char value) => Array.Fill<char>(buffer, value);

        public void DrawFillRectengle(Vector2 LeftTop, Vector2 Size, char value)
        {
            int iterator;
            for (int i = LeftTop.Y; i < LeftTop.Y + Size.Y; i++)
            {
                iterator = Width * i + LeftTop.X;
                for (int j = iterator; j < iterator + Size.X; j++)
                {
                    buffer[j] = value;
                }
            }
        }

        public void DrawRectengle(Vector2 LeftTop, Vector2 Size, char value)
        {
            int iterator;

            iterator = Width * LeftTop.Y + LeftTop.X;
            for (int j = iterator; j < iterator + Size.X; j++)
            {
                buffer[j] = value;
            }
            iterator = Width * (LeftTop.Y + Size.Y - 1) + LeftTop.X;
            for (int j = iterator; j < iterator + Size.X; j++)
            {
                buffer[j] = value;
            }

            iterator = Width * LeftTop.Y + LeftTop.X;
            for (int j = iterator; j < buffer.Length; j += Width)
            {
                buffer[j] = value;
            }

            iterator = Width * LeftTop.Y + LeftTop.X + Size.X - 1;
            for (int j = iterator; j < buffer.Length; j += Width)
            {
                buffer[j] = value;
            }

        }

        public void DrawRectengle(Vector2 LeftTop, Vector2 Size, char corner, char horizontal, char vertical)
        {
            int iterator;

            iterator = Width * LeftTop.Y + LeftTop.X;
            for (int j = iterator; j < iterator + Size.X; j++)
            {
                buffer[j] = horizontal;
            }
            iterator = Width * (LeftTop.Y + Size.Y - 1) + LeftTop.X;
            for (int j = iterator; j < iterator + Size.X; j++)
            {
                buffer[j] = horizontal;
            }

            iterator = Width * LeftTop.Y + LeftTop.X;
            for (int j = iterator; j < buffer.Length; j += Width)
            {
                buffer[j] = vertical;
            }

            iterator = Width * LeftTop.Y + LeftTop.X + Size.X - 1;
            for (int j = iterator; j < buffer.Length; j += Width)
            {
                buffer[j] = vertical;
            }

            this[LeftTop.X, LeftTop.Y] = corner;
            this[LeftTop.X + Size.X - 1, LeftTop.Y] = corner;
            this[LeftTop.X, LeftTop.Y + Size.Y - 1] = corner;
            this[LeftTop.X + Size.X - 1, LeftTop.Y + Size.Y - 1] = corner;

        }

        public void DrawLine()
        {

        }

        public void DrawVerticalLine(Vector2 LeftTop, int Lenght, char value)
        {
            int iterator;

            iterator = Width * LeftTop.Y + LeftTop.X;
            for (int j = iterator; j < buffer.Length; j += Width)
            {
                buffer[j] = value;
            }
        }

        public void DrawHorizontalLine(Vector2 LeftTop, int Lenght, char value)
        {
            int iterator;

            iterator = Width * LeftTop.Y + LeftTop.X;
            for (int j = iterator; j < iterator + Lenght; j++)
            {
                buffer[j] = value;
            }
        }

        public void DrawText(Vector2 LeftTop, string text)
        {
            int iterator;

            iterator = Width * LeftTop.Y + LeftTop.X;
            for (int j = 0; j < text.Length; j++)
            {
                buffer[iterator + j] = text[j];
            }
        }





        public void Render()
        {
            SetDefaultTheme();
            Console.SetCursorPosition(0, 0);
            int j = Width;
            for (int i = 0; i < buffer.Length; i++)
            {
                Console.Out.Write(buffer[i]);
                j--;
                if (j == 0)
                {
                    j = Width;
                    Console.Out.Write('\n');
                }
            }
        }

        public void Render(List<ColorRenderData> renderData)
        {
            //SetDefaultTheme();
            Console.SetCursorPosition(0, 0);
            int j = Width;
            for (int i = 0; i < buffer.Length; i++)
            {
                Console.Out.Write(buffer[i]);
                j--;
                if (j == 0)
                {
                    j = Width;
                    Console.Out.Write('\n');
                }
            }
        }

        public void SetDefaultTheme()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void ColorRender(List<ColorRenderData> renderData)
        {
            foreach (var item in renderData)
            {
                Console.SetCursorPosition(item.Position.X, item.Position.Y);
                Console.ForegroundColor = item.Foreground;
                Console.BackgroundColor = item.Background;
                Console.Write(this[item.Position]);
            }
        }

        public struct ColorRenderData
        {
            public Vector2 Position;
            public ConsoleColor Foreground;
            public ConsoleColor Background;

            public ColorRenderData(Vector2 position, ConsoleColor foreground, ConsoleColor background)
            {
                Position = position;
                Foreground = foreground;
                Background = background;
            }
        }
    }




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

        public static Vector2 Up => new Vector2(0, 1);

        public static Vector2 Down => new Vector2(0, -1);




        public static bool operator ==(Vector2 v1, Vector2 v2) => v1.X == v2.X && v1.Y == v2.Y;
        public static bool operator !=(Vector2 v1, Vector2 v2) => v1.X != v2.X || v1.Y != v2.Y;
        public static Vector2 operator +(Vector2 v1, Vector2 v2) => new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2 operator -(Vector2 v1, Vector2 v2) => new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2 operator -(Vector2 v1) => new Vector2(-v1.X, -v1.Y);
        public static Vector2 operator *(Vector2 v1, int v2) => new Vector2(v1.X * v2, v1.Y * v2);
        public static Vector2 operator /(Vector2 v1, int v2) => new Vector2(v1.X / v2, v1.Y / v2);


        public override bool Equals(object obj) => obj is Vector2 vector && X == vector.X && Y == vector.Y;
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X}, {Y})";
    }

    public class ConsoleMouseInput
    {
        [DllImport("kernel32")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT rc);


        static readonly Vector2 LeftTopOffset = new Vector2(8, 31);
        static readonly Vector2 RightBottomOffset = new Vector2(40, 60);


        public static Vector2 GetMousePosition()
        {
            Vector2 position = MouseOperations.GetCursorPosition().Vector;

            RECT rect = new RECT();
            GetWindowRect(GetConsoleWindow(), ref rect);

            position -= LeftTopOffset;
            position -= rect.Position;

            float kX = Console.WindowWidth / (float)(rect.right - rect.left - RightBottomOffset.X);
            float kY = Console.WindowHeight / (float)(rect.bottom - rect.top - RightBottomOffset.Y);

            position.X = (int)(position.X * kX);
            position.Y = (int)(position.Y * kY);

            return position;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public Vector2 Position => new Vector2(left, top);
        }
    }


    public class MouseOperations
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out Point lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(Point point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static Point GetCursorPosition()
        {
            Point currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new Point(0, 0); }
            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            Point position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

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

            public Vector2 Vector => new Vector2(X, Y);
        }

    }

    public static class KeyboardInput
    {
        private const int KeyPressed = 0x8000;

        public static bool IsKeyDown(KeyCode key)
        {
            return (GetKeyState((int)key) & KeyPressed) != 0;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(int key);

        //All keys -> https://docs.microsoft.com/ru-ru/dotnet/api/system.windows.forms.keys?view=netframework-4.8
        public enum KeyCode : int
        {
            Space = 32,
            Left = 37,
            Up = 38,
            Right = 39,
            Down = 40
        }
    }

    class Creature//¤
    {
        public int DeathCount { get; set; }

        public long ID { get; }
        public Vector2 Position { get; set; }
        public string Name { get; }

        public int FullHP { get; set; }

        public int HP { get; set; }
        public int Damage { get; set; }
        public int Lvl { get; set; }

        public int Expa { get; set; }

        public float MissChance { get; set; }

        public void LvlUp()
        {
            Lvl++;

            MissChance -= 0.001f;

            FullHP = (int)(FullHP * 1.1);
            Damage = (int)(Damage * 1.05);

            Expa = Lvl * 20;

            HP = FullHP;

        }

        public void SetDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0) DeathCount = 1;
        }

        public void Move(Vector2 movement)
        {
            Position += movement;
            Position = new Vector2(Position.X, Position.Y);
        }
        public void Move(Vector2 movement, Vector2 size)
        {
            Position += movement;
            Position = new Vector2(Position.X % size.X, Position.Y % size.Y);
        }

        public Creature(int id, string name, Vector2 position)
        {
            ID = id;
            Name = name;

            Lvl = 1;
            Position = position;

            MissChance = 0.2f;

            FullHP = 100;
            Damage = 20;

            Expa = Lvl * 20;

            HP = FullHP;

            DeathCount = 0;
        }

        public static void Bump(Creature c1, Creature c2)
        {
            Random r = new Random();

            if (c1.HP * 2 < c2.HP)
            {
                if (r.Next(0, 10) < 3)
                {
                    c1.SetDamage(c2.Damage / 4);
                    c1.Move(c1.Position - c2.Position);
                    return;
                }
            }

            if(c2.HP * 2 < c1.HP)
            {
                if (r.Next(0, 10) < 3)
                {
                    c2.SetDamage(c1.Damage / 4);
                    c2.Move(c2.Position - c1.Position);
                    return;
                }
            }

            if (r.Next(0, 100) < 95)
            {
                c1.SetDamage(c2.Damage);
                c2.SetDamage(c1.Damage);
            }

        }

    }

}
