using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;

namespace MonoXEngine.Scenes
{
    public class SplashScreen : Scene
    {
        Entity fader;
        Entity entity2;
        
        public override void Initialise()
        {
            fader = new Entity(entity => {
                entity.AddComponent<DrawableRectangle>().Build(new Rectangle(0, 0, 256, 240), Color.Black);
                entity.LayerName = "Fader";
            });

            entity2 = new Entity(entity => {
                entity.AddComponent<Drawable>().LoadTexture("Test");
            });
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalSeconds > 2)
                fader.Opacity -= 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            entity2.Rotation -= 0.1f;
        }
    }
}
