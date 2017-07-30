using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using climbing.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace climbing.Scene
{
    class Ending : IScene
    {

        private InputState input;
        private bool isEnd;
        private IScene gamePlay;
        private int score;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice">ゲームデバイス</param>
        /// <param name="gamePlay">ゲームプレイシーン</param>
        public Ending(GameDevice gameDevice, IScene gamePlay)
        {
            this.input = gameDevice.GetInputState();
            this.gamePlay = gamePlay;
            this.score = score;
        }

        public void Draw(Renderer renderer)
        {
            gamePlay.Draw(renderer);

            renderer.Begin();
            renderer.DrawTexture("ending", Vector2.Zero);
            renderer.End();
        }

        public void Initialize()
        {
            isEnd = false;
        }

        public bool IsEnd()
        {
            return isEnd;
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTimer)
        {
            if (input.IsKeyDown(Keys.Space))
                isEnd = true;
        }
    }
}
