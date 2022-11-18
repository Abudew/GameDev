using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private double secondCounter = 0;
        private int counter;

        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void GetFramesFromTextureProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heigthOfFrame = height / numberOfHeightSprites;
             
            for (int y = 0; y <= height - heigthOfFrame; y+=heigthOfFrame)
            {
                for (int x = 0; x <= width - widthOfFrame; x+= widthOfFrame)
                {
                    frames.Add(new AnimationFrame(new Rectangle(x, y, widthOfFrame, heigthOfFrame)));
                }
            }
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[counter];
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 4;

            if (secondCounter >= 1d/fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }
    }
}
