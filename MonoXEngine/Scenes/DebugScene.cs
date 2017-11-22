﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System.Collections.Generic;

namespace MonoXEngine.Scenes
{
    public class DebugScene : Scene
    {
        Entity player;

        public override void Initialise()
        {
            new Entity(entity => {
                entity.LayerName = "Fade";
                entity.AddComponent(new Drawable()).Run<Drawable>(component => {
                    component.BuildRectangle(new Point(256, 240), Color.Black);
                });

                CoroutineHelper.RunFor(2, p => {
                    entity.Opacity = 1 - p;
                }, () => {
                    entity.Destroy();
                });
            });

            new Entity(entity => {
                entity.LayerName = "Background";
                entity.AddComponent(new CameraOffsetTexture { Coefficient = new Vector2(0.3f, 0) }).Run<CameraOffsetTexture>(component => {
                    component.LoadTexture("StarBackground");
                });
            });

            new Entity(entity => {
                entity.LayerName = "Background";
                entity.AddComponent(new CameraOffsetTexture { Coefficient = new Vector2(0.6f, 0) }).Run<CameraOffsetTexture>(component => {
                    component.LoadTexture("Buildings");
                });
            });

            // Build TileMap
            List<Tile> tempTiles = new List<Tile>();
            tempTiles.Add(new Tile(new Point(0, 0), new Point(5, 0)));
            for (int x = 0; x < 15; x++)
                tempTiles.Add(new Tile(new Point(0, 0), new Point(x, 1)));
            for (int x = 0; x < 19; x++)
                for (int y = 0; y < 5; y++)
                    tempTiles.Add(new Tile(new Point(0, 1), new Point(x, y+2)));

            TileMap tileMap = new TileMap(new Point(32, 32), "Tileset", tempTiles);
            tileMap.Build(new Point(10, 10));

            // Build player
            player = new Entity(entity => {
                entity.Position = new Vector2(128, 16);

                entity.AddComponent(new Sprite()).Run<Sprite>(component => {
                    component.BuildRectangle(new Point(16, 16), Color.White);
                });

                entity.AddComponent(new PlatformerController(tileMap) {
                    JumpStrength = 5
                });
            });

            // Debug
            new Entity(entity => {
                entity.LayerName = "UI";
                entity.Position = -(Global.Resolution.ToVector2() / 2);
                entity.AddComponent(new Text()).Run<Text>(component => {
                    component.SetSpriteFont("Retro1-12");
                    component.String = "Debug string";
                    component.Color = Color.Yellow;
                });
            });
        }
        
        public override void Update()
        {
            Vector2 newPos = new Vector2(player.Position.X, -20);
            Global.Camera.Position = newPos;
        }
    }
}