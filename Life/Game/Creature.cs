using Life.Engine.Structs;
using System;

namespace Life.Game
{
    class Creature//¤
    {
        public static long LastId = 1;

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

        public char FirstLetter => Name[0];


        public void LvlUp()
        {
            Lvl++;

            MissChance -= 0.001f;

            FullHP = (int)(FullHP * 1.2);
            Damage = (int)(Damage * 1.1);

            Expa = Lvl * 20;

            HP = FullHP;
            DeathCount = 0;
        }

        public void SetExpa(int add)
        {
            Expa -= add;
            if (Expa <= 0) LvlUp();
        }

        public void SetDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0) DeathCount = 1;
        }

        public void Move(Vector2 movement)
        {
            Position += movement;
        }
        public void Move(Vector2 movement, Rectangle world)
        {
            Position += movement;

            if (Position.X < world.TopLeft.X) Position = new Vector2(world.RightBottom.X, Position.Y);
            if (Position.X > world.RightBottom.X) Position = new Vector2(world.TopLeft.X, Position.Y);

            if (Position.Y < world.TopLeft.Y) Position = new Vector2(Position.X, world.RightBottom.Y);
            if (Position.Y > world.RightBottom.Y) Position = new Vector2(Position.X, world.TopLeft.Y);
        }

        public Creature(string name, Vector2 position)
        {
            ID = LastId++;
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
                    c1.SetDamage(c2.Damage / 8);
                    c1.Move(c1.Position - c2.Position);
                    if (c1.DeathCount > 0) c2.SetExpa(c1.Lvl * 10);
                    return;
                }
            }

            if (c2.HP * 2 < c1.HP)
            {
                if (r.Next(0, 10) < 3)
                {
                    c2.SetDamage(c1.Damage / 8);
                    c2.Move(c2.Position - c1.Position);
                    if (c2.DeathCount > 0) c1.SetExpa(c2.Lvl * 10);
                    return;
                }
            }

            if (r.Next(0, 100) < 95)
            {
                c1.SetDamage(c2.Damage);
                c2.SetDamage(c1.Damage);
                if (c1.DeathCount > 0) c2.SetExpa(c1.Lvl * 10);
                if (c2.DeathCount > 0) c1.SetExpa(c2.Lvl * 10);
            }

        }

    }

}
