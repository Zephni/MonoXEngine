using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoXEngine.EntityComponents;
using System.Diagnostics;

namespace MonoXEngine
{
    public class TileMap
    {
        private Texture2D[,] tileset;
        private Point tileSize;
        private List<Tile> tiles;
        private Texture2D[,] chunks;
        private Point totalMapSize;
        private Point totalChunks;
        private Point chunkSize;

        /// <summary>
        /// TileMap construct
        /// </summary>
        /// <param name="_tileSize"></param>
        /// <param name="tilesetName"></param>
        /// <param name="tiles"></param>
        public TileMap(Point _tileSize, string tilesetName, List<Tile> tiles)
        {
            this.tileSize = _tileSize;
            this.LoadTileset(tilesetName);
            this.SetTiles(tiles);
        }

        /// <summary>
        /// Loads a tileset and splits it into individual tiles, stores in this.tileset
        /// </summary>
        /// <param name="fileName">File name of texture to load</param>
        protected void LoadTileset(string fileName)
        {
            Texture2D texture2D = Global.Content.Load<Texture2D>(fileName);
            this.tileset = this.Split(texture2D, this.tileSize.X, this.tileSize.Y);
        }

        /// <summary>
        /// Set list of tiles
        /// </summary>
        /// <param name="_tiles"></param>
        protected void SetTiles(List<Tile> _tiles)
        {
            this.tiles = _tiles;
        }

        protected Point TotalMapSize()
        {
            int minX = this.tiles.Min(tile => tile.Position.X);
            int minY = this.tiles.Min(tile => tile.Position.Y);
            int maxX = this.tiles.Max(tile => tile.Position.X);
            int maxY = this.tiles.Max(tile => tile.Position.Y);

            return new Point(
                (1+maxX - minX) * this.tileSize.X,
                (1+maxY - minY) * this.tileSize.Y
            );
        }

        /// <summary>
        /// Splits a texture into an array of smaller textures of the specified size.
        /// </summary>
        /// <param name="original">The texture to be split into smaller textures</param>
        /// <param name="partWidth">The width of each of the smaller textures that will be contained in the returned array.</param>
        /// <param name="partHeight">The height of each of the smaller textures that will be contained in the returned array.</param>
        protected Texture2D[,] Split(Texture2D original, int partWidth, int partHeight)
        {
            int yCount = original.Height / partHeight + (partHeight % original.Height == 0 ? 0 : 1);//The number of textures in each horizontal row
            int xCount = original.Height / partHeight + (partHeight % original.Height == 0 ? 0 : 1);//The number of textures in each vertical column
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

        /// <summary>
        /// Builds entities based on tilemap, and tiles provided and splits them up into the chunks passed
        /// </summary>
        /// <param name="perChunkTileAmount">Number of tiles per chunk</param>
        public void Build(Point perChunkTileAmount)
        {
            totalMapSize = this.TotalMapSize();
            totalChunks = new Point(
                Math.Max((int)Math.Ceiling((double)totalMapSize.X / (perChunkTileAmount.X * this.tileSize.X)), 1),
                Math.Max((int)Math.Ceiling((double)totalMapSize.Y / (perChunkTileAmount.Y * this.tileSize.Y)), 1)
            );
            chunkSize = perChunkTileAmount * this.tileSize;
            chunks = new Texture2D[totalChunks.X, totalChunks.Y];            

            foreach (Tile tile in this.tiles)
            {
                Point chunkIndex = (tile.Position * tileSize) / chunkSize;
                Rectangle chunkRect = new Rectangle(chunkIndex * chunkSize, chunkSize);
                Color[] tileColors = new Color[this.tileSize.X * this.tileSize.Y];
                this.tileset[tile.SourcePosition.X, tile.SourcePosition.Y].GetData<Color>(tileColors, 0, tileColors.Length);

                if (chunks[chunkIndex.X, chunkIndex.Y] == null)
                    chunks[chunkIndex.X, chunkIndex.Y] = new Texture2D(Global.GraphicsDevice, chunkSize.X, chunkSize.Y);

                Rectangle relativeTileRect = new Rectangle((tile.Position * this.tileSize) - chunkRect.Location, this.tileSize);
                chunks[chunkIndex.X, chunkIndex.Y].SetData<Color>(0, relativeTileRect, tileColors, 0, this.tileSize.X * this.tileSize.Y);
            }

            for (int x = 0; x < totalChunks.X; x++)
            {
                for (int y = 0; y < totalChunks.Y; y++)
                {
                    new Entity(entity => {
                        entity.Origin = Vector2.Zero;
                        entity.Position = new Vector2(x * chunkSize.X, y * chunkSize.Y);
                        entity.AddComponent(new Drawable()).Run<Drawable>(component => {
                            component.SetTexture(chunks[x, y]);
                        });
                    });
                }
            }
        }

        public bool IsRectOverlappingPixels(Rectangle rect)
        {
            for (int x = 0; x < totalChunks.X; x++)
            {
                for (int y = 0; y < totalChunks.Y; y++)
                {
                    Rectangle chunkRect = new Rectangle(new Point(x * chunkSize.X, y * chunkSize.Y), chunkSize);
                    Rectangle intersectRect;
                    Rectangle.Intersect(ref rect, ref chunkRect, out intersectRect);
                    
                    if (intersectRect.Width > 0 && intersectRect.Height > 0)
                    {
                        intersectRect.Location -= chunkRect.Location;
                        int totalPixels = intersectRect.Width * intersectRect.Height;
                        Color[] colors = new Color[totalPixels];
                        chunks[x, y].GetData(0, intersectRect, colors, 0, totalPixels);

                        for (int I = 0; I < colors.Length; I++)
                            if (colors[I].A != byte.MinValue)
                                return true;
                    }
                }
            }

            return false;
        }
    }
}
