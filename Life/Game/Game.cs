using Life.Engine.Core;
using Life.Engine.Grapfics;
using Life.Engine.Input;
using Life.Engine.Structs;
using System;
using System.Collections.Generic;

namespace Life.Game
{
    class Game : Main
    {
        Vector2 delimitrStartPos;

        Button MakeGreen;
        Button MakeWhite;
        Button MakeCyan;

        Button AddCreature;

        LinkedList<Creature> creatures;

        Random ran;

        Rectangle board;

        bool pause;

        protected override void Start()
        {
            delimitrStartPos = Vector2.Down + Vector2.Right * 10;

            MakeWhite = new Button(Keyboard, Mouse);
            MakeWhite.Text = "White";
            MakeWhite.StartPosition = Vector2.Right * 2 + Vector2.Down * 4;

            MakeGreen = new Button(Keyboard, Mouse);
            MakeGreen.Text = "Green";
            MakeGreen.StartPosition = Vector2.Right * 2 + Vector2.Down * 5;

            MakeCyan = new Button(Keyboard, Mouse);
            MakeCyan.Text = "Cyan";
            MakeCyan.StartPosition = Vector2.Right * 2 + Vector2.Down * 6;

            AddCreature = new Button(Keyboard, Mouse);
            AddCreature.Text = "Add";
            AddCreature.StartPosition = Vector2.Right*2 + Vector2.Down * 11;

            creatures = new LinkedList<Creature>();
            ran = new Random();

            board = new Rectangle();
            board.TopLeft = delimitrStartPos+Vector2.Right;
            board.RightBottom = Canvas.CanvasBorder.RightBottom-1;

            Keyboard.AddKeyHandler(KeyCodes.Space);
            pause = false;
        }

        protected override void Update()
        {
            Canvas.Fill(' ');

            Canvas.DrawRectengle(Canvas.CanvasBorder, '+', '-', '|');
            Canvas.DrawVerticalLine(delimitrStartPos, Canvas.Height-2, '|');

            Canvas.DrawText(Vector2.One, "Mouse:");
            Canvas.DrawText(Vector2.Right + Vector2.Down*2, " " + Mouse.CursorPosition.ToString());


            Canvas.DrawText(Vector2.Right + Vector2.Down*3, "Color:");
            MakeWhite.Draw(Canvas);
            MakeCyan.Draw(Canvas);
            MakeGreen.Draw(Canvas);

            if (Keyboard.IsPressed(KeyCodes.Space)) pause = !pause;
            Canvas.DrawText(Vector2.Right + Vector2.Down * 7, "Pause:");
            Canvas.DrawText(Vector2.Right + Vector2.Down * 8, " "+ pause.ToString());

            Canvas.DrawText(Vector2.Right + Vector2.Down * 9, "Creature:");
            Canvas.DrawText(Vector2.Right + Vector2.Down * 10, "Count: "+ creatures.Count); 
            AddCreature.Draw(Canvas);


            if (MakeWhite.IsPressed) Console.ForegroundColor = ConsoleColor.White;
            if (MakeCyan.IsPressed) Console.ForegroundColor = ConsoleColor.Cyan;
            if (MakeGreen.IsPressed) Console.ForegroundColor = ConsoleColor.Green;

            if (AddCreature.IsPressed) creatures.AddLast(GetNewCreature());

           
            foreach (var item in creatures)
            {
                
                if (!pause) 
                {
                    if (item.DeathCount > 0) 
                    { 
                        item.DeathCount++;
                        //if (DeathCount > 100)
                    }
                    else
                    {
                        item.Move(new Vector2(ran.Next(-1, 2), ran.Next(-1, 2)), board);
                        foreach (var item1 in creatures)
                        {
                            if (item1.ID != item.ID && item1.DeathCount==0)
                                if ((item1.Position - item.Position).SqrMagnetude <= 2)
                                {
                                    Creature.Bump(item1, item);
                                }
                        }
                    }
                }
                if (Mouse.CursorPosition == item.Position) DrawCreatureWindow(item);
                Canvas.DrawPoint(item.Position, item.DeathCount==0?item.FirstLetter:'.');
            }


            /*if (Canvas.CanvasBorder.Contain(Mouse.CursorPosition))
                Canvas.DrawText(Mouse.CursorPosition, Mouse.CursorPosition.ToString());*/




        }

        Creature GetNewCreature()
        {
            int N = ran.Next(3, 6);

            string name = "";
            name += (char)ran.Next('A', 'Z'+1);

            for (int i = 0; i < N; i++)
            {
                name += (char)ran.Next('a', 'z' + 1);
            }

            Vector2 pos = new Vector2(ran.Next(11, Canvas.Width), ran.Next(1, Canvas.Height));

            return new Creature( name, pos);
        }

        void DrawCreatureWindow(Creature c)
        {
            Vector2 Dir = new Vector2();

            Dir.X = Math.Abs(c.Position.X - board.TopLeft.X) < Math.Abs(c.Position.X - board.RightBottom.X) ? c.Position.X+1 : c.Position.X-11;
            Dir.Y = Math.Abs(c.Position.Y - board.TopLeft.Y) < Math.Abs(c.Position.Y - board.RightBottom.Y) ? c.Position.Y+1 : c.Position.Y - 9;

            
            Canvas.DrawFillRectengle(new Rectangle(Dir, new Vector2(10, 8)), ' ');
            Canvas.DrawRectengle(new Rectangle(Dir, new Vector2(10, 8)), '+', '-', '|');

            Dir.X+=1;
            Canvas.DrawText(Dir+ Vector2.Down, c.Name);
            Canvas.DrawText(Dir+ Vector2.Down*2, "ID: "+c.ID);
            Canvas.DrawText(Dir+ Vector2.Down*3, "Lvl: "+c.Lvl);

            Canvas.DrawText(Dir+ Vector2.Down*4, c.HP+"/"+c.FullHP);

            Canvas.DrawText(Dir + Vector2.Down * 5, "Expa:"+c.Expa);

            if (c.DeathCount == 0) Canvas.DrawText(Dir+ Vector2.Down*7,"ALIVE");
            else Canvas.DrawText(Dir+ Vector2.Down*7,"DEAD "+ c.DeathCount);

        }

    }

    public class Button
    {
        public string Text;
        public Vector2 StartPosition;
        private KeyCodes Key;

        private KeyboardInput _keyboard;
        private MouseInput _mouse;

        public bool IsOver => DoesMouseOver();
        public bool IsPressed => DoesMouseOver() && _keyboard.IsPressed(Key);

        public Button(KeyboardInput keyboard, MouseInput mouse)
        {
            Text = "button";
            StartPosition = Vector2.One;
            Key = KeyCodes.Enter;
            _keyboard = keyboard;
            _mouse = mouse;

            _keyboard.AddKeyHandler(Key);
        }

        public Button(string text, Vector2 startPosition, KeyCodes key, KeyboardInput keyboard, MouseInput mouse)
        {
            Text = text;
            StartPosition = startPosition;
            Key = key;
            _keyboard = keyboard;
            _mouse = mouse;
        } 

        public void Draw(Canvas canvas)
        {
            if (DoesMouseOver())
                canvas.DrawText(StartPosition, "> " + Text);
            else
                canvas.DrawText(StartPosition, Text);
        }

        private bool DoesMouseOver()
        {
            return
                StartPosition.Y == _mouse.CursorPosition.Y &&
                _mouse.CursorPosition.X >= StartPosition.X &&
                _mouse.CursorPosition.X <= StartPosition.X+Text.Length;
        }
    }
}
