using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Life.Engine.Input
{
    public class KeyboardInput
    {
        private const int KEY_PRESSED = 0x8000;

        private readonly Dictionary<KeyCodes, KeyHandler> _observedKeys;

        public KeyboardInput()
        {
            _observedKeys = new Dictionary<KeyCodes, KeyHandler>();
        }

        public bool KeyHandlerExist(KeyCodes key)
        {
            return _observedKeys.ContainsKey(key);
        }

        public void AddKeyHandler(KeyCodes key)
        {
            if (!_observedKeys.ContainsKey(key))
                _observedKeys.Add(key, new KeyHandler(key));
        }

        public void RemoveKeyHandler(KeyCodes key)
        {
            _observedKeys.Remove(key);
        }

        public bool IsPressed(KeyCodes key)
        {
            return _observedKeys[key].Pressed();
        }

        public void ProcessAllKeyHandlers()
        {
            foreach (var item in _observedKeys)
            {
                item.Value.Process();
            }
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
