using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoXEngine.EntityComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine
{
    public class TileMap
    {
        Texture2D[,] Tileset;
        List<Tile> Tiles;
        Point TileSize;

        public TileMap(Point tileSize)
        {
            this.Tiles = new List<Tile>();
            this.TileSize = tileSize;
        }

        public void LoadTileset(string tilesetFile)
        {
            Texture2D texture2D = Global.Content.Load<Texture2D>(tilesetFile);

            int xCount, yCount;
            this.Tileset = this.Split(texture2D, this.TileSize.X, this.TileSize.Y, out xCount, out yCount);
        }

        public void LoadTiles(List<Tile> tiles)
        {
            this.Tiles = tiles;
        }

        public void BuildEntities()
        {
            foreach (Tile tile in this.Tiles)
            {
                new Entity(entity =>
                {
                    entity.Position = tile.Position.ToVector2();
                    entity.AddComponent<Drawable>(component => {
                        component.SetTexture(this.Tileset[tile.SourcePosition.X, tile.SourcePosition.Y]);
                    });
                    entity.Position = new Vector2(this.TileSize.X * tile.Position.X, this.TileSize.Y * tile.Position.Y);
                });
            }
        }

        /// <summary>
        /// Splits a texture into an array of smaller textures of the specified size.
        /// </summary>
        /// <param name="original">The texture to be split into smaller textures</param>
        /// <param name="partWidth">The width of each of the smaller textures that will be contained in the returned array.</param>
        /// <param name="partHeight">The height of each of the smaller textures that will be contained in the returned array.</param>
        private Texture2D[,] Split(Texture2D original, int partWidth, int partHeight, out int xCount, out int yCount)
        {
            yCount = original.Height / partHeight + (partHeight % original.Height == 0 ? 0 : 1);//The number of textures in each horizontal row
            xCount = original.Height / partHeight + (partHeight % original.Height == 0 ? 0 : 1);//The number of textures in each vertical column
            Texture2D[,] r = new Texture2D[xCount, yCount];//Number of parts = (area of original) / (area of each part).
            int dataPerPart = partWidth * partHeight;//Number of pixels in each of the split parts

            //Get the pixel data from the original texture:
            Color[] originalData = new Color[original.Width * original.Height];
            original.GetData<Color>(originalData);

            for (int y = 0; y < yCount * partHeight; y += partHeight)
                for (int x = 0; x < xCount * partWidth; x += partWidth)
                {
                    //The texture at coordinate {x, y} from the top-left of the original texture
                    Texture2D part = new Texture2D(original.GraphicsDevice, partWidth, partHeight);
                    //The data for part
                    Color[] partData = new Color[dataPerPart];

                    //Fill the part data with colors from the original texture
                    for (int py = 0; py < partHeight; py++)
                        for (int px = 0; px < partWidth; px++)
                        {
                            int partIndex = px + py * partWidth;
                            //If a part goes outside of the source texture, then fill the overlapping part with Color.Transparent
                            if (y + py >= original.Height || x + px >= original.Width)
                                partData[partIndex] = Color.Transparent;
                            else
                                partData[partIndex] = originalData[(x + px) + (y + py) * original.Width];
                        }

                    //Fill the part with the extracted data
                    part.SetData<Color>(partData);
                    //Stick the part in the return array:                    
                    r[x / partWidth, y / partHeight] = part;
                }
            //Return the array of parts.
            return r;
        }
    }
}
