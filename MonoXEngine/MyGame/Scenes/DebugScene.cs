using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System.Collections.Generic;
using MonoXEngine.Structs;
using Microsoft.Xna.Framework.Graphics;
using MonoXEngine;
using StaticCoroutines;

namespace MyGame.Scenes
{
    public class DebugScene : Scene
    {
        public override void Initialise()
        {
            Entity fader = new Entity(entity => {
                entity.LayerName = "Fade";
                entity.AddComponent(new Drawable()).Run<Drawable>(component => {
                    component.BuildRectangle(new Point(Global.ScreenBounds.Width, Global.ScreenBounds.Height), Color.Black);
                });

                entity.AddFunction("FadeIn", e => {
                    CoroutineHelper.RunFor(2, pcnt => { e.Opacity = 1 - pcnt; });
                });

                entity.AddFunction("FadeOut", e => {
                    CoroutineHelper.RunFor(2, pcnt => { e.Opacity = pcnt; }, () => {
                        Global.SceneManager.LoadScene("DebugScene");
                    });
                });
            });

            fader.RunFunction("FadeIn");
            
            // Debug
            new Entity(entity => {
                entity.SortingLayer = 1;
                entity.LayerName = "Fade";
                entity.Position = -(Global.Resolution.ToVector2() / 2);
                entity.AddComponent(new Text()).Run<Text>(component => {
                    component.Color = Color.Yellow;

                    entity.UpdateAction = e => {
                        component.String = Global.FPS.ToString();
                    };
                });
            });
        }
        
        public override void Update()
        {
            if(Global.RunOnce("Restart", Keyboard.GetState().IsKeyDown(Keys.Space)))
            {
                Global.SceneManager.LoadScene("DebugScene");
            }
        }
    }
}