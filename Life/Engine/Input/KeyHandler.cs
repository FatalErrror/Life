namespace Life.Engine.Input
{
   
    public class KeyHandler
    {
        public bool State => KeyboardInput.KeyPressed(_key);

        private KeyCodes _key;

        private bool _button;
        private bool _buttonUnpressed;

        public KeyHandler(KeyCodes key)
        {
            _key = key;
            _buttonUnpressed = true;
            _button = false;
        }


        /// <summary>
        /// Calculate value for current frame
        /// </summary>
        public void Process()
        {
            if (State)
            {
                if (_buttonUnpressed)
                {
                    _buttonUnpressed = false;
                    _button = true;
                }
                else _button = false;
            }
            else
            {
                _buttonUnpressed = true;
                _button = false;
            }
        }

        /// <summary>
        /// Return value for current frame or for previous frame if this function called before Process
        /// </summary>
        public bool Pressed()
        {
            return _button;
        }
    }
}

