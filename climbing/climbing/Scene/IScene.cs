using climbing.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace climbing.Scene
{
    interface IScene
    {
        void Initialize();
        void Update(GameTime gameTimer);
        void Draw(Renderer renderer);

        void Shutdown();

        bool IsEnd();
        Scene Next();
    }
}
