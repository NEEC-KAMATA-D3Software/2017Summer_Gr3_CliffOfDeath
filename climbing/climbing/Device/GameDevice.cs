using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace climbing.Device
{
    class GameDevice
    {
        private Renderer renderer;
        private InputState input;
        private Sound sound;
        private static Random rand;

        public GameDevice(ContentManager contentManager, GraphicsDevice graphics)
        {
            renderer = new Renderer(contentManager, graphics);
            input = new InputState();
            sound = new Sound(contentManager);
            rand = new Random();
        }

        public void Initialize() { }

        public void Update(GameTime gameTime)
        {
            input.Update();
        }

        /// <summary>
        /// 画像オブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public Renderer GetRenderer()
        {
            return renderer;
        }

        public InputState GetInputState()
        {
            return input;
        }

        public Sound GetSound()
        {
            return sound;
        }

        public Random GetRandom()
        {
            return rand;
        }
    }
}
