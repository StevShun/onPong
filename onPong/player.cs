using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace onPong
{
    class player
    {

        /*
        * Enum to take care of variables
        */
        enum Position
        {
            Left, Right, Up, Down, Stop
        }

        private Graphics playerPiece;
        private int x;
        private int y;
        private Position objPosition;

        public void constructPlayer(int playHeight)
        {
            x = 50;
            y = playHeight / 2 - 100;
            objPosition = Position.Stop;
           
        }
    }
}
