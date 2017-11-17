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
                entity.AddComponent<CameraOffsetTexture>(component => {
                    component.LoadTexture("StarBackground");
                    component.Coefficient = new Vector2(0.3f, 1);
                });
            });

            new Entity(entity => {
                entity.LayerName = "Background";
                entity.AddComponent<CameraOffsetTexture>(component => {
                    component.LoadTexture("Buildings");
                    component.Coefficient = new Vector2(0.6f, 1);
                });
            });

            Entity blueBox = new Entity(entity => {
                entity.Position = new Vector2(64, 0);
                entity.AddComponent<Sprite>().BuildRectangle(new Point(32, 32), Color.Blue);
                entity.AddComponent<BoxCollider>();
            });

                new Entity(entity => {
                entity.Position = new Vector2(-32, 0);
                entity.AddComponent<Sprite>().BuildRectangle(new Point(32, 32), Color.White);
                BoxCollider bc = entity.AddComponent<BoxCollider>();

                CoroutineHelper.Always(() => {
                    if (!bc.CollidingWith(blueBox.GetComponent<BoxCollider>()))
                        entity.Position.X += 1;
                });
            });
        }

        public override void Update()
        {
            //Global.Camera.Position += new Vector2(1f, 0);
        }
    }
}