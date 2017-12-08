using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoXEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoXEngine.Structs;

namespace MonoXEngine.EntityComponents
{
    public class DynamicTexture : Drawable
    {
        public Color[,] Data;

        public DynamicTexture(Entity entity, string textureFile)
        {
            this.Entity = entity;
            Data = DynamicTexture.TextureDataTo2DArray(Global.Content.Load<Texture2D>(textureFile));
        }

        public override void Update()
        {
            SetTexture(DynamicTexture.Array2DToTextureData(Data));
        }

        public static Texture2D Array2DToTextureData(Color[,] colors)
        {
            Color[] colors1D = new Color[colors.GetLength(0) * colors.GetLength(1)];
            for(int x = 0; x < colors.GetLength(0); ++x)
              for(int y = 0; y < colors.GetLength(1); ++y)
                  colors1D[x + y * colors.GetLength(0)] = colors[x, y];

            Texture2D texture = new Texture2D(Global.GraphicsDevice, colors.GetLength(0), colors.GetLength(1));
            texture.SetData(colors1D);
            return texture;
        }

        public static Color[,] TextureDataTo2DArray(Texture2D texture)
        {
            Color[] colorsOne = new Color[texture.Width * texture.Height]; //The hard to read,1D array
            texture.GetData(colorsOne); //Get the colors and add them to the array

            Color[,] colorsTwo = new Color[texture.Width, texture.Height]; //The new, easy to read 2D array
            for (int x = 0; x < texture.Width; x++) //Convert!
                for (int y = 0; y < texture.Height; y++)
                    colorsTwo[x, y] = colorsOne[x + y * texture.Width];

            return colorsTwo; //Done!
        }
    }
}
