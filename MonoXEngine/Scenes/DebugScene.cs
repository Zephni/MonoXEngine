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
                Sprite sprite = entity.AddComponent<Sprite>();
                sprite.LoadTexture("Test");
                sprite.AddAnimation(new Animation("Test", 0.07f, new Point(180, 245), new Point[] {
                    new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0),
                    new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1), new Point(4, 1)
                }));
                sprite.RunAnimation("Test");
            });
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}