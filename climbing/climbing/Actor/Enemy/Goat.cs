using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using climbing.Utility;

namespace climbing.Actor.Enemy
{
    class Goat : Character
    {
        ///<summary>
        ///コンストラクタ
        /// </summary>
        public Goat(string name, Vector2 position, float radius) : base (name, position, radius)
        {
            motion.Add(0, new Rectangle(64 * 0, 0, 64, 64));

            motion.Add(1, new Rectangle(64 * 1, 0, 64, 64));

            motion.Initialize(new Range(0, 1), new Timer(50.0f));
        }

        public override void Update(GameTime gameTime)
        {
            motion.Update(gameTime);

            position.X += -3.0f;

            position.Y = originalPosition.Y + (-(float)Math.Cos(position.X / 50) * 50);

            if (position.X < 80)
                isOut = true;
        }
    }
}

    

