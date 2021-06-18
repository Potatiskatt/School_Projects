using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanApocalypse
{
    class Barrel : Graphic
    {
        public int speed;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="texture">Barrels texture</param>
        /// <param name="position">Barrels position</param>
        /// <param name="speed">Barrels speed</param>
        public Barrel(Texture2D texture, Vector2 position, int speed)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
        }
        /// <summary>
        /// Update the position
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            position.X -= speed;
        }
        /// <summary>
        /// Draw the barrel
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
