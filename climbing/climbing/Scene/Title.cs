using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using climbing.Device;
using climbing.Utility;
using climbing.Actor;
using climbing.Actor.Enemy;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace climbing.Scene
{
    class Title : IScene
    {
        private InputState input;
        private bool isEnd;
        private Motion motion;
        private Motion yagiMotion;
        private Sound sound;

        public Title(GameDevice gameDevice)
        {
            this.input = gameDevice.GetInputState();
            this.sound = gameDevice.GetSound();
            isEnd = false;
        }

        public void Initialize()
        {
            isEnd = false;
             // プレイヤーのアニメーション
            motion = new Motion();
            motion.Add(0, new Rectangle(128 * 0, 0, 128, 128));
            motion.Add(1, new Rectangle(128 * 2, 0, 128, 128));
            motion.Initialize(new Range(0, 1), new Timer(30.0f));
            // ヤギのアニメーション
            yagiMotion = new Motion();
            yagiMotion.Add(0, new Rectangle(64 * 0, 64 * 0, 64, 64));
            yagiMotion.Add(1, new Rectangle(64 * 1, 64 * 0, 64, 64));
            yagiMotion.Initialize(new Range(0, 1), new Timer(20.0f));
        }

        public void Update(GameTime gameTimer)
        {
            sound.PlayBGM("TitleBGM");
            motion.Update(gameTimer);
            yagiMotion.Update(gameTimer);
            if (input.GetKeyTrigger(Keys.Space))
            {
                isEnd = true;
            }
            
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("title", Vector2.Zero);
            renderer.DrawTexture("hito", new Vector2(304f, 380f), motion.DrawingRange());
            renderer.DrawTexture("yagi", new Vector2(150f, 180f), yagiMotion.DrawingRange());
            renderer.End();
        }

        public bool IsEnd()
        {
            return isEnd;
        }

        public Scene Next()
        {
            return Scene.GamePlay;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }
        
    }
}
