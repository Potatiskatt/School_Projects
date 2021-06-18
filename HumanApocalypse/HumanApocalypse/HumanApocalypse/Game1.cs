using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HumanApocalypse
{
    /// <summary>
    /// States that the game can be in
    /// </summary>
    public enum GameState
    {
        Menu,
        Game,
        Death,
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //Textures
        Texture2D city;
        Texture2D street;
        Texture2D menuBackground;
        Texture2D humanTexture;
        Texture2D soldierTexture;
        Texture2D heartTexture;
        Texture2D barrelTexture;
        Texture2D deathBackground;

        //Animations
        SpriteAnimation unicornAnimation;
        SpriteAnimation humanAnimation;
        SpriteAnimation soldierAnimation;
        int unicornNmrOfFrames;
        public int enemyNmrOfFrames;

        //Objects
        Unicorn unicorn;
        Soldier soldier;
        Human human;
        Laser laser;
        Heart heart;
        Barrel barrel;
        List<Barrel> barrels;
        List<Heart> hearts;
        List<Enemy> enemies;
        ScrollingBackground scrollingBackground;

        //Variables
        int score = 0;
        
        Random random;

        //Fonts
        SpriteFont font;

        GameState gameState = new GameState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //Windowsize
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1800;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //When game starts the menu will show and the lists are initialized.
            gameState = GameState.Menu;
            enemies = new List<Enemy>();
            hearts = new List<Heart>();
            barrels = new List<Barrel>();
            random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here 

            //Enemies and objects, load textures
            enemyNmrOfFrames = 30;
            humanTexture = Content.Load<Texture2D>("human");
            soldierTexture = Content.Load<Texture2D>("soldier");
            heartTexture = Content.Load<Texture2D>("heart");
            barrelTexture = Content.Load<Texture2D>("barrel");

            //Scrolling background
            city = Content.Load<Texture2D>("city");
            scrollingBackground = new ScrollingBackground(this.GraphicsDevice.Viewport); 
            //The scrolling background should scroll through the same picture, 5 are loaded straight away
            for(int i = 0; i<5; i++)
            {
                scrollingBackground.AddBackground("city");
            }
            scrollingBackground.LoadContent(this.Content);

            //Background, menu and deathscreen 
            street = Content.Load<Texture2D>("street");
            menuBackground = Content.Load<Texture2D>("menu");
            deathBackground = Content.Load<Texture2D>("death");

            //Player, load animation
            unicornNmrOfFrames = 15;
            unicornAnimation = new SpriteAnimation(Content.Load<Texture2D>("unicornAnimation"), unicornNmrOfFrames);
            unicornAnimation.isLooping = true;
            unicornAnimation.framesPerSecond = 30;

            //Initialize unicorn and laser
            unicorn = new Unicorn(unicornAnimation, new Vector2(30, 100), 3, 7); //Health, speed  
            laser = new Laser(Content.Load<Texture2D>("laser"), new Vector2(unicorn.position.X*2, unicorn.position.Y*2), 20); //Make sure the laser is positioned behind the unicorn

            //Fonts
            font = Content.Load<SpriteFont>("SpriteFont");
           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(gameState == GameState.Menu || gameState == GameState.Death)//Check what state we are in, if we are in menu or death do this
            {
                KeyboardState keyboard = Keyboard.GetState();
                //Start the game if enter is pressed, exit if escape is pressed
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    gameState = GameState.Game;
                }
                if (keyboard.IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }
            }
            else if(gameState == GameState.Game)//Check what state we are in, if we are in game do this
            {
                KeyboardState keyboard = Keyboard.GetState();
                //If escape is pressed, exit the game
                if (keyboard.IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }

                // TODO: Add your update logic here

                //Update the scrolling background
                scrollingBackground.Update(gameTime, 160); 

                //Update animations
                humanAnimation = new SpriteAnimation(humanTexture, enemyNmrOfFrames);
                humanAnimation.isLooping = true;
                humanAnimation.framesPerSecond = 30;
                soldierAnimation = new SpriteAnimation(soldierTexture, enemyNmrOfFrames);
                soldierAnimation.isLooping = true;
                soldierAnimation.framesPerSecond = 30;

                //Spawn enemies and objects
                if (random.Next(0, 1000) <= 10)
                {
                    Vector2 position = new Vector2(2000, random.Next(370, 800 - humanAnimation.texture.Height));
                    human = new Human(humanAnimation, position, 1, 10, 5000); //Health, speed, strength, spawnchance
                    enemies.Add(human);
                }
                if (random.Next(0, 2000) <= 10)
                {
                    Vector2 position = new Vector2(2000, random.Next(370, 800 - soldierAnimation.texture.Height));
                    soldier = new Soldier(soldierAnimation, position, 2, 5, 10000, 1); //Health, speed, strength, spawnchance, healpwr
                    enemies.Add(soldier);
                }
                if (random.Next(0, 8000) <= 10)
                {
                    Vector2 position = new Vector2(2000, random.Next(370, 800 - heartTexture.Height));
                    heart = new Heart(heartTexture, position, 10);
                    hearts.Add(heart);
                }
                if (random.Next(0, 8000) <= 10)
                {
                    Vector2 position = new Vector2(2000, random.Next(370, 800 - barrelTexture.Height));
                    barrel = new Barrel(barrelTexture, position, 10);
                    barrels.Add(barrel);
                }

                //Update the player
                unicorn.Update(gameTime); 

                //Update enemies and objects
                for (int i = hearts.Count - 1; i >= 0; i--)
                {
                    hearts[i].Update(gameTime);
                }
                for (int i = barrels.Count - 1; i >= 0; i--)
                {
                    barrels[i].Update(gameTime);
                }
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    enemies[i].Update(gameTime);
                    if (enemies[i].position.X <= 0)
                    {
                        enemies.RemoveAt(i);
                        score--;
                    }
                }

                //Check if unicorn is shooting, fire the laser or place it behind the unicorn
                switch (unicorn.isShooting)
                {
                    case true:
                        laser.Fire();
                        if (laser.position.X > 1800)
                        {
                            unicorn.isShooting = false;
                        }
                        break;
                    case false:
                        laser.position.X = unicorn.position.X + 80;
                        laser.position.Y = unicorn.position.Y + 40;
                        break;
                }

                //Check for collisions
                //Draw a rectangle around the player and laser in their current position
                Rectangle unicornRectangle = new Rectangle((int)unicorn.position.X, (int)unicorn.position.Y, unicorn.animation.texture.Width/unicornNmrOfFrames, unicorn.animation.texture.Height);
                Rectangle laserRectangle = new Rectangle((int)laser.position.X, (int)laser.position.Y, laser.texture.Width, laser.texture.Height);
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Draw a rectangle around the enemies in their current position
                    Rectangle enemyRectangle = new Rectangle((int)enemies[i].position.X, (int)enemies[i].position.Y, enemies[i].width/enemyNmrOfFrames, enemies[i].height);
                    //Check for collisions
                    if (unicornRectangle.Intersects(enemyRectangle))
                    {
                        unicorn.health--;
                        enemies.RemoveAt(i);
                    }
                    if (laserRectangle.Intersects(enemyRectangle))
                    {
                        unicorn.isShooting = false;
                        score++;
                        enemies[i].health--;
                        if (enemies[i].health <= 0)
                        {
                            enemies.RemoveAt(i);
                        }
                    } 
                }
                for(int i = 0; i < hearts.Count; i++)
                {
                    //Draw a rectangle around the hearts in their current position
                    Rectangle heartRectangle = new Rectangle((int)hearts[i].position.X, (int)hearts[i].position.Y, hearts[i].texture.Width, hearts[i].texture.Height);
                    //Check for collisions
                    if (unicornRectangle.Intersects(heartRectangle))
                    {
                        unicorn.health++;
                        hearts.RemoveAt(i);
                    }
                }
                for(int i = 0; i < barrels.Count; i++)
                {
                    //Draw a rectangle around the barrels in their current position
                    Rectangle barrelRectangle = new Rectangle((int)barrels[i].position.X, (int)barrels[i].position.Y, barrels[i].texture.Width, barrels[i].texture.Height);
                    //Check for collisions
                    if (unicornRectangle.Intersects(barrelRectangle))
                    {
                        unicorn.health--;
                        barrels.RemoveAt(i);
                    }
                }

                //Check if unicorn is dead, change the state  
                if(unicorn.health <= 0)
                {
                    gameState = GameState.Death;
                }

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if(gameState == GameState.Menu) //Check what state we are in, if we are in menu draw the menu-screen
            {
                spriteBatch.Begin();
                spriteBatch.Draw(menuBackground, new Rectangle(0, 0, 1800, 800), Color.White);
                spriteBatch.End();
            }
            else if(gameState == GameState.Death)//Check what state we are in, if we are in death draw the death-screen
            {
                spriteBatch.Begin();
                spriteBatch.Draw(deathBackground, new Rectangle(0, 0, 1800, 800), Color.White);
                spriteBatch.End();
            }
            else if(gameState == GameState.Game)//Check what state we are in, if we are in game draw all objects needed, score etc
            {
                // TODO: Add your drawing code here
                spriteBatch.Begin();

                //Draw background
                scrollingBackground.Draw(spriteBatch);
                spriteBatch.Draw(street, new Rectangle(0, 371, 1800, 574), Color.White);

                //Draw laser and unicorn
                laser.Draw(spriteBatch);
                unicorn.Draw(gameTime, spriteBatch);

                //Draw hearts and enemies
                for (int i = 0; i < hearts.Count; i++)
                {
                    hearts[i].Draw(gameTime, spriteBatch);
                }
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(gameTime, spriteBatch); 
                }
                for(int i = 0; i < barrels.Count; i++)
                {
                    barrels[i].Draw(gameTime, spriteBatch);
                }

                //Draw text
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "Health: " + unicorn.health, new Vector2(0, 20), Color.White);

                spriteBatch.End();

                base.Draw(gameTime);
            }      
        }
    }
}
