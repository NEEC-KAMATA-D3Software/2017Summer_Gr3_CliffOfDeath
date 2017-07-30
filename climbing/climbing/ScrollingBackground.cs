using climbing.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace climbing
{
    class ScrollingBackground
    {
        private Texture2D texture;
        public Rectangle rect;
        private string name;

        public ScrollingBackground(string name, Texture2D texture, Rectangle rect)
        {
            this.name = name;
            this.texture = texture;
            this.rect = rect;
        }

        public ScrollingBackground(string name, Rectangle rect)
        {
            this.name = name;
            this.rect = rect;
        }

        public Rectangle getRect()
        {
            return rect;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, rect);
        }

        public void Update()
        {
            rect.Y += 2;
        }
    }
}
