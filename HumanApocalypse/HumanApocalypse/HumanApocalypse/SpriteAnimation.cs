using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanApocalypse
{
    class SpriteAnimation : SpriteManager
    {
        private float timeElapsed;
        public bool isLooping = false;
        private float timeToUpdate = 0.05f;
        public int framesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="texture">Texture</param>
        /// <param name="frames">Number of frames</param>
        public SpriteAnimation(Texture2D texture, int frames) : base(texture, frames)
        {
        }
        /// <summary>
        /// Update the animation by changing the frame that is currently showing
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
                if (frameIndex < rectangles.Length - 1)
                {
                    frameIndex++;
                }
                else if (isLooping)
                {
                    frameIndex = 0;
                }
            }
        }
    }
}
