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
        public List<Entity> Entities;

        public Scene()
        {
            this.Entities = new List<Entity>();
            this.Initialise();
        }

        public virtual void Initialise() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Entity entity in this.Entities)
                entity.Draw(gameTime, spriteBatch);
        }
    }
}
