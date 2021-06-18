using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanApocalypse
{
    class Enemy : Graphic
    {
        public int health;
        public int speed;
        public int spawnChance;
        public SpriteAnimation animation;
        //Calculate width of one frame
        public int width
        {
            get { return animation.texture.Width / 15; }
        }
        //Calculate height of one frame
        public int height
        {
            get { return animation.texture.Height; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="animation">Enemys animation</param>
        /// <param name="position">Enemys position</param>
        /// <param name="health">Enemys health</param>
        /// <param name="speed">Emeys speed</param>
        /// <param name="spawnChance">Enemys chance of spawning</param>
        public Enemy(SpriteAnimation animation, Vector2 position, int health, int speed, int spawnChance)
        {
            this.animation = animation;
            this.position = position;
            this.health = health;
            this.speed = speed;
            this.spawnChance = spawnChance;
        }
        /// <summary>
        /// Update position and animation
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            position.X -= speed;
            animation.Update(gameTime);
            animation.position = this.position;
        }
        /// <summary>
        /// Draw the animation
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
