using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using climbing.Utility;
using climbing.Actor.Enemy;
using Microsoft.Xna.Framework;
using climbing.Device;
using climbing.Actor;

namespace climbing.Scene
{
    class GamePlay : IScene
    {
        private GameDevice gameDevice;
        private bool isEnd;

        // スクローリング画面
        // ------------------
        private ScrollingBackground scrolling1;
        private ScrollingBackground scrolling2;


        // ゲームシステムと関係ある
        // ------------------------
        private Timer gameTime;
        private const float waitTime = 120.0f; // 毎2秒新しいSpawnerを作る。
        private int limitToThree; // 一つ障害物の種類は三つSpawnerだけ。
        private Dictionary<Spawner, Timer> spawnerDict; // Spawnerの辞書
        private List<Character> ep; // EnemyParentのリスト
        protected Score score; // プレイヤーはどこまで進める？
        private int counter; // どちらの障害物をだす
        private Sound sound; // 音の管理
        private Player player;

        private Timer hitCooldown;
        private const float nextHitTimer = 180.0f;
        
        public GamePlay(GameDevice gameDevice)
        {
            this.gameDevice = gameDevice;
            isEnd = false;
        }

        public void Initialize()
        {
            sound = gameDevice.GetSound();
            isEnd = false;
            score = new Score();
            spawnerDict = new Dictionary<Spawner, Timer>();
            ep = new List<Character>();
            limitToThree = 1;
            counter = 1;

            gameTime = new Timer();
            hitCooldown = new Timer();

            scrolling1 = new ScrollingBackground("stage1", new Rectangle(0, 0, 736, 640));
            scrolling2 = new ScrollingBackground("stage2", new Rectangle(0, 640, 736, 640));

            player = new Player("hito", new Vector2(304f, 380f), gameDevice.GetInputState(), 64.0f, 3);
            player.Initialize();
        }

        public void Update(GameTime gameTimer)
        {
            gameTime.Update();
            hitCooldown.Update();
            //Console.WriteLine(spawnerDict.Count());

            // Spawnerを実現する
            if (gameTime.Now() >= waitTime)
            {
                if (counter == 1 && limitToThree <= 9) // 岩のSpawner
                {
                    spawnerDict.Add(new Spawner(1), new Timer(120.0f));
                    counter++;
                }
                else if (counter == 2 && limitToThree <= 9) // ヤギのSpawner
                {
                    spawnerDict.Add(new Spawner(2), new Timer(360.0f));
                    counter++;
                }
                else if (counter == 3 && limitToThree <= 9) // 鳥のSpawner
                {
                    counter = 1;
                    spawnerDict.Add(new Spawner(3), new Timer(480.0f));
                }

                score.Add(3);
                limitToThree++;
                gameTime.ResetTimer();
            }

            // Spawnerがあるなら、障害物を準備する
            if (spawnerDict.Count() != 0)
            {
                foreach (var x in spawnerDict)
                {
                    x.Value.DecreaseTime();
                    if (x.Value.Now() <= 0.0f) // 毎三秒障害を出す
                    {
                        // 障害物を出す
                        if (x.Key.getType() == 1)
                        {
                            ep.Add(new BlackMotion("stone", x.Key.getPosition(), 32.0f));
                            x.Key.RenewPosition(x.Key.getType());
                        }
                        else if (x.Key.getType() == 2) // 他の障害物
                        {
                            ep.Add(new Goat("yagi", x.Key.getPosition(), 32.0f));
                            x.Key.RenewPosition(x.Key.getType());
                        }
                        else if (x.Key.getType() == 3)
                        {
                            ep.Add(new Bird("bard", x.Key.getPosition(), 32.0f));
                            x.Key.RenewPosition(x.Key.getType());
                        }
                            x.Value.ResetCounter();
                     }
                }
            }
                
                // 障害物があるなら、Updateする（動かせるために）
            if (ep.Count() != 0)
            {
                foreach (var x in ep)
                { 
                    x.Update(gameTimer);
                    if (player.IsCollision(x) && hitCooldown.Now() >= nextHitTimer)
                    {
                        if (!player.GuardingStatus())
                        {
                            hitCooldown.ResetTimer();
                            sound.PlaySE("collisionSE");
                            player.DecreaseHealth();
                            if (player.GetHealth() <= 0)
                                isEnd = true;
                        }
                        else
                        {
                            x.setIsOut(true);
                        }
                    }
                }
                // 画面のそとに行く障害物を消す
                ep.RemoveAll(e => e.getIsOut());
            }
                
            // スクローリング画面
            // ------------------
            if (scrolling1.getRect().Y >= 640)
            {
                scrolling1.rect.Y = scrolling2.getRect().Y - scrolling2.getRect().Height;
            }
            if (scrolling2.getRect().Y >= 640)
            {
                scrolling2.rect.Y = scrolling1.getRect().Y - scrolling1.getRect().Height;
            }

            scrolling1.Update();
            scrolling2.Update();

            // プレイヤーアップデート
            // ---------------------
            player.Update(gameTimer);

            sound.PlayBGM("StageBGM");

        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
           
            renderer.DrawTexture("stage1", scrolling1.getRect());
            renderer.DrawTexture("stage2", scrolling2.getRect());

            foreach (var x in ep)
            {
                /*if (player.IsCollision(x))
                {
                    score.Draw(renderer, true);
                }*/
                x.Draw(renderer);
            }

            for(int i = 0; i < player.GetHealth(); i++)
            {
                renderer.DrawTexture("heart", new Vector2(600 - (50 * i), 30));
            }

            if(hitCooldown.Now() < nextHitTimer)
            {
                renderer.DrawTexture("exclamation", new Vector2(player.GetPos().X + 85, player.GetPos().Y - 15));
            }
            player.Draw(renderer);

            score.Draw(renderer, false);
            
            renderer.End();
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public bool IsEnd()
        {
            return isEnd;
        }

        public Scene Next()
        {
            return Scene.Ending;
        }
    }
}
