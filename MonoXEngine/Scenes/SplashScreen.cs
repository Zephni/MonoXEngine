using Microsoft.Xna.Framework;
using MonoXEngine.EntityComponents;
using System;

namespace MonoXEngine.Scenes
{
    public class SplashScreen : Scene
    {
        public override void Initialise()
        {
            Entity entity = new Entity();
            entity.AddComponent<Drawable>().LoadTexture("Test");

            MonoXEngineGame.Instance.SpriteBatchLayers["Main"].Entities.Add(entity);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
