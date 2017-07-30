using climbing.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace climbing.Actor
{
    abstract class Character
    {
        protected string name;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 originalPosition;
        protected bool isOut;
        protected float radius;

        protected Motion motion;
        public Character(string name, Vector2 position, float radius)
        {
            this.name = name;
            this.position = position;
            this.radius = radius;
            originalPosition = position;

            motion = new Motion();
        }

        public abstract void Update(GameTime gameTime);
        
        public void newPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, motion.DrawingRange());
        }
        
        public bool getIsOut()
        {
            return isOut;
        }

        public string GetName()
        {
            return name;
        }

        public bool IsCollision(Character other)
        {
            Vector2 myCenter = position + new Vector2(radius, radius);
            Vector2 otherCenter = other.position + new Vector2(other.radius, other.radius);

            float xLength = myCenter.X - otherCenter.X;
            float yLength = myCenter.Y - otherCenter.Y;

            float squareLength = xLength * xLength + yLength * yLength;

            float squareRadius = (radius - 40 + other.radius) * (radius - 40 + other.radius);

            if (squareLength <= squareRadius)
            {
                return true;
            }

            return false;
        }

        public void setIsOut(bool isOut)
        {
            this.isOut = isOut;
        }

        public Vector2 GetPos()
        {
            return position;
        }
    }
}
