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
                Text text = entity.AddComponent<Text>();
                string Str = "This is an eventual string";

                CoroutineHelper.Every(0.1f, () => {
                    if (text.text != Str)
                        text.text = Str.Substring(0, text.text.Length+1);
                });
            });
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}