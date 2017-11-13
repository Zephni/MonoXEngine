using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoXEngine
{
    public class Scene
    {
        public Scene()
        {
            this.Initialise();
        }

        public virtual void Initialise() { }

        public virtual void Update(GameTime gameTime) { }
    }
}
