using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using climbing.Utility;
using climbing.Device;
//using climbing.Actor;
//using climbing.Device;
using climbing.Scene;

namespace climbing
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphicsDeviceManager;
        ContentManager contentManager;

        private GameDevice gameDevice;
      
        private Renderer renderer;
        private SceneManager sceneManager;
        private Sound sound;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferHeight = 640;
            graphicsDeviceManager.PreferredBackBufferWidth = 736;
            Content.RootDirectory = "Content";
            contentManager = Content;
        }

        protected override void Initialize()
        {
            gameDevice = new GameDevice(Content, GraphicsDevice);

            sound = gameDevice.GetSound();

            renderer = gameDevice.GetRenderer();

            sceneManager = new SceneManager();
            sceneManager.Add(Scene.Scene.Title, new Title(gameDevice));

            IScene gamePlay = new GamePlay(gameDevice);
            sceneManager.Add(Scene.Scene.GamePlay, gamePlay);
            
            sceneManager.Add(Scene.Scene.Ending, new Ending(gameDevice, gamePlay));

            sceneManager.Change(Scene.Scene.Title);

            Window.Title = "Cliff of Death";//ウィンドウ画面のタイトル
            base.Initialize();
        }

        protected override void LoadContent()
        {
            renderer.LoadTexture("stage1");
            renderer.LoadTexture("stage2");
            renderer.LoadTexture("yagi");
            renderer.LoadTexture("bard");
            renderer.LoadTexture("score");
            renderer.LoadTexture("number");
            renderer.LoadTexture("title");
            renderer.LoadTexture("heart");
            renderer.LoadTexture("hito");
            renderer.LoadTexture("ending");
            renderer.LoadTexture("stone");
            renderer.LoadTexture("exclamation");

            renderer.LoadTexture("guard");
            renderer.LoadTexture("shield");
            renderer.LoadTexture("gauge");
            renderer.LoadTexture("pixel");

            //BGM
            sound.LoadBGM("TitleBGM");
            sound.LoadBGM("StageBGM");
            //SE
            sound.LoadSE("collisionSE");
        }

        protected override void UnloadContent()
        {
            renderer.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            //ゲーム終了条件,Escで終了できる
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)||(Keyboard.GetState().IsKeyDown(Keys.Escape)))
                this.Exit();
            
            gameDevice.Update(gameTime);
            sceneManager.Update(gameTime);

            //gameManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //描画クリア時を設定（画像が何も無いときは、水色が描画設定されますよっていうやつ）
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            sceneManager.Draw(renderer);

            base.Draw(gameTime);
        }
    }
}
