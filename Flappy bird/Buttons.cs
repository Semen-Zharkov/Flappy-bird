namespace Flappy_bird
{
    internal class Buttons : Button
    {
        public void CreateButtonPause(EventHandler evh)
        {
            Button buttonPause = new Button();
            buttonPause.Text = "Pause";
            buttonPause.Size = new Size(150, 92);
            buttonPause.Location = new Point(190, 153);
            buttonPause.Click += evh;
            


        }
    }
}