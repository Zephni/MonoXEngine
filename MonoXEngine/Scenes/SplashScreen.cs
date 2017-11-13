using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System;

namespace MonoXEngine.Scenes
{
    public class SplashScreen : Scene
    {
        Entity entity1;
        Entity entity2;

        public override void Initialise()
        {
            entity1 = new Entity(entity => {
                entity.AddComponent<Drawable>().LoadTexture("Test");
            });

            entity2 = new Entity(entity => {
                entity.AddComponent<Drawable>().LoadTexture("Test");
                entity.Position = new Point(40, 40);
            });
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
