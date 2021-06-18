using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanApocalypse
{
    class Human : Enemy
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="animation">Humans animation</param>
        /// <param name="position">Humans position</param>
        /// <param name="health">Humans health</param>
        /// <param name="speed">Humans speed</param>
        /// <param name="spawnChance">Humans chance of spawning</param>
        public Human(SpriteAnimation animation, Vector2 position, int health, int speed, int spawnChance) : base(animation, position, health, speed, spawnChance)
        {
            this.animation = animation;
            this.position = position;
            this.health = health;
            this.speed = speed;
            this.spawnChance = spawnChance;
        }
    }
}
