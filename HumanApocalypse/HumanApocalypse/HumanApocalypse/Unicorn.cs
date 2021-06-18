using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HumanApocalypse
{
    class Unicorn : Graphic
    {
        public int health;
        public int speed;
        public bool isShooting = false;
        public SpriteAnimation animation;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="animation">Unicorns animation</param>
        /// <param name="position">Unicorns position</param>
        /// <param name="health">Unicorns health</param>
        /// <param name="speed">Unicorns speed</param>
        public Unicorn(SpriteAnimation animation, Vector2 position, int health, int speed)
        {
            this.animation = animation;
            this.position = position;
            this.health = health;
            this.speed = speed;
        }
        /// <summary>
        /// Update position according to user input and update animation
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }
            if (keyboard.IsKeyDown(Keys.Space) && isShooting == false)
            {
                isShooting = true;
            }
            if (position.Y + animation.texture.Height > 800)
            {
                position.Y = 800 - animation.texture.Height;
            }
            if(position.Y < 0)
            {
                position.Y = 0;
            }
            animation.Update(gameTime);
            animation.position = this.position;
        }
        /// <summary>
        /// Draw animation
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
