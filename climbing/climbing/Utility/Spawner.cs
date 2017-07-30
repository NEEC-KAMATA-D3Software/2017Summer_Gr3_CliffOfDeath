using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using climbing.Actor;
using Microsoft.Xna.Framework;
using climbing.Actor.Enemy;

namespace climbing.Utility
{
    class Spawner
    {
        Vector2 position;
        static Random rand;
        int type;

        public Spawner(int type)
        {
            rand = new Random();
            this.type = type;

            switch (type)
            {
                case 1:
                    position = new Vector2(rand.Next(80, (577 + 80 - 64)), 0.0f); // 64 = 敵の画像の大きさ
                    break;
                case 2:
                    position = new Vector2((577 + 80), rand.Next(100, (640 - 100)));
                    break;
                case 3:
                    position = new Vector2(80 - 64, rand.Next(100, (640 - 100)));
                    break;
            }
        }

        public void RenewPosition(int type)
        {
            switch (type)
            {
                case 1:
                    position = new Vector2(rand.Next(80, (577 + 80 - 64)), 0.0f); // 64 = 敵の画像の大きさ
                    break;
                case 2:
                    position = new Vector2((577 + 80), rand.Next(100, (640 - 100)));
                    break;
                case 3:
                    position = new Vector2(80 - 64, rand.Next(100, (640 - 100)));
                    break;
            }
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public int getType()
        {
            return type;
        }

        //static Random rand;
        //Timer timer;
        //Vector2 currentPos;
        //float stage;
        //int type;
        //EnemyParent ep;

        //public Spawner(int type)
        //{
        //    this.type = type;
        //    rand = new Random();
        //    timer = new Timer();
        //    stage = 180.0f;

        //    switch (type)
        //    {
        //        case 1:
        //            currentPos = new Vector2(rand.Next(0, (577 - 64)), 0.0f);
        //            break;
        //        case 2:
        //            currentPos = new Vector2(0.0f, rand.Next(0, (640 - 64)));
        //            break;
        //        case 3:
        //            currentPos = new Vector2((577 - 64), rand.Next(0, (640 - 64)));
        //            break;
        //    }
        //}

        //public Vector2 GetPos()
        //{
        //    return currentPos;
        //}

        //public void SpawnEnemies()
        //{
        //    if(type == 1)
        //    {
        //        ep = new BlackMotion("black", this.GetPos());
        //    }
        //    else if(type == 2)
        //    {
        //        ep = new Goat("black", this.GetPos());
        //    }
        //    else
        //    {
        //        ep = new Bird("black", this.GetPos());
        //    }
        //}

        //public void Update()
        //{
        //    timer.Update();
            
        //    if(timer.Now() == 360.0f)
        //    {
        //        stage = 90.0f;
        //    }

        //    if(timer.Now() == 480.0f)
        //    {
        //        stage = 45.0f;
        //    }

        //    if (timer.Now() % stage == 0.0f)
        //    {
        //        float x = 0.0f;
        //        float y = 0.0f;
        //        switch (type)
        //        {
        //            case 1:
        //                x = rand.Next(0, (577 - 64));
        //                currentPos = new Vector2(x, y);
        //                BlackMotion bm = new BlackMotion("black", new Vector2(x, y));
        //                break;
        //            case 2:
        //                y = rand.Next(0, (640 - 64));
        //                currentPos = new Vector2(x, y);
        //                // BELUm
        //                break;
        //            case 3:
        //                // BELUM
        //                x = (577.0f - 64.0f);
        //                y = rand.Next(0, (640 - 64));
        //                currentPos = new Vector2(x, y);
        //                break;
        //        }
        //    }
        //}



    }
}
