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
        public float x;
        public float y;

        public int sizeX;
        public int sizeY;

        public Image wallImg;

        public TheWall(int x, int y, bool isRotatedImage = false)
        {
            wallImg = new Bitmap("D:\\Проекты\\Flappy bird\\картинки\\tube.png");
            this.x = x;
            this.y = y;
            sizeX = 170;
            sizeY = 400;
            if (isRotatedImage)
                wallImg.RotateFlip(RotateFlipType.Rotate180FlipX);
        }
    }
}