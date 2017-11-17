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
                entity.AddComponent<CameraWrapperSprite>(component => {
                    component.LoadTexture("StarBackground");
                    component.Coefficient = new Vector2(0.3f, 1);
                });
            });

            new Entity(entity => {
                entity.LayerName = "Background";
                entity.AddComponent<CameraWrapperSprite>(component => {
                    component.LoadTexture("Buildings");
                    component.Coefficient = new Vector2(0.6f, 1);
                });
            });
        }

        public override void Update()
        {
            Global.Camera.Position += new Vector2(1f, 0);
        }
    }
}