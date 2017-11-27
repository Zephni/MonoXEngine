using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System.Collections.Generic;
using MonoXEngine.Structs;

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

                CoroutineHelper.WaitRun(1, () => {
                    CoroutineHelper.RunFor(2, p =>
                    {
                        entity.Opacity = 1 - p;
                    }, () =>
                    {
                        entity.Destroy();
                    });
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

            // Player
            player = new Entity(entity => {
                entity.Position = new Vector2(128, 16);

                entity.AddComponent(new Sprite()).Run<Sprite>(component => {
                    component.BuildRectangle(new Point(16, 16), Color.White);
                });

                entity.AddComponent(new PlatformerController(new PixelCollider()));

                entity.CollidedWithTrigger += other => {
                    if (other.Name == "Collectable")
                        other.Destroy();
                };
            });

            // Collectable prefab
            Entity collectable = new Entity(true, entity => {
                entity.Trigger = true;
                entity.Name = "Collectable";
                entity.AddComponent(new Drawable()).Run<Drawable>(component => {
                    component.BuildRectangle(new Point(8, 8), Color.Blue);
                });
            });

            for(int X = 0; X < 5; X++)
            {
                Entity newCollectable = collectable.BuildPrefab();
                newCollectable.Position = new Vector2(150 + (X * 16), 0);
            }

            // Build TileMap
            List<Tile> tempTiles = new List<Tile>();
            tempTiles.Add(new Tile(new Point(0, 0), new Point3D(5, 0, 2)));
            for (int x = 0; x < 15; x++)
                tempTiles.Add(new Tile(new Point(0, 0), new Point3D(x, 1, 0)));
            for (int x = 0; x < 19; x++)
                for (int y = 0; y < 5; y++)
                    tempTiles.Add(new Tile(new Point(0, 1), new Point3D(x, y+2, 0)));

            TileMap tileMap = new TileMap(new Point(32, 32), "Tileset", tempTiles);
            tileMap.Build(new Point(10, 10));

            // Debug
            new Entity(entity => {
                entity.SortingLayer = 1;
                entity.LayerName = "UI";
                entity.Position = -(Global.Resolution.ToVector2() / 2);
                entity.AddComponent(new Text()).Run<Text>(component => {
                    component.SetSpriteFont("Retro1-12");
                    component.Color = Color.Yellow;

                    entity.UpdateAction = e => {
                        component.String = player.Position.X + ", " + player.Position.Y;
                    };
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