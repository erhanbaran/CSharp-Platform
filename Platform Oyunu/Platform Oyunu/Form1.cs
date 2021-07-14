using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Oyunu
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed, force, score = 0, playerSpeed = 7, horizontalSpeed = 5, verticalSpeed = 3, enemyOneSpeed = 5, enemyTwoSpeed = 3;


        public Form1()
        {
            InitializeComponent();
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            txtScore.Text = "Skor: " + score;

            pbPlayer.Top += jumpSpeed;
            if(goLeft == true)
            {
                pbPlayer.Left -= playerSpeed;
            }
            if(goRight == true)
            {
                pbPlayer.Left += playerSpeed;
            }
            if(jumping == true && force < 0)
            {
                jumping = false;
            }
            if(jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach(Control x in this.Controls)
            {
                if( x is PictureBox)
                {

                    if((string)x.Tag == "platform")
                    {
                        if (pbPlayer.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            pbPlayer.Top = x.Top - pbPlayer.Height;

                            if((string)x.Name == "pbHorizontalPlatform" && goLeft == false || (string)x.Name == "pbHorizontalPlatform" && goRight == false)
                            {
                                pbPlayer.Left -= horizontalSpeed;
                            }
                        }

                        x.BringToFront();
                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (pbPlayer.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (pbPlayer.Bounds.IntersectsWith(x.Bounds))
                        {
                            tmrGame.Stop();
                            isGameOver = true;
                            txtScore.Text = "Skor: " + score + Environment.NewLine + "You were killed in your journey!";
                            MessageBox.Show("Oyun bitti kardeş");
                        }
                    }
                }
            }

            pbHorizontalPlatform.Left -= horizontalSpeed;

            if (pbHorizontalPlatform.Left < 0 || pbHorizontalPlatform.Left + pbHorizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            pbVerticalPlatform.Top += verticalSpeed;

            if(pbVerticalPlatform.Top < 195 || pbVerticalPlatform.Top > 581)
            {
                verticalSpeed = -verticalSpeed;
            }

            pbEnemyOne.Left -= enemyOneSpeed;

            if(pbEnemyOne.Left < pictureBox5.Left || pbEnemyOne.Left + pbEnemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            pbEnemyTwo.Left += enemyTwoSpeed;

            if (pbEnemyTwo.Left < pictureBox2.Left || pbEnemyTwo.Left + pbEnemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if(pbPlayer.Top + pbPlayer.Height > this.ClientSize.Height + 50)
            {
                tmrGame.Stop();
                isGameOver = true;
                txtScore.Text = "Skor: " + score + Environment.NewLine + "You Feel To your death";
            }

            if(pbPlayer.Bounds.IntersectsWith(pbDoor.Bounds) && score == 27)
            {
                tmrGame.Stop();
                isGameOver = true;
                txtScore.Text = "Skor: " + score + Environment.NewLine + "Your quest is complete!";
            }
            else
            {
                txtScore.Text = "Skor: " + score + Environment.NewLine + "Collect all the coins";
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if(e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if(jumping == true)
            {
                jumping = false;
            }
            if(e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            txtScore.Text = "Skor: " + score;
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            //reset the position of player, platform and enemies

            pbPlayer.Left = 87;
            pbPlayer.Top = 685;

            pbEnemyOne.Left = 510;
            pbEnemyTwo.Left = 373;

            pbHorizontalPlatform.Left = 238;
            pbVerticalPlatform.Top = 577;

            tmrGame.Start();

        }
    }
}
