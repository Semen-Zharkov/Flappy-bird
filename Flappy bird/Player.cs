using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlappyBird
{


    class Player
    {
        // координата X птички
        public float x;

        //координата Y птички
        public float y;

        // размер птички 
        public int size;
        
        // кол-во набранных очков
        public int score;

        // значение гравитации 
        public float gravityValue;

        // картинка птички
        public Image birdImg;

        // переменная хранящая значение, true - если птичка жива
        // false - если разбилась
        public bool isAlive;

        /// <summary>
        /// Метод конструктор где происходит создание птички
        /// </summary>
        /// <param name="x"> координата X птички </param>
        /// <param name="y"> координата Y птички </param>
        public Player(int x, int y)
        {
            birdImg = new Bitmap("D:\\Проекты\\Flappy bird\\картинки\\bird.png");
            this.x = x;
            this.y = y;
            size = 50;
            gravityValue = 0.1f;
            isAlive = true;
            score = 0;
        }
    }
}