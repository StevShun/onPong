using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace onPong
{
    static class Program
    {
        //PictureBox picBoxPlayer, picBoxAI, picBoxBall;
        //Timer gameTime;

        const int SCREEN_WIDTH = 800;
        const int SCREEN_HEIGHT = 600;

        //Size sizePlayer = new Size(25, 100);
        //Size sizeAI = new Size(25, 100);
        //Size sizeBall = new Size(20, 20);

        static int ballSpeedX = 3;
        static int ballSpeedY = 3;
        static int gameTimeInterval = 1;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormView());
        }
    }
}
