using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;//Vector2使えるようにするため
using Microsoft.Xna.Framework.Input;//Keyboard入力のやつ使えるようにするため
using Microsoft.Xna.Framework.Graphics;//spriteBatch
using climbing.Actor;//player呼び出し
using climbing.Device;

namespace climbing.Device
{
    class InputState
    {
        //フィールド
        private Vector2 velocity = Vector2.Zero;//移動量
        //キー
        private KeyboardState currentKey;//現在のキー
        private KeyboardState previousKey;//1フレーム前のキー

        private GamePadState currentPad;//現在のパッド
        private GamePadState previousPad;//1フレーム前のパッド

        //コンストラクタ
        public InputState() { }

        public Vector2 Velocity()
        {
            return velocity;
        }

        public void UpdateVelocity(KeyboardState keyState)//キーボード入力処理
        {
            velocity = Vector2.Zero;

            //上キー処理
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1.3f;
            }
            //下キー処理
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                velocity.Y += 1.3f;
            }
            //右キー処理
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X += 1.5f;
            }
            //左キー処理
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X -= 1.0f;
            }

            //斜め移動処理
            if (velocity.Length() != 0.0f)//velocityの長さが0じゃなかったら
            {
                velocity.Normalize();//移動距離１の単位ベクトルにする
            }
        }

        public void resetVelocity()
        {
            velocity = Vector2.Zero;
        }

        public void UpdateVelocity(GamePadState padState)//ゲームパッド入力処理
        {
            //左右移動
            velocity.X += padState.ThumbSticks.Left.X * 1.0f;

            //上下移動
            velocity.Y -= padState.ThumbSticks.Left.Y * 1.0f;

            //斜め移動処理
            if (velocity.Length() != 0.0f)//もしvelocityの長さが0じゃなかったら
            {
                velocity.Normalize();//移動距離１の単位ベクトルにする
            }
        }

        //キー情報の更新
        private void UpdateKey(KeyboardState keyState)
        {
            //現在登録されているキーを１フレーム前のキーに
            previousKey = currentKey;

            //現在のキーを最新のキーに
            currentKey = keyState;
        }
        private void UpdatePad(GamePadState padState)
        {
            //現在登録されているキーを１フレーム前のキーに
            previousPad = currentPad;
            //現在のキーを最新のキーに
            currentPad = padState;
        }
        
        public bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        public bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        public bool IsKeyDown(Keys key)
        {
            //現在チェックしたいキーが押されていたか
            bool current = currentKey.IsKeyDown(key);

            //１フレーム前に押されていたか
            bool previous = previousKey.IsKeyDown(key);

            //現在押されていて、１フレーム前に押されていなければtrue
            return current && !previous;

        }
        public bool IsPadDown(Buttons btn)
        {
            return currentPad.IsButtonDown(btn) && !previousPad.IsButtonDown(btn);
        }


        //更新
        public void Update()
        {


            //現在のキーボードの状態を取得
            var keyState = Keyboard.GetState();//var型はその後の変数名に、合わせた型に変形してくれる

            //現在のゲームパッドの状態を取得
            var padState = GamePad.GetState(PlayerIndex.One);
             
            //移動量の更新をできるようにする
            UpdateVelocity(keyState);//キーボード
            UpdateVelocity(padState);//ゲームパッド

            //キーの更新
            UpdateKey(keyState);
            UpdatePad(padState);

        }

    }
}
