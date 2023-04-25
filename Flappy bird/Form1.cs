using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flappy_bird;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        Player bird;
        TheWall wall1;
        TheWall wall2;
        TheWall wall3;
        TheWall wall4;
        TheWall wall5;
        TheWall wall6;
        Buttons button = new Buttons();
        Button buttonPause;

        float gravity;
        public Form1()
        {
            this.BackgroundImage = Image.FromFile(@"D:\Проекты\Flappy bird\картинки\фон.png");
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(update);


            Init();
            Invalidate();
        }

        public void Init()
        {
            bird = new Player(200, 90);
            wall1 = new TheWall(450, -300, true);
            wall2 = new TheWall(450, 250);
            //wall3 = new TheWall(1100, -100, true);
            //wall4 = new TheWall(1100, 350);
            //wall5 = new TheWall(1400, -20, true);
            //wall6 = new TheWall(1400, 430);




            gravity = 0;
            this.Text = "Flappy Bird Score: 0";
            timer1.Start();


        }

        private void update(object sender, EventArgs e)
        {
            if (bird.y > 600)
            {
                bird.isAlive = false;
                timer1.Stop();
                Init();
            }

            if (Collide(bird, wall1) || Collide(bird, wall2))
            {
                bird.isAlive = false;
                timer1.Stop();
                Init();
            }

            if (bird.gravityValue != 0.1f)
                bird.gravityValue += 0.005f;
            gravity += bird.gravityValue;
            bird.y += gravity;

            if (bird.isAlive)
            {
                MoveWalls();
            }

            Invalidate();
        }

        /// <summary>
        /// Метод обрабатывающий столкновение птички со стеной
        /// </summary>
        /// <param name="bird"> объект птички </param>
        /// <param name="wall"> объект стены </param>
        /// <returns></returns>
        private bool Collide(Player bird, TheWall wall)
        {
            PointF delta = new PointF();
            delta.X = (bird.x + bird.size / 2) - (wall.x + wall.sizeX / 2);
            delta.Y = (bird.y + bird.size / 2) - (wall.y + wall.sizeY / 2);
            if (Math.Abs(delta.X) <= bird.size / 2 + wall.sizeX / 2)
            {
                if (Math.Abs(delta.Y) <= bird.size / 2 + wall.sizeY / 2)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Метод для создания новой стены 
        /// </summary>
        /// <param name="sender"> ссылка на объект, вызвавающая событие </param>
        /// <param name="e"> объект, относящийся к обрабатываемому событию </param>
        private void NewWall(object sender, EventArgs e)
        {
            if (wall2.x == bird.x - 100) this.Text = "Flappy Bird Score: " + ++bird.score;

            if (wall1.x < bird.x - 490)
            {
                Random r = new Random();
                int y1;
                y1 = r.Next(-380, -250);
                wall1 = new TheWall(520, y1, true);
                wall2 = new TheWall(520, y1 + 550);
            }
        }

        /// <summary>
        /// Метод движение стен
        /// </summary>
        private void MoveWalls()
        {
            wall1.x -= 5f;
            wall2.x -= 5f;
            NewWall(null, null);
        }

        /// <summary>
        /// Метод для отрисовки объктов в игре
        /// </summary>
        /// <param name="sender"> ссылка на объект, вызвавающая событие </param>
        /// <param name="e"> объект, относящийся к обрабатываемому событию </param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.DrawImage(bird.birdImg, bird.x, bird.y, bird.size, bird.size);
            
            graphics.DrawImage(wall1.wallImg, wall1.x, wall1.y, wall1.sizeX, wall1.sizeY);
            graphics.DrawImage(wall2.wallImg, wall2.x, wall2.y, wall2.sizeX, wall2.sizeY);
        }

        /// <summary>
        /// Метод реализующий движение птички при нажатие на клавиши клавиатуры
        /// </summary>
        /// <param name="sender"> ссылка на объект, вызвавающая событие </param>
        /// <param name="e"> объект, относящийся к обрабатываемому событию </param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == (char)Keys.Space || e.KeyValue == (char)Keys.Up) && bird.isAlive)
            {
                gravity = 0;
                bird.gravityValue = -0.125f;

            }
            

        }

        /// <summary>
        /// Метод реализующий движение птички при нажатие на кнопки мыши
        /// </summary>
        /// <param name="sender"> ссылка на объект, вызвавающая событие </param>
        /// <param name="e"> объект, относящийся к обрабатываемому событию </param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == (char)Keys.LButton && bird.isAlive)
            {
                gravity = 0;
                bird.gravityValue = -0.125f;
            }
        }

        /// <summary>
        /// Метод для реализации выхода из приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            DialogResult dialog = MessageBox.Show(
             "Вы действительно хотите выйти из программы?",
             "Завершение программы",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Warning
            );
            if (dialog == DialogResult.Yes) e.Cancel = false;
      
            else
            {
                e.Cancel = true;
                timer1.Start();
            }
        }


    }
}