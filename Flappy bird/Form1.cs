using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flappy_bird;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;


using System.Windows.Forms;
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;


namespace FlappyBird
{
    public partial class Form1 : Form
    {
        Player bird; // объект класса птички
        TheWall wall1; // объект класса верхней стены
        TheWall wall2; // объект класса нижней стены
        float gravity = 0; // переменная отвечающая за гравитацию
        int score = 0; // кол-во очков
        int countScore = 0; // кол-во очков для ускорения движения
        bool flag = false; // метка для информационного окна
        System.Windows.Forms.Label informationManagement; // окно с информацией о управление
        System.Windows.Forms.Label informationLoss; // окно с информацией о проигрыше
        System.Windows.Forms.Label informationPoints; // окно с кол-вом очков

        /// <summary>
        /// Метод конструктор
        /// </summary>
        public Form1()
        {
            this.BackgroundImage = Image.FromFile("D:\\Проекты\\Flappy bird\\Flappy bird\\Flappy bird\\Resources\\фон.png");
            InitializeComponent();
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(update);
          
            Init();
            InformationWindow();
            Invalidate();
        }

        /// <summary>
        /// Метод для инициализации объектов и присвоение значений переменным
        /// </summary>
        public void Init()
        {
            informationManagement = new System.Windows.Forms.Label();
            informationLoss = new System.Windows.Forms.Label();
            if(informationPoints==null)
                informationPoints = new System.Windows.Forms.Label();
            wasted.Visible = false;
            InformationPoints();
            pointsImage();
            bird = new Player(200, 90);
            wall1 = new TheWall(450, -300, true);
            wall2 = new TheWall(450, 250);
            this.Text = "Flappy Bird";
            if (flag == true) timer1.Start();
        }

        /// <summary>
        /// Метод отвечающий за работу игры 
        /// </summary>
        /// <param name="sender"> ссылка на объект, вызвавающая событие </param>
        /// <param name="e"> объект, относящийся к обрабатываемому событию </param>
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
                wasted.Visible = true;
                InformationLoss();
                informationLoss.Visible = true;
                informationLoss.Text = $"Ваши очки: {score}";
                score = 0;
                countScore = 0;

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
            if (wall2.x == (bird.x - 200))
            {       
                informationPoints.Text = "Score: " + ++score;
                if (score % 10 == 0) countScore++;
            }
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
        /// Метод реализующий движение стен
        /// </summary>
        private void MoveWalls()
        {
            if (countScore == 0)
            {
                wall1.x -= 5f;
                wall2.x -= 5f;
            }
            else
            {
                wall1.x = wall1.x - (5f * countScore);
                wall2.x = wall2.x - (5f * countScore);
            }
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

            if (e.KeyValue == (char)Keys.Up && bird.isAlive)
            {
                gravity = 0;
                bird.gravityValue = -0.125f;

            }

            if (e.KeyValue == (char)Keys.Enter && flag == false)
            {
                flag = true;
                informationManagement.Visible = false;
                timer1.Start();

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
        /// <param name="sender"> ссылка на объект, вызвавающая событие </param>
        /// <param name="e"> объект, относящийся к обрабатываемому событию </param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            DialogResult dialog = MessageBox.Show(
             "Вы действительно хотите выйти из игры?",
             "Завершение игры",
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

        /// <summary>
        /// Метод для реализации нажатина на кнопку "ПРОДОЛЖИТЬ ИГРУ"
        /// </summary>
        /// <param name="sender"> ссылка на объект, вызвавающая событие </param>
        /// <param name="e"> объект, относящийся к обрабатываемому событию </param>
        private void button1_Click(object sender, EventArgs e)
        {
            wasted.Visible = false;
            informationLoss.Visible = false;
         
            Init();
            Invalidate();
        }

        /// <summary>
        /// Метод заглушка
        /// </summary>
        /// <param name="sender"> ссылка на объект, вызвавающая событие </param>
        /// <param name="e"> объект, относящийся к обрабатываемому событию </param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Метод для реализации окна с информацией о игре
        /// </summary>
        private void InformationWindow()
        {

            informationManagement.Size = new Size(350, 180);
            informationManagement.Text = "Информация об игре:\r\nУправление в игры осуществляется клавишей стрелочки вверх, а также кнопками мыши.\r\nВ верхнем левом углу показаны ваши набранные очки.\r\nС каждыми набранными 10 очками, скорость птички увеличивается.\r\nЧтобы начать игру нажмите на ENTER, для возобновления игры после проигрыша нажмите на иконку c надписью “продолжить игру”\r\n";
            informationManagement.TextAlign = ContentAlignment.MiddleCenter;
            informationManagement.Location = new Point(90, 80);
            //Color transparentColor = Color.FromArgb(128, 255, 255, 255);
            //informationManagement.BackColor = transparentColor;
            this.Controls.Add(informationManagement);


        }

        /// <summary>
        /// Метод для реализации окна после проигрыша с информацией о набранных очках
        /// </summary>
        private void InformationLoss()
        {
            informationLoss.Size = new Size(230, 90);
            informationLoss.TextAlign = ContentAlignment.TopCenter;
            informationLoss.Location = new Point(150, 120);
            informationLoss.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular);
            //Color transparentColor = Color.FromArgb(0, 255, 255, 255);
            this.Controls.Add(informationLoss);
        }

        /// <summary>
        /// Метод длч реализации окна с информационей о кол-ве набранных очков
        /// </summary>
        private void InformationPoints()
        {
            informationPoints.Text = "Score: " + score.ToString();
            informationPoints.Update();
            informationPoints.Size = new Size(120, 30);
            informationPoints.Location = new Point(385, 0);
            informationPoints.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular);
            Color transparentColor = Color.FromArgb(1, 255, 255, 255);
            informationPoints.BackColor = transparentColor;
            this.Controls.Add(informationPoints);

        }

        /// <summary>
        /// Метод для отображение иконки монет
        /// </summary>
        private void pointsImage()
        {
            var imageControl = new PictureBox();
            imageControl.Image = Image.FromFile("D:\\Проекты\\Flappy bird\\Flappy bird\\Flappy bird\\Resources\\points.png");
            imageControl.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(imageControl);
            Color transparentColo = Color.FromArgb(1, 255, 255, 255);
            imageControl.BackColor = transparentColo;
            imageControl.BringToFront();
            imageControl.Size = new Size(23, 23);
            imageControl.Location = new Point(500, 5);
        }
    }
}