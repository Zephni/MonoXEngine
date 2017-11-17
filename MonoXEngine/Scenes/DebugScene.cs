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
            // Background
            new Entity(entity => {
                entity.LayerName = "Background";
                entity.AddComponent<CameraOffsetTexture>(component => {
                    component.LoadTexture("StarBackground");
                    component.Coefficient.X = 0.3f;
                });
            });

            new Entity(entity => {
                entity.LayerName = "Background";
                entity.AddComponent<CameraOffsetTexture>(component => {
                    component.LoadTexture("Buildings");
                    component.Coefficient.X = 0.5f;
                });
            });

            // Build TileMap
            TileMap tileMap = new TileMap(new Point(32, 32));
            tileMap.LoadTileset("Tileset");

            List<Tile> tempTiles = new List<Tile>();
            for (int X = 0; X < 32; X++)
                tempTiles.Add(new Tile(new Point(0, 0), new Point(X, 3)));
            for (int X = 0; X < 32; X++)
                for (int Y = 0; Y < 3; Y++)
                    tempTiles.Add(new Tile(new Point(0, 1), new Point(X, Y+4)));

            tileMap.LoadTiles(tempTiles);
            tileMap.BuildEntities();
        }

        public override void Update()
        {
            Global.Camera.Position += new Vector2(1f, 0);
        }
    }
}