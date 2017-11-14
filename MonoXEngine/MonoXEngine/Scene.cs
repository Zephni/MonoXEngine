using Microsoft.Xna.Framework;

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