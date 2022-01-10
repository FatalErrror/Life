using System;
using Life.Engine.Structs;

namespace Life.Engine.Grapfics
{
    public class Canvas
    {
        private char[] _buffer;
        public int Width { get; }
        public int Height { get; }

        public Vector2 Size { get; }

        public Rectangle CanvasBorder { get; }

        public Canvas(Vector2 size)
        {
            Width = size.X;
            Height = size.Y;
            Size = size;
            CanvasBorder = new Rectangle(Size-1);
            _buffer = new char[Width * Height];
        }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            Size = new Vector2(Width, Height);
            CanvasBorder = new Rectangle(Size - 1);
            _buffer = new char[Width * Height];
        }

        public char this[int x, int y]
        {
            get => _buffer[y * Width + x];
            set => _buffer[y * Width + x] = value;
        }

        public char this[Vector2 point]
        {
            get => _buffer[point.Y * Width + point.X];
            set => _buffer[point.Y * Width + point.X] = value;
        }


        public char[] GetBuffer() => _buffer;

        public void Fill(char value) => Array.Fill<char>(_buffer, value);

        public void DrawFillRectengle(Rectangle rectangle, char value)
        {
            int iterator;
            int sizeX = rectangle.Size.X;

            for (int i = rectangle.TopLeft.Y; i < rectangle.RightBottom.Y; i++)
            {
                iterator = Width * i + rectangle.TopLeft.X;
                for (int j = iterator; j < iterator + sizeX; j++)
                {
                    _buffer[j] = value;
                }
            }
        }

        public void DrawRectengle(Rectangle rectangle, char value)
        {
            int iterator;
            int sizeX = rectangle.Size.X;

            iterator = Width * rectangle.TopLeft.Y + rectangle.TopLeft.X;
            for (int j = iterator; j < iterator + sizeX; j++)
            {
                _buffer[j] = value;
            }
            iterator = Width * rectangle.RightBottom.Y + rectangle.TopLeft.X;
            for (int j = iterator; j < iterator + sizeX; j++)
            {
                _buffer[j] = value;
            }

            int i = 0;
            iterator = Width * rectangle.TopLeft.Y + rectangle.TopLeft.X;
            for (int j = iterator; j < _buffer.Length && i < rectangle.Size.Y; j += Width, i++)
            {
                _buffer[j] = value;
            }

            i = 0;
            iterator = Width * rectangle.TopLeft.Y + rectangle.RightBottom.X;
            for (int j = iterator; j < _buffer.Length && i < rectangle.Size.Y; j += Width, i++)
            {
                _buffer[j] = value;
            }

        }

        public void DrawRectengle(Rectangle rectangle, char corner, char horizontal, char vertical)
        {
            int iterator;
            int sizeX = rectangle.Size.X;

            iterator = Width * rectangle.TopLeft.Y + rectangle.TopLeft.X;
            for (int j = iterator; j < iterator + sizeX; j++)
            {
                _buffer[j] = horizontal;
            }
            iterator = Width * rectangle.RightBottom.Y + rectangle.TopLeft.X;
            for (int j = iterator; j < iterator + sizeX; j++)
            {
                _buffer[j] = horizontal;
            }

            int i = 0;
            iterator = Width * rectangle.TopLeft.Y + rectangle.TopLeft.X;
            for (int j = iterator; j < _buffer.Length && i < rectangle.Size.Y; j += Width, i++)
            {
                _buffer[j] = vertical;
            }

            i = 0;
            iterator = Width * rectangle.TopLeft.Y + rectangle.RightBottom.X;
            for (int j = iterator; j < _buffer.Length && i < rectangle.Size.Y; j += Width, i++)
            {
                _buffer[j] = vertical;
            }

            this[rectangle.TopLeft.X, rectangle.TopLeft.Y] = corner;
            this[rectangle.RightBottom.X, rectangle.TopLeft.Y] = corner;
            this[rectangle.TopLeft.X, rectangle.RightBottom.Y] = corner;
            this[rectangle.RightBottom.X, rectangle.RightBottom.Y] = corner;

        }

        public void DrawLine(Vector2 startPos, Vector2 line, char value)
        {
            float k = line.Y/(float)line.X;

            for (int j = startPos.X; j < startPos.X + line.X; j++)
            {
                this[j, startPos.Y+(int)(j*k)] = value;
            }
        }

        public void DrawVerticalLine(Vector2 startPos, int lenght, char value)
        {
            int iterator;
            int i = 0;

            iterator = Width * startPos.Y + startPos.X;
            for (int j = iterator; j < _buffer.Length && i < lenght; j += Width, i++)
            {
                _buffer[j] = value;
            }
        }

        public void DrawHorizontalLine(Vector2 startPos, int lenght, char value)
        {
            int iterator;

            iterator = Width * startPos.Y + startPos.X;
            for (int j = iterator; j < iterator + lenght; j++)
            {
                _buffer[j] = value;
            }
        }

        public void DrawText(Vector2 startPos, string text)
        {
            int iterator;

            iterator = Width * startPos.Y + startPos.X;
            for (int j = 0; j < text.Length && iterator + j < _buffer.Length; j++)
            {
                _buffer[iterator + j] = text[j];
            }
        }

        public void DrawPoint(Vector2 position, char value)
        {
            _buffer[Width * position.Y + position.X] = value;
        }

    }
}
