using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Engine.Core
{
    public class Main
    {
        private bool _mainWhileProcessing;

        public Main()
        {
            _mainWhileProcessing = true;


            while (_mainWhileProcessing)
            {
                MainWhile();
            }
        }


        public void MainWhile()
        {

        }


    }
}
