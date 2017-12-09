using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System.Collections.Generic;
using MonoXEngine.Structs;
using Microsoft.Xna.Framework.Graphics;

namespace MonoXEngine.Scenes
{
    public class DebugScene : Scene
    {
        Entity player;

        public override void Initialise()
        {
            new Entity(entity => {
                entity.LayerName = "Background";
                entity.Position += new Vector2(0, -80);
                entity.AddComponent(new Drawable(){
                    Texture2D = Global.Content.Load<Texture2D>("SkyTest"),
                });
            });

            new Entity(entity => {
                entity.LayerName = "Background";
                entity.Position += new Vector2(0, -50);
                entity.AddComponent(new CameraOffsetTexture(){
                    Texture2D = Global.Content.Load<Texture2D>("MountainsTest"),
                    Coefficient = new Vector2(0.05f, 0)
                });
            });

            new Entity(entity => {
                entity.LayerName = "Background";
                entity.Position += new Vector2(0, 0);
                entity.AddComponent(new CameraOffsetTexture(){
                    Texture2D = Global.Content.Load<Texture2D>("BGTest"),
                    Coefficient = new Vector2(0.05f, 0)
                });
            });

            new Entity(entity =>{
                entity.LayerName = "Background";
                entity.Position += new Vector2(0, 80);
                entity.AddComponent(new Drawable() { Texture2D = Global.Content.Load<Texture2D>("WaterTest") }).Run<Drawable>(component => {
                    Color[,] colorArr = component.Texture2D.To2DArray();
                    CoroutineHelper.Always(() => {
                        component.Texture2D.ManipulatePixels(colors => {
                            colors = colorArr.Copy2D(new Rectangle(0, 0, colorArr.GetLength(0), colorArr.GetLength(1)));
                            for (int y = 0; y < 100; y++)
                                colors.Shift(new Rectangle(0, 1 * y, 256, 1), new Point(-(int)(Global.Camera.Position.X * (y+1) * 0.05f), 1 * y));
                            return colors;
                        });
                    });
                });
            });

            /*
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
                    component.BuildRectangle(new Point(8, 8), Color.Yellow);
                });
            });

            for(int X = 0; X < 5; X++)
            {
                Entity newCollectable = collectable.BuildPrefab();
                newCollectable.Position = new Vector2(150 + (X * 16), 32*4.72f);
            }

            // Build TileMap
            List<Tile> tempTiles = new List<Tile>();
            for (int X = 0; X < 12; X++) tempTiles.Add(new Tile(new Point(0, 0), new Point3D(X, 5, 0)));
            for (int X = 0; X < 12; X++) for (int Y = 1; Y < 4; Y++) tempTiles.Add(new Tile(new Point(0, 1), new Point3D(X, Y+5, 0)));
            
            TileMap tileMap = new TileMap(new Point(32, 32), "Tileset", tempTiles);
            tileMap.Build(new Point(30, 30));

            
            */
            // Debug
            new Entity(entity => {
                entity.SortingLayer = 1;
                entity.LayerName = "UI";
                entity.Position = -(Global.Resolution.ToVector2() / 2);
                entity.AddComponent(new Text()).Run<Text>(component => {
                    component.SetSpriteFont("Retro1-12");
                    component.Color = Color.Yellow;

                    entity.UpdateAction = e => {
                        component.String = Global.CountEntities().ToString();
                        component.String += (Global.GameTime.IsRunningSlowly) ? " | Running slow" : "";
                    };
                });
            });
        }
        
        public override void Update()
        {
            //Vector2 camPos = player.Position + new Vector2(0, -30);
            Global.Camera.Position += new Vector2(1f, 0);
        }
    }
}