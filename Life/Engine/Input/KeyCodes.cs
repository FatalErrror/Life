namespace Life.Engine.Input
{
    //All keys -> https://docs.microsoft.com/ru-ru/dotnet/api/system.windows.forms.keys?view=netframework-4.8
    [System.Flags]
        public enum KeyCodes : int
        {
            Space = 32,
            Left = 37,
            Up = 38,
            Right = 39,
            Down = 40
        }
    }
}
