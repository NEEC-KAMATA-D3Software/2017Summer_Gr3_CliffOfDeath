using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using climbing.Actor;

namespace climbing.Actor.Enemy
{
    class BlackMotion : Character
    {
        static Random rand = new Random();
        ///<summary>
        ///コンストラクタ
        /// </summary>
        public BlackMotion(string name, Vector2 position, float radius) : base (name, position, radius)
        {    
            velocity = new Vector2(0, rand.Next(4, 6));

            motion.Add(0, new Rectangle(64 * 0, 0, 64, 64));

            motion.Initialize(new Utility.Range(0, 1), new Utility.Timer(30.0f));
        }

        public override void Update(GameTime gameTime)
        {   
            if (position.Y > 640)
                isOut = true;

            //黒玉移動処理
            position = position + velocity;
        }        
    }
}
