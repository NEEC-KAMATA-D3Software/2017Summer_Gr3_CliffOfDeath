using climbing.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace climbing.Scene
{
    class Score
    {
        private int score;

        public Score()
        {
            Initialize();
        }

        public void Initialize()
        {
            score = 0;
        }

        public void Add()
        {
            score++;
        }

        public void Add(int num)
        {
            score += num;
        }

        public int getScore()
        {
            return score;
        }

        public void Draw(Renderer renderer, bool hit)
        {
            renderer.DrawTexture("score", new Microsoft.Xna.Framework.Vector2(50, 10));
            if (hit)
                renderer.DrawNumber("number", new Microsoft.Xna.Framework.Vector2(368, 320), score);
            renderer.DrawNumber("number", new Microsoft.Xna.Framework.Vector2(250, 13), score);
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("score", new Microsoft.Xna.Framework.Vector2(50, 10));
            renderer.DrawNumber("number", new Microsoft.Xna.Framework.Vector2(250, 13), score);
        }
    }
}
