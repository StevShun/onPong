using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onPong
{
    class ball
    {
        public Boolean hasCollisionPlayer(int x, int y, int ballx, int bally)
        {
            //collision
            if ((ballx <= x + 50) && (bally <= y + 150  && bally >= y - 50))
                return true;
            else return false;
        }

        public Boolean hasCollisionAI(int x, int y, int ballx, int bally)
        {
            //collision
            if ((ballx + 50 >= x) && (bally <= y + 150 && bally >= y - 50)) // old way ((ballx >= x && ballx <= x + 25) && (bally <= y + 150 && bally >= y))
                return true;
            else return false;
        }
    }
}
