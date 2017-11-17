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
                sprite.AddAnimation(new Animation("Test", 0.07f, new Point(48, 60), Global.Str2Points("0,1,2,3,4,5", "0")));
                sprite.RunAnimation("Test");
            });
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}