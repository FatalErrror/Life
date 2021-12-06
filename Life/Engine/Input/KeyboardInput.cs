using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Life.Engine.Input
{
    public partial class KeyboardInput
    {
        private const int KEY_PRESSED = 0x8000;

        private Dictionary<KeyCodes, KeyHandler> _observedKeys;

        public KeyboardInput()
        {
            _observedKeys = new Dictionary<KeyCodes, KeyHandler>();


        }

        public static bool KeyPressed(KeyCodes key)
        {
            //System.Windows.Input.KeyStates
            return (GetKeyState((int)key) & KEY_PRESSED) != 0;
        }

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int key);
    }
}
