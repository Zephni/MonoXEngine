using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoXEngine.EntityComponents;
using System.Collections.Generic;

namespace MonoXEngine.Scenes
{
    public class DebugScene : Scene
    {
        Entity p;

        public override void Initialise()
        {
            // Build TileMap
            List<Tile> tempTiles = new List<Tile>();
            for (int x = 0; x < 10; x++)
                tempTiles.Add(new Tile(new Point(0, 0), new Point(x, 0)));
            for (int x = 0; x < 10; x++)
                tempTiles.Add(new Tile(new Point(0, 1), new Point(x, 1)));

            TileMap tileMap = new TileMap(new Point(32, 32), "Tileset", tempTiles);
            //tileMap.Build(new Point(32, 32));

            // Build p
            p = new Entity(entity => {
                entity.AddComponent<Sprite>(component => {
                    component.BuildRectangle(new Point(16, 16), Color.Black);
                });
                
                entity.AddComponent<PlatformerController>(component => {
                    component.Collider = entity.AddComponent<TileMapCollider>();
                });
            });
        }

        public override void Update()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
                p.Position = new Vector2(0, -100);
            //Global.Camera.Position += new Vector2(1f, 0);
        }
    }
}