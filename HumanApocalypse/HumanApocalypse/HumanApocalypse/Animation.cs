using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HumanApocalypse
{
    class Animation : Graphic
    {
        int currentFrame = 0;
        int frameWidth;
        int frameHeight;
        int spriteSpeed;
        int frameCount;
        Rectangle sourceRect = new Rectangle();
        Rectangle destinationRect = new Rectangle();

        /// <summary>
        /// Initialize, constructor
        /// </summary>
        /// <param name="texture">Objects texture, spritesheet</param>
        /// <param name="position">Objects position</param>
        /// <param name="frameWidth">Width of one frame on the spritesheet</param>
        /// <param name="frameHeight">Height of one frame on the spritesheet</param>
        /// <param name="frameCount">Number of frames on the spritesheet</param>
        /// <param name="spriteSpeed">Speed</param>
        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int spriteSpeed)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.spriteSpeed = spriteSpeed;

            this.position = position;
            this.texture = texture;
        }
        /// <summary>
        /// Update current frame
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            frameCount++;
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            destinationRect = new Rectangle((int)position.X - (int)frameWidth /2, (int)position.Y - (int)frameHeight /2, (int)frameWidth, (int)frameHeight);
        }
        /// <summary>
        /// Draw the animation
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRect, sourceRect, Color.White);
        }
    }
}
