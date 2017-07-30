using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace climbing.Device
{
    class Renderer
    {
        private ContentManager contentManager; // コンテンツ管理者
        private GraphicsDevice graphicsDevice; // グラフィック機器
        private SpriteBatch spriteBatch; // スプライトー括

        // Dictionary で複数の画像を管理
        private Dictionary<string, Texture2D> textures = new
            Dictionary<string, Texture2D>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content">Game1のコンテンツ管理者</param>
        /// <param name="graphics">Game1のグラフィック機器</param>
        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="filepath">ファイルまでのパス</param>
        public void LoadTexture(string name, string filepath = "./")
        {
            // ガード節
            // Dictionaryへの2重登録を回避
            if (textures.ContainsKey(name))
            {
#if DEBUG
                System.Console.WriteLine("この" + name + "は Key で、すでに登録してます");
#endif
                return;
            }
            // 画像の読み込みとDictionaryにアセット名と画像を追加
            textures.Add(name, contentManager.Load<Texture2D>(filepath + name));
        }

        /// <summary>
        /// アンロード
        /// </summary>
        public void Unload()
        {
            //　Dictionary 登録情報をクリア
            textures.Clear();
        }

        /// <summary>
        /// 画像開発
        /// </summary>
        public void Begin()
        {
            spriteBatch.Begin();
        }

        /// <summary>
        /// 画像終了
        /// </summary>
        public void End()
        {
            spriteBatch.End();
        }

        /// <summary>
        /// 画像の描画
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="alpha">透明値（0.0 : 透明, 1.0 : 不透明）</param>
        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか？" +
                "大文字小文字間違えませんか？" +
                "LoadTexture で読み込んでますか？" +
                "プログラムを確認してください");

            spriteBatch.Draw(textures[name], position, Color.White * alpha);

        }

        public void DrawTexture(string name, Rectangle rect, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか？" +
                "大文字小文字間違えませんか？" +
                "LoadTexture で読み込んでますか？" +
                "プログラムを確認してください");

            spriteBatch.Draw(textures[name], rect, Color.White * alpha);
        }

        // アニメーション用
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか？" +
                "大文字小文字間違えませんか？" +
                "LoadTexture で読み込んでますか？" +
                "プログラムを確認してください");

            spriteBatch.Draw(
                textures[name],
                position,
                rect,
                Color.White * alpha);
        }


        public void DrawNumber(string name, Vector2 position, int number, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？ \n" +
                "大文字小文字間違えていませんか？ \n" +
                "LoadTextureメソッドでよみこんでいますか？ \n" +
                "プログラムを確認してください \n");

            if(number < 0)
            {
                number = 0;
            }

            foreach (var n in number.ToString())
            {
                spriteBatch.Draw(
                    textures[name],
                    position,
                    new Rectangle((n - '0') * 32, 0, 32, 64),
                    Color.White * alpha);
                position.X += 32;
            }
        }

        public void DrawTexture(string name, Vector2 position, Color color)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか？" +
                "大文字小文字間違えませんか？" +
                "LoadTexture で読み込んでますか？" +
                "プログラムを確認してください");

            spriteBatch.Draw(textures[name], position, color);
        }

        public void DrawTexture(string name, Rectangle rect, Color color)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか？" +
                "大文字小文字間違えませんか？" +
                "LoadTexture で読み込んでますか？" +
                "プログラムを確認してください");

            spriteBatch.Draw(textures[name], rect, color);
        }
    }
}
