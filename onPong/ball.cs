using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onPong
{
    class ball
    {
        private Boolean collision = false;

        public Boolean hasCollisionPlayer(int x, int y, int ballx, int bally)
        {
            //collision
            if ((ballx >= x - 25 && ballx <= x + 25) && (bally < y + 75 && bally > y - 75))
                return true;
            else return false;
        }

        public Boolean hasCollisionAI()
        {
            return collision;
        }
    }
}
