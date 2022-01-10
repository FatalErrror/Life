using Life.Engine.Grapfics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Engine.Core
{
    public abstract class Main
    {
        protected Input.KeyboardInput Keyboard => _keyboard;
        protected Input.MouseInput Mouse => _mouse;
        protected Canvas Canvas => _canvas;

        private Input.KeyboardInput _keyboard;
        private Input.MouseInput _mouse;
        private Canvas _canvas;
        private Renderer _renderer;

        private bool _mainWhileProcessing;

        public Main()
        {
            Console.WindowWidth = Console.WindowWidth;
            Console.WindowHeight = Console.WindowHeight;
            _mainWhileProcessing = true;
            _keyboard = new Input.KeyboardInput();
            _mouse = new Input.MouseInput();
            _renderer = new Renderer();
            _canvas = new Canvas(Console.WindowWidth, Console.WindowHeight);

            _canvas.Fill(' ');
        }

        public void StartMainWhile()
        {
            Start();
            while (_mainWhileProcessing)
            {
                PreUpdate();
                Update();
                PostUpdate();
            }
        }


        public void StopMainWhile()
        {
            _mainWhileProcessing = false;
        }

        protected abstract void Start();

        protected abstract void Update();


        private void PreUpdate()
        {
            _keyboard.ProcessAllKeyHandlers();
            _mouse.UpdateCursorPosition();
        }

        private void PostUpdate()
        {
            _renderer.Render(_canvas);
        }
    }
}
