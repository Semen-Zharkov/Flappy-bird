using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlappyBird
{
    class TheWall
    {
        // координата X стены
        public float x;

        // координата Y стены
        public float y;

        // ширина стены
        public int sizeX;

        //высота стены
        public int sizeY;

        // картинка стены
        public Image wallImg;

        /// <summary>
        /// Метод конструктор, где происходит создание стен
        /// </summary>
        /// <param name="x"> координата X стены </param>
        /// <param name="y"> координата Y стены </param>
        /// <param name="isRotatedImage"> перменная отвечающая за переворот стены </param>
        public TheWall(int x, int y, bool isRotatedImage = false)
        {
            wallImg = new Bitmap("D:\\Проекты\\Flappy bird\\Flappy bird\\Flappy bird\\Resources\\tube.png");
            this.x = x;
            this.y = y;
            sizeX = 170;
            sizeY = 400;
            if (isRotatedImage)
                wallImg.RotateFlip(RotateFlipType.Rotate180FlipX);
        }
    }
}