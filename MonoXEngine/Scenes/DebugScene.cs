using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System.Collections.Generic;

namespace MonoXEngine.Scenes
{
    public class DebugScene : Scene
    {
        public override void Initialise()
        {
            new Entity(entity => {
                Sprite sprite = entity.AddComponent<Sprite>();
                sprite.LoadTexture("Test");
                sprite.AddAnimation(new Animation(
                    "Test Animation",
                    new List<Rectangle>(){
                        new Rectangle(32 * 0, 0, 32, 32),
                        new Rectangle(32 * 1, 0, 32, 32),
                        new Rectangle(32 * 2, 0, 32, 32),
                        new Rectangle(32 * 3, 0, 32, 32),
                        new Rectangle(32 * 1, 32, 42, 42)
                    },
                    0.5f
                ));

                sprite.RunAnimation("Test Animation");
            });
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
