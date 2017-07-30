using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using climbing.Utility;
using climbing.Actor;

namespace climbing.Device
{
    class Gauge
    {
        //フィールド\
        private Timer timer;

        private InputState inputState;

        private float atMax = 100.0f;//最大の値、時間

        private Rectangle rect;//四角い範囲

        private int nowWidth;//現在の%量

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Gauge(InputState inputState)
        {
            this.inputState = inputState;
            timer = new Timer();
            rect = new Rectangle(30, 30, (int)atMax, 30);
        }

        public void Update(GameTime gameTime)
        {
            timer.UpdateGauge();

            if(timer.Now() >= 100.0f && inputState.IsKeyDown(Keys.B))
            {
                timer.ResetTimer();
            }

            nowWidth = (int)((timer.Now() / atMax));
            if (nowWidth >= 100.0f) { nowWidth = 100; }
        }

        /*
        public void Update(GameTime gameTime)
        {
            timer.UpdateGauge();

            if (timer.Now() >= 100.0f && (inputState.IsKeyDown(Keys.B) || inputState.IsPadDown(Buttons.A)))
            {
                timer.ResetTimer();
            }

            //現在何%か、ゲージの量を計算
            nowWidth = (int)((timer.Now() / fullValue));
            if (nowWidth >= 100.0f) { nowWidth = 100; }
        }

        
        public Timer GetTime() { return timer; }
        public void Draw(Renderer renderer)
        {

            //ゲージの中身を描画
            renderer.Draw("pixel", new Rectangle(bounds.X, bounds.Y, nowWidth, bounds.Height), Color.LightGreen);

            //ゲージの外枠の描画
            renderer.Draw("gauge", bounds, Color.White);
        }

        public void Caluc()
        {
            nowWidth = 0;
        }
        */
    }
}
