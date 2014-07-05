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

        //ai player chords
        private int aix;
        private int aiy;

        //Ball chords and bool for collisions
        private Position ballXPosition;
        private Position ballYPosition;
        private int ballx;
        private int bally;
        private Boolean aiCollision = false;
        private Boolean playerCollision = false;
        private Boolean whichWay = true;
        private int ballSpeed = 7;
        Random rnd = new Random();
        private int randNum;
        private int ySpeed;

        //score counter + timer
        private int enemyCounter;
        private int friendlyCounter;
        private Boolean start = true; 

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
            aix = this.Width - 100;
            aiy = this.Height / 2 - 100;

            objPosition = Position.Stop;
            ballXPosition = Position.Left;
            //play.constructPlayer(this.Height);
            //Thread thread = new Thread(new ThreadStart(play.constructPlayer(this.Height, player)));
        }

        /*
        * Initialize new box (The players Pong padles) 
        */
        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            //Print both players and ball on screen 
            e.Graphics.FillRectangle(Brushes.Black, x, y, 30, 150);
            e.Graphics.FillRectangle(Brushes.Black, ballx, bally, 50, 50);
            e.Graphics.FillRectangle(Brushes.Black, aix, aiy, 30, 150);

            //print score on screen 
            drawFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(String.Format("{0}", enemyCounter), drawFont, Brushes.Black, drawRect, drawFormat);
            e.Graphics.DrawString(String.Format("{0}", friendlyCounter), drawFont, Brushes.Black, new RectangleF(50, 50, 800, 800), drawFormat);

            //These two are the chords of ball and player for debug
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
            moveAI();
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
            aiCollision = ballPlayer.hasCollisionAI(aix, aiy, ballx, bally);

            //Ball is moving left
            if (ballXPosition == Position.Left)
            {
                ballx -= ballSpeed;
                ballArch();
                ballMoveRight();
            }

            //The balls move
            else if (ballXPosition == Position.Right)
            {
                ballx += ballSpeed;
                ballArch();
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
                ySpeed = rnd.Next(0, 4);
                ballXPosition = Position.Right;
                randomizeDirection();
                playerCollision = false;
                ballSpeed = ballSpeed + 2;
            }

            //ball hit wall, increase points and reset ball
            if (ballx <= 0)
            {
                ySpeed = rnd.Next(0, 4);
                ballSpeed = 7;
                ballXPosition = Position.Stop;
                ballYPosition = Position.Stop;
                ballx = this.Width / 2 - 100;
                bally = this.Height / 2 - 100;
                enemyCounter += 1;

                //Changes direction the ball starts from each time
                if (whichWay)
                {
                    ballXPosition = Position.Right;
                    whichWay = false;
                }
                else
                {
                    ballXPosition = Position.Left;
                    whichWay = true;
                }
            } 
        }

        /*
         * Moves ball to left ig there is a collision with ball moving to right 
         */
        private void ballMoveLeft()
        {
            //ball hit ai player
            if (aiCollision == true)
            {
                ySpeed = rnd.Next(0, 4);
                ballXPosition = Position.Left;
                randomizeDirection();
                aiCollision = false;
                ballSpeed = ballSpeed + 2;
            }

            //Collision with wall, move ball to left 
            if (ballx >= this.Width)
                {
                    ySpeed = rnd.Next(0, 4);
                    ballSpeed = 7;
                    ballXPosition = Position.Stop;
                    ballYPosition = Position.Stop;
                    ballx = this.Width / 2 - 100;
                    bally = this.Height / 2 - 100;
                    friendlyCounter += 1;

                    //Changes direction the ball starts from each time
                    if (whichWay)
                    {
                        ballXPosition = Position.Right;
                        whichWay = false;
                    }
                    else
                    {
                        ballXPosition = Position.Left;
                        whichWay = true;
                    }
                }
        }

        /*
         * Move ball on y axis up or down based on ySpeed given from random generator
         */
        private void ballArch()
        {
            if (bally <= 0) ballYPosition = Position.Down;
            if (bally >= 628) ballYPosition = Position.Up;

            if (ballYPosition == Position.Stop) randomizeDirection();
            if (ballYPosition == Position.Down && bally < this.Height - 50)
            {
                bally += ySpeed;
            }
            else if (ballYPosition == Position.Up && bally > 0)
            {
                bally -= ySpeed;
            }
        }

        /*
         * Randomize direction of ball on y axis (sometimes go up, sometimes go down)
         */
        private void randomizeDirection()
        {
            randNum = rnd.Next(1, 100);
            if (randNum >= 50)
            {
                ballYPosition = Position.Up;
            }
            else
            {
                ballYPosition = Position.Down;
            }
        }

        private void moveAI()
        {
            if (bally > aiy)
            {
                aiy += 7;
            }
            else aiy -= 7;
        }
    }
}
