using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanApocalypse
{
    public abstract class SpriteManager : Graphic
    { 
        public Color color = Color.White;
        public Vector2 origin;
        public float rotation = 0f;
        public float scale = 1.3f;
        public SpriteEffects spriteEffect;
        protected Rectangle[] rectangles;
        protected int frameIndex = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="texture">Objects texture, spritesheet</param>
        /// <param name="frames">Number of frames on spritesheet</param>
        public SpriteManager(Texture2D texture, int frames)
        {
            position = Vector2.Zero;
            this.texture = texture;
            int width = texture.Width / frames;
            rectangles = new Rectangle[frames];
            for(int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle(i * width, 0, width, texture.Height);
            }
        }
        /// <summary>
        /// Draw the sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangles[frameIndex], color, rotation, origin, scale, spriteEffect, 0f);
        }
    }
}
