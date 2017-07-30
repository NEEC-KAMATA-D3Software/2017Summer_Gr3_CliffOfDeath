using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;//Vector2,GameTime
using Microsoft.Xna.Framework.Graphics;//spriteBatch
using Microsoft.Xna.Framework.Input;
using climbing.Device;//InputStateクラスの呼び出し
using climbing.Utility;

namespace climbing.Actor
{
    class Player : Character
    {
        private InputState inputState;

        private Timer guardCooldown; // Set to 5 seconds later

        private int health;

        private bool isGuarding;

        private enum Direction
        {
            UP, DOWN, LEFT, RIGHT, NORMAL
        };
        private Direction direction;

        public Player(string name, Vector2 position, InputState input, float radius, int health) : base(name, position, radius)
        {
            inputState = input;

            motion.Add(0, new Rectangle(128 * 0, 0, 128, 128));
            motion.Add(1, new Rectangle(128 * 2, 0, 128, 128));
            motion.Add(2, new Rectangle(128 * 0, 128 * 1, 128, 128)); //左
            motion.Add(3, new Rectangle(128 * 1, 128 * 1, 128, 128));
            motion.Add(4, new Rectangle(128 * 2, 128 * 1, 128, 128)); //右
            motion.Add(5, new Rectangle(128 * 3, 128 * 1, 128, 128));

            direction = Direction.NORMAL;

            this.health = health;

            guardCooldown = new Timer();

            Initialize();
        }

        public void Initialize()
        {
            motion.Initialize(new Range(0, 1), new Timer(30.0f));
            isGuarding = false;
        }

        public override void Update(GameTime gameTime)
        {
            motion.Update(gameTime);

            UpdateMotion(inputState.Velocity());
            position += inputState.Velocity();

            var min = new Vector2(50.0f, 0);
            var max = new Vector2(558.0f, 512.0f);
            position = Vector2.Clamp(position, min, max);//minとmaxの範囲を切り取ってそこだけ動けるようにしている

            GuardCondition(isGuarding);

            if (guardCooldown.Now() == 180.0f && inputState.IsKeyDown(Keys.B))
            {
                isGuarding = true;
            }
            else if (guardCooldown.IsTime())
            {
                isGuarding = false;
            }

        }

        public void GuardCondition(bool guarding)
        {
            if (!guarding)
            {
                if (guardCooldown.Now() >= 180.0f)
                {
                    guardCooldown.Change(180.0f);
                }
                else
                {
                    guardCooldown.Update();
                }
            }
            else
            {
                if (guardCooldown.Now() <= 0.0f)
                {
                    guardCooldown.Change(0.0f);
                }
                else
                {
                    guardCooldown.DecreaseTime();
                }
            }
        }

        public void MovingPunishment()
        {
            if (isGuarding && direction != Direction.NORMAL)
            {
                guardCooldown.Change(0.0f);
                isGuarding = false;
            }
        }

        public bool GuardingStatus()
        {
            return isGuarding;
        }

        public void UpdateMotion(Vector2 velocity)
        {

            if ((velocity.Y > 0.0f) && (direction != Direction.DOWN))
            {
                direction = Direction.DOWN;
                motion.Initialize(new Range(0, 1), new Timer(10.0f));
            }
            else if ((velocity.Y < 0.0f) && (direction != Direction.UP))
            {
                direction = Direction.UP;
                motion.Initialize(new Range(0, 1), new Timer(10.0f));
            }
            else if ((velocity.X > 0.0f) && (direction != Direction.RIGHT))
            {
                direction = Direction.RIGHT;
                motion.Initialize(new Range(2, 3), new Timer(10.0f));
            }
            else if ((velocity.X < 0.0f) && (direction != Direction.LEFT))
            {
                direction = Direction.LEFT;
                motion.Initialize(new Range(4, 5), new Timer(10.0f));
            }
            else if ((velocity.X == 0.0f && velocity.Y == 0.0f) && (direction != Direction.NORMAL))
            {
                direction = Direction.NORMAL;
                Initialize();
            }
            MovingPunishment();
        }

        public void DecreaseHealth()
        {
            health--;
        }

        public int GetHealth()
        {
            return health;
        }

        public override void Draw(Renderer renderer)
        {
            if (!isGuarding)
                base.Draw(renderer);
            else
            {
                renderer.DrawTexture("guard", position, Color.LightSlateGray);
            }

            renderer.DrawTexture("pixel", new Rectangle(450, 70, (int)guardCooldown.Now(), 15), Color.LightGreen);

            renderer.DrawTexture("gauge", new Rectangle(450, 70, 180, 15));

            renderer.DrawTexture("shield", new Vector2(455, 55));
        }
    }
}
