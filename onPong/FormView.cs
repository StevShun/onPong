using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace onPong
{
    public partial class FormView : Form
    {
        /*
       * Enum to take care of variables
       */
        enum Position
        {
            Left, Right, Up, Down, Stop
        }

        //private player play = new player();
        //private int[] cords = new int[2];
        //private Graphics player;
        private int x;
        private int y;
        private int ballx;
        private int bally;
        private int enemyCounter;
        private Position objPosition;
        private Position ballPosition;
        //Stuff for font and numbers
        private Font drawFont = new Font("Arial", 16);
        private RectangleF drawRect = new RectangleF(50, 50, 50, 50);
        StringFormat drawFormat = new StringFormat();
        



        /*
         * The initial start of the window
         */
        public FormView()
        {
            InitializeComponent();

            x = 50;
            y = this.Height/2 - 100;
            ballx = this.Width / 2 - 100;
            bally = this.Height / 2 - 100;
            objPosition = Position.Stop;
            ballPosition = Position.Left;
            //play.constructPlayer(this.Height);
            //Thread thread = new Thread(new ThreadStart(play.constructPlayer(this.Height, player)));
        }

        /*
        * Initialize new box (The players Pong padles) 
        */
        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            //play.constructPlayer(this.Height, e);
            e.Graphics.FillRectangle(Brushes.Black, x, y, 30, 150);
            e.Graphics.FillRectangle(Brushes.Black, ballx, bally, 50, 50);

            //print score on screen 
            drawFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(String.Format("{0}", enemyCounter), drawFont, Brushes.Black, drawRect, drawFormat); 
            //e.Graphics.DrawImage(new Bitmap("slig.png"), x, y, 100, 100);
        }

        /*
         * Redraws image ever 10ms (This is around 60hz) 
         * x and 'y'(not being used) is updated when keys are pressed
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            movePlayer();
            moveBall();
            enemyPoint();
        }

        /*
         *  Watches for Key presses
         */
        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Left)
            {
                objPosition = Position.Left;
            }
            else if (e.KeyCode == Keys.Right)
            {
                objPosition = Position.Right;
            } */


            if (e.KeyCode == Keys.Down)
            {
                objPosition = Position.Down;
            }
            else if (e.KeyCode == Keys.Up)
            {
                objPosition = Position.Up;
            }
        }

        /*
         * When button is released kill movement
         */
        private void FormView_KeyUp(object sender, KeyEventArgs e)
        {
            objPosition = Position.Stop;
        }

        /*
         * Controlls player movement
         */ 
        private void movePlayer()
        {
            //My move
            if (objPosition == Position.Right)
            {
                x += 10;
                Invalidate();
            }
            else if (objPosition == Position.Left)
            {
                x -= 10;
                Invalidate();
            }

            //Whats being used
            else if (objPosition == Position.Up)
            {
                if (y > 15)
                {
                    y -= 10;
                    Invalidate();
                }
            }
            else if (objPosition == Position.Down)
            {
                if (y < this.Height - 200)
                {
                    y += 10;
                    Invalidate();
                }
            }
        }

        /*
         * Takes care of moving ball
         * TODO up and down if it comes in contact with player (watch player height and width if it coincides bounce it back)
         */
        private void moveBall()
        {
            //The balls move
            if (ballPosition == Position.Left)
            {
                if (ballx >= 0)
                {
                    {
                        ballx -= 7;
                        if (ballx <= 0)
                        {
                            enemyCounter += 1;
                            ballPosition = Position.Right;
                        }
                        Invalidate();
                    }
                }
            }
            else if (ballPosition == Position.Right)
            {
                if (ballx < this.Width)
                {
                    ballx += 7;
                    if (ballx >= this.Width - 50)
                    {
                        ballPosition = Position.Left;
                    }
                    Invalidate();
                }
            }
        }

        private void enemyPoint()
        {
            
        }
    }
}
