using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanApocalypse
{
    class Laser : Graphic
    {
        public int speed;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="texture">Lasers texture</param>
        /// <param name="position">Lasers position</param>
        /// <param name="speed">Lasers speed</param>
        public Laser(Texture2D texture, Vector2 position, int speed)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
        }
        /// <summary>
        /// Fire the laser, make it move
        /// </summary>
        public void Fire()
        {
            position.X += speed;
        }
        /// <summary>
        /// Draw the laser
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
