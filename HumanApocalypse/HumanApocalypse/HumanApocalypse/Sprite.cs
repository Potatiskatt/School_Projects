using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanApocalypse
{
    class Sprite : Graphic
    {
        public string assetName;
        public Rectangle size;
        private float mScale = 1.0f;

        /// <summary>
        /// Calculate size of the sprite
        /// </summary>
        public float scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                size = new Rectangle(0, 0, (int)(texture.Width * scale), (int)(texture.Height * scale));
            }
        }
        /// <summary>
        /// Load textures and make a rectangle that is the same size as the 
        /// </summary>
        /// <param name="theContentManager"></param>
        /// <param name="theAssetName">Name of the texture</param>
        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            texture = theContentManager.Load<Texture2D>(theAssetName);
            assetName = theAssetName;
            size = new Rectangle(0, 0, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }
        /// <summary>
        /// Update the position of the sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public void Update(GameTime gameTime, Vector2 speed, Vector2 direction)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        /// <summary>
        /// Draw the sprite
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
