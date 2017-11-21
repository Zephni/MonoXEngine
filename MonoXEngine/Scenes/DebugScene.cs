using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoXEngine.Scenes
{
    public class DebugScene : Scene
    {
        public override void Initialise()
        {
            // Build TileMap
            List<Tile> tempTiles = new List<Tile>();
            for (int x = 0; x < 10; x++)
                tempTiles.Add(new Tile(new Point(0, 0), new Point(x, 0)));
            for (int x = 0; x < 10; x++)
                tempTiles.Add(new Tile(new Point(0, 1), new Point(x, 1)));

            TileMap tileMap = new TileMap(new Point(32, 32), "Tileset", tempTiles);
            tileMap.Build(new Point(32, 32));

            // Build p
            new Entities.Platformer();
        }

        public override void Update()
        {
            //Global.Camera.Position += new Vector2(1f, 0);
        }
    }
}