using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanApocalypse
{
    class Soldier : Enemy
    {
        public int healPwr; //How big the chance of healing 1 health is
        Random random = new Random();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="animation">Soldiers animation</param>
        /// <param name="position">Soldiers position</param>
        /// <param name="health">Soldiers health</param>
        /// <param name="speed">Soldiers speed</param>
        /// <param name="spawnChance">Soldiers chance of spawning</param>
        /// <param name="healPwr">Chance to heal soldier</param>
        public Soldier(SpriteAnimation animation, Vector2 position, int health, int speed, int spawnChance, int healPwr) : base(animation, position, health, speed, spawnChance)
        {
            this.animation = animation;
            this.position = position;
            this.health = health;
            this.speed = speed;
            this.spawnChance = spawnChance;
            this.healPwr = healPwr;
        }
        /// <summary>
        /// Update position and animation and heal
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position.X -= speed;
            animation.Update(gameTime);
            animation.position = this.position;
            if (random.Next(1, 10) < this.healPwr)
            {
                Heal();
            }
        }
        /// <summary>
        /// Heals the soldier one point
        /// </summary>
        public void Heal()
        {
            health++;
        }
    }
}
