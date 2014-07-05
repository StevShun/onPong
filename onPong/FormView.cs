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

        private player play = new player();
        private ball ballPlayer = new ball();
        
        //Player chords 
        private Position objPosition;
        private int x;
        private int y;

        //Ball chords and bool for collisions
        private Position ballPosition;
        private int ballx;
        private int bally;
        private Boolean aiCollision = false;
        private Boolean playerCollision = false;
        private int ballSpeed = 7;

        //score counter + timer
        private int enemyCounter;
        
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

            //player position
            e.Graphics.DrawString(String.Format("x = {0}", x), drawFont, Brushes.Black, new RectangleF(100,100,50,50), drawFormat);
            e.Graphics.DrawString(String.Format("y = {0}", y), drawFont, Brushes.Black, new RectangleF(100, 100,350, 50), drawFormat); 

            //ball position
            e.Graphics.DrawString(String.Format("x = {0}", ballx), drawFont, Brushes.Black, new RectangleF(100, 100, 650, 50), drawFormat);
            e.Graphics.DrawString(String.Format("y = {0}", bally), drawFont, Brushes.Black, new RectangleF(100, 100, 950, 50), drawFormat); 



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
            //My move these first two would control left and right, these are diables as of now 
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
            playerCollision = ballPlayer.hasCollisionPlayer(x, y, ballx, bally);
            aiCollision = ballPlayer.hasCollisionAI();

            //Ball is moving left
            if (ballPosition == Position.Left)
            {
                ballx -= ballSpeed;
                ballMoveRight();
            }

            //The balls move
            else if (ballPosition == Position.Right)
            {
                ballx += ballSpeed;
                ballMoveLeft();
            }

            //Draw new screen
            Invalidate();
        }

        /*
         * Moves ball to right if there is a collision with ball moving to left 
         */
        private void ballMoveRight()
        {
            //ball hit player
            if (playerCollision == true)
            {
                ballPosition = Position.Right;
                playerCollision = false;
                ballSpeed = ballSpeed + 2;
            }
            //ball hit wall, increase points and reset ball
            else if (ballx <= 0)
            {
                ballSpeed = 7;
                enemyCounter += 1;
                ballPosition = Position.Right;
            }
            
        }

        /*
         * Moves ball to left ig there is a collision with ball moving to right 
         */
        private void ballMoveLeft()
        {
            //Collision with wall, move ball to left 
            if (ballx >= this.Width)
            {
                ballPosition = Position.Left;
            }
        }
    }
}
