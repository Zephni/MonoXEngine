using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoXEngine.Scenes
{
    public class DebugScene : Scene
    {
        public override void Initialise()
        {
            new Entity(entity => {
                entity.LayerName = "Background";
                entity.AddComponent<Drawable>().LoadTexture("Test");

                int X = 0;
                CoroutineHelper.Always(() => {
                    X++;
                    entity.GetComponent<Drawable>().SourceRectangle = new Rectangle(X, 0, 1000, 1000);
                });
            });

            new Entity(entity => {
                entity.AddComponent<Drawable>().BuildRectangle(new Rectangle(100, 100, 100, 100), Color.Black);
            });
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
               Global.Camera.Position += new Vector2(-1f, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Global.Camera.Position += new Vector2(1f, 0);
        }
    }
}