using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System.Collections.Generic;

namespace MonoXEngine.Scenes
{
    public class DebugScene : Scene
    {
        Entity testentity;
        Sprite sprite;
        List<Rectangle> AnimList = new List<Rectangle>();

        public override void Initialise()
        {
            new Entity(entity => {
                entity.AddComponent<DrawableRectangle>().Initialise(new Rectangle(0, 0, 256, 240), Color.SteelBlue);
            });

            testentity = new Entity(entity => {
                sprite = entity.AddComponent<Sprite>();
                sprite.LoadTexture("Test");
                sprite.SourceRectangle = new Rectangle(0, 0, 128, 128);
            });

            AnimList.Add(new Rectangle(32 * 0, 0, 32, 32));
            AnimList.Add(new Rectangle(32 * 1, 0, 32, 32));
            AnimList.Add(new Rectangle(32 * 2, 0, 32, 32));
            AnimList.Add(new Rectangle(32 * 3, 0, 32, 32));
            AnimList.Add(new Rectangle(32 * 1, 32, 42, 42));

            sprite.SourceRectangle = AnimList[0];
        }

        bool KeyPressed = false;
        int CurrentFrame = 0;

        public override void Update(GameTime gameTime)
        {
            if(!KeyPressed && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                KeyPressed = true;
                CurrentFrame++;
                if (CurrentFrame >= AnimList.Count)
                    CurrentFrame = 0;

                sprite.SourceRectangle = AnimList[CurrentFrame];
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
                KeyPressed = false;
        }
    }
}
