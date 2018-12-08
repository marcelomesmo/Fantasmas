﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phantoms.Entities.Sprites;
using Phantoms.Helpers;
using System.Collections.Generic;

namespace Phantoms.Entities
{
    public class Vortex : Body
    {
        public string Destiny { get; set; }

        public Vortex(Texture2D spriteSheet, Vector2 position) : base(position, sprite: GetAnimationDefault(spriteSheet)) { }

        public Vortex(string textureName, Vector2 position) : base(position, sprite: GetAnimationDefault(Loader.LoadTexture(textureName))) { }

        protected static AnimatedSprite GetAnimationDefault(Texture2D spriteSheet)
        {
            Dictionary<string, Frame[]> animationFrames = new Dictionary<string, Frame[]>();
            animationFrames.Add("Default", new Frame[]
            {
                new Frame() { Name = "spin_1", Source = new Rectangle(0, 0, 90, 84), Duration = 100 },
                new Frame() { Name = "spin_2", Source = new Rectangle(90, 0, 90, 84), Duration = 100 },
                new Frame() { Name = "spin_3", Source = new Rectangle(180, 0, 90, 84), Duration = 100 }
            });

            return new AnimatedSprite(spriteSheet, animationFrames);
        }
    }
}
