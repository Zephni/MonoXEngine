using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;

namespace MonoXEngine.Scenes
{
    public class SplashScreen : Scene
    {
        Entity entity1;
        
        public override void Initialise()
        {
            entity1 = new Entity(entity => {
                entity.AddComponent<Drawable>().LoadTexture("Test");
            });
        }

        public override void Update(GameTime gameTime)
        {
            entity1.Rotation -= 0.1f;
        }
    }
}
