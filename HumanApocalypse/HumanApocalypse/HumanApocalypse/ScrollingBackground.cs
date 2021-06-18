using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace HumanApocalypse
{
    class ScrollingBackground
    {
        List<Sprite> sprites;
        Sprite rightMostSprite;
        Sprite leftMostSprite;
        Viewport viewPort;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewPort"></param>
        public ScrollingBackground(Viewport viewPort)
        {
            sprites = new List<Sprite>();
            rightMostSprite = null;
            leftMostSprite = null;
            this.viewPort = viewPort;
        }
        /// <summary>
        /// Loads textures, puts them in the right starting position
        /// </summary>
        /// <param name="contentManager"></param>
        public void LoadContent(ContentManager contentManager)
        {
            rightMostSprite = null;
            leftMostSprite = null;
            float width = 0;
            foreach(Sprite sprite in sprites)
            {
                sprite.LoadContent(contentManager, sprite.assetName);
                sprite.scale = 1;
                if (rightMostSprite == null)
                {
                    sprite.position = new Vector2(viewPort.X, viewPort.Y);
                    leftMostSprite = sprite;
                }
                else
                {
                    sprite.position = new Vector2(rightMostSprite.position.X + rightMostSprite.size.Width, viewPort.Y);
                }
                rightMostSprite = sprite;
                width += sprite.size.Width;
            }
            int index = 0;
            if(sprites.Count > 0 && width < viewPort.Width * 2)
            {
                do
                {
                    Sprite sprite = new Sprite();
                    sprite.assetName = sprites[index].assetName;
                    sprite.LoadContent(contentManager, sprite.assetName);
                    sprite.scale = 1;
                    sprite.position = new Vector2(rightMostSprite.position.X + rightMostSprite.size.Width, viewPort.Y);
                    sprites.Add(sprite);
                    rightMostSprite = sprite;

                    width += sprite.size.Width;

                    index += 1;
                    if (index > sprites.Count - 1)
                    {
                        index = 0;
                    }
                } while (width < viewPort.Width * 2);
            }
        }
        /// <summary>
        /// Add a texture to the list of backgrounds to scroll through
        /// </summary>
        /// <param name="assetName"></param>
        public void AddBackground(string assetName)
        {
            Sprite sprite = new Sprite();
            sprite.assetName = assetName;
            sprites.Add(sprite);
        }
        /// <summary>
        /// Scroll through all backgrounds
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="speed"></param>
        public void Update(GameTime gameTime, int speed)
        {
            foreach(Sprite sprite in sprites)
            {
                if (sprite.position.X < viewPort.X - sprite.size.Width)
                {
                    sprite.position = new Vector2(rightMostSprite.position.X + rightMostSprite.size.Width, viewPort.Y);
                    rightMostSprite = sprite;
                }
            }
            Vector2 direction = Vector2.Zero;
            direction.X = -1;
            foreach (Sprite sprite in sprites)
            {
                sprite.Update(gameTime, new Vector2(speed, 0), direction);
            }
        }
        /// <summary>
        /// Draw the backgrounds
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
        }
    }
}
