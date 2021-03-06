﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Phantoms.Entities.Sprites;
using Phantoms.Helpers;
using System;
using System.Collections.Generic;

namespace Phantoms.Entities.Ghostly
{
    public class PhantomExpression
    {
        public enum Expression { Cry, Love, Bored, Sing }

        private static Texture2D _expressionSheet;
        private static Texture2D ExpressionSheet { get { return GetTexture(); } }

        private Body body;
        private Phantom phantom;
        private AnimatedSprite Animation { get { return ((AnimatedSprite)body.Sprite); } }

        public bool IsExpressing { get { return Animation.IsPlaying; } }
        public Expression Main { get; private set; }

        public PhantomExpression(Phantom phantom)
        {
            Initialize(phantom);
        }

        public void ExpressPhantom()
        {
            ExpressPhantom(GetExpressionName(Main));
        }

        public void ExpressPhantom(Expression expression)
        {
            ExpressPhantom(GetExpressionName(expression));
        }

        public void ExpressPhantom(string expression)
        {
            if (IsExpressing)
                return;
            
            Animation.Stop();
            Animation.Change(expression);
            UpdatePosition();
            Animation.Play();
        }

        public string GetCurrentExpressionName()
        {
            if (!IsExpressing)
                return "";

            return Animation.CurrentName;
        }

        public void UpdatePosition()
        {
            Vector2 position = new Vector2(phantom.Position.X + (phantom.Width * .5f), phantom.Position.Y - (body.Height * .75f));
            body.MoveTo(position, setFacingDirection: false, keepOnScreenBounds: false);
        }

        public void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);
        }

        public void StopExpressing()
        {
            if (IsExpressing)
                Animation.Stop();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            body.Draw(spriteBatch);
        }

        private string GetExpressionName(Expression expression)
        {
            switch (expression)
            {
                case Expression.Cry:
                    return "Cry";

                case Expression.Love:
                    return "Love";

                case Expression.Bored:
                    return "Bored";

                case Expression.Sing:
                    return "Sing";

                default:
                    throw new Exception("ops");
            }
        }

        private void Initialize(Phantom phantom)
        {
            Dictionary<string, Frame[]> expressionsFrames = new Dictionary<string, Frame[]>();
            expressionsFrames.Add("Cry", GetCryFrames());
            expressionsFrames.Add("Love", GetLoveFrames());
            expressionsFrames.Add("Bored", GetBoredFrames());
            expressionsFrames.Add("Sing", GetSingFrames());

            AnimatedSprite animation = null;
            int ciclesCount = 0;
            animation = new AnimatedSprite(ExpressionSheet, expressionsFrames, onFrameChange: (sender, e) =>
            {
                int totalCicles = animation.CurrentName == "Love" || animation.CurrentName == "Sing" ? 3 : 1;

                if (e.HasCompletedCicle)
                    ciclesCount++;

                if (ciclesCount >= totalCicles)
                {
                    ciclesCount = 0;
                    animation.Stop();
                }
            }, autoPlay: false);

            Array values = Enum.GetValues(typeof(Expression));
            Random random = new Random();
            Main = (Expression)values.GetValue(random.Next(values.Length));

            body = new Body(Vector2.Zero, animation, scale: 3f);
            body.SetOrigin(.5f);
            this.phantom = phantom;
        }

        private Frame[] GetCryFrames()
        {
            return new Frame[]
            {
                new Frame() { Name = "cry_1", Source = new Rectangle(0, 12, 7, 7), Duration = 100 },
                new Frame() { Name = "cry_2", Source = new Rectangle(7, 12, 7, 7), Duration = 100 },
                new Frame() { Name = "cry_3", Source = new Rectangle(14, 12, 7, 7), Duration = 100 },
                new Frame() { Name = "cry_3", Source = new Rectangle(0, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_4", Source = new Rectangle(21, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_5", Source = new Rectangle(0, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_6", Source = new Rectangle(21, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_7", Source = new Rectangle(0, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_8", Source = new Rectangle(28, 12, 7, 7), Duration = 100 },
                new Frame() { Name = "cry_9", Source = new Rectangle(35, 12, 7, 7), Duration = 100 },
                new Frame() { Name = "cry_10", Source = new Rectangle(0, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_11", Source = new Rectangle(21, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_12", Source = new Rectangle(0, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_13", Source = new Rectangle(21, 12, 7, 7), Duration = 200 },
                new Frame() { Name = "cry_14", Source = new Rectangle(0, 12, 7, 7), Duration = 200 },
            };
        }

        private Frame[] GetLoveFrames()
        {
            return new Frame[]
            {
                new Frame() { Name = "love_1", Source = new Rectangle(0, 0, 7, 6), Duration = 250 },
                new Frame() { Name = "love_2", Source = new Rectangle(14, 0, 7, 6), Duration = 250 }
            };
        }

        private Frame[] GetBoredFrames()
        {
            return new Frame[]
            {
                new Frame() { Name = "bored_1", Source = new Rectangle(42, 11, 8, 8), Duration = 500 },
                new Frame() { Name = "bored_2", Source = new Rectangle(50, 11, 8, 8), Duration = 250 },
                new Frame() { Name = "bored_3", Source = new Rectangle(42, 11, 8, 8), Duration = 500 },
                new Frame() { Name = "bored_4", Source = new Rectangle(58, 11, 8, 8), Duration = 1000 }
            };
        }

        private Frame[] GetSingFrames()
        {
            return new Frame[]
            {
                new Frame() { Name = "sing_1", Source = new Rectangle(28, 0, 5, 9), Duration = 250 },
                new Frame() { Name = "sing_2", Source = new Rectangle(33, 0, 5, 9), Duration = 250 },
                new Frame() { Name = "sing_3", Source = new Rectangle(38, 0, 5, 9), Duration = 125 },
                new Frame() { Name = "sing_4", Source = new Rectangle(43, 0, 5, 9), Duration = 125 },
                new Frame() { Name = "sing_5", Source = new Rectangle(48, 0, 5, 9), Duration = 125 },
                new Frame() { Name = "sing_6", Source = new Rectangle(53, 0, 5, 9), Duration = 125 }
            };
        }

        private static Texture2D GetTexture()
        {
            if (_expressionSheet == null)
                _expressionSheet = Loader.LoadTexture("phantom_expressions_sheet");

            return _expressionSheet;
        }
    }
}
