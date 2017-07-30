using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using climbing.Utility;

namespace climbing.Actor.Enemy
{
    class Bird : Character
    {
        ///<summary>
        ///コンストラクタ
        /// </summary>
        public Bird (string name, Vector2 position, float radius) : base (name, position, radius)
        {
            motion.Add(0, new Rectangle(64 * 0, 0, 64, 64));

            motion.Add(1, new Rectangle(64 * 1, 0, 64, 64));

            motion.Add(2, new Rectangle(64 * 2, 0, 64, 64));

            motion.Initialize(new Range(0, 2), new Timer(10.0f));
        }

        ///<summary>
        ///更新
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
            //移動速度
            velocity = new Vector2(3.0f,1.0f);

            //鳥の移動処理
            position = position + velocity;
        }
    }
}
