using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame {
    public partial class Form1 : Form {
        bool up, down, left, right;
        PictureBox food = new PictureBox();
        PictureBox head = new PictureBox();
        List<PictureBox> Snake = new List<PictureBox>();
        Random random = new Random();
        int score = 0;

        public Form1() {
            InitializeComponent();
        }

        private void StartGame(object sender, EventArgs e) {
            Snake.Add(head);
            timer1.Start();
        }
        private void RestartGame() {
            for (int i = 1; i < Snake.Count; i++) {
                if(Snake.Count !=1)
                    this.Controls.Remove(Snake[i]);
            }
            if (Snake.Count != 1)
                Snake.Clear();
        }
        private PictureBox AddTail() {
            PictureBox tail = new PictureBox();
            tail.Name = score.ToString();
            tail.BackColor = Color.Maroon;
            tail.Width = 10;
            tail.Height = 10;
            this.Controls.Add(tail);
            return tail;
        }
        // Food spawning, snake init
        private void Spawner(PictureBox p, Color color) {
            int X = random.Next(15, (picCanvas.Width - 10) / 10);
            int Y = random.Next(15, (picCanvas.Height -10) / 10);
            p.BackColor = color;
            p.Height = 10;
            p.Width = 10;
            this.Controls.Add(p);
            p.Location = new Point(X*10, Y*10);
        }

        private void Form1_Load(object sender, EventArgs e) {
            Spawner(head, Color.DarkGreen);
            Spawner(food, Color.DarkRed);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            EatFood();
            TailLocation();
            Collision();
            int X = head.Location.X;
            int Y = head.Location.Y;
                if (left == true) {
                    head.Location = new Point(X - 10, Y);
                }
                else if (right == true) {
                    head.Location = new Point(X + 10, Y);
                }
                else if (up == true) {
                    head.Location = new Point(X, Y - 10);
                }
                else if (down == true) {
                    head.Location = new Point(X, Y + 10);
                }
        }

        private void button2_Click(object sender, EventArgs e) {
            timer1.Stop();
        }

        private void Collision() {
            if (head.Location.Y == picCanvas.Height - head.Height
                || head.Location.Y == 12) {
                timer1.Stop(); GameOver();
            }
            else if (head.Location.X == picCanvas.Width - head.Width
                || head.Location.X == 12) {
                timer1.Stop(); GameOver();
            }
        }

        private void TailLocation() {
            for (int i = Snake.Count - 1; i > 0; i--) {
                Snake[0].Location = head.Location;
                Snake[i].Location = Snake[i - 1].Location;
            }
        }
        private void GameOver() {
            lblScore.Text = score.ToString();
            lblGameOver.Visible = true;
            lblTotalScore.Visible = true;
            lblScore.Visible = true;
        }

        private void KeyBindings(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar.ToString().ToLower()) {
                case "w":
                    if (down == false) {
                        right = false;
                        up = true;
                        down = false;
                        left = false;
                    }
                    break;
                case "a":
                    if (right == false) {
                        right = false;
                        up = false;
                        down = false;
                        left = true;
                    }
                    break;
                case "s":
                    if (up == false) {
                        right = false;
                        up = false;
                        down = true;
                        left = false;
                    }
                    break;
                case "d":
                    if (left == false) {
                        right = true;
                        up = false;
                        down = false;
                        left = false;
                    }
                    break;
                default:
                    break;
            }
        }
        private void EatFood() {
            if(head.Location == food.Location) {
                Snake.Add(AddTail());
                score++;
                Spawner(food, Color.Orange);
            }
        }
    }
}
