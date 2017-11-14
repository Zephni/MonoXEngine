using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoXEngine.EntityComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoXEngine
{
    public class DrawableRectangle : Drawable
    {
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return this.rectangle; }
            set { this.rectangle = value; this.BuildTexture(); }
        }

        private Color color;
        public Color Color
        {
            get { return this.color; }
            set { this.color = value; this.BuildTexture(); }
        }

        public DrawableRectangle Initialise(Rectangle _rectangle, Color _color)
        {
            this.rectangle = _rectangle;
            this.color = _color;
            this.BuildTexture();
            return this;
        }

        private void BuildTexture()
        {
            this.Texture2D = new Texture2D(MonoXEngineGame.Instance.GraphicsDevice, this.rectangle.Width, this.rectangle.Height, false, SurfaceFormat.Color);

            Color[] colors = new Color[this.rectangle.Width * this.rectangle.Height];
            for (int I = 0; I < this.rectangle.Width * this.rectangle.Height; I++ )
                colors[I] = this.Color;

            this.Texture2D.SetData<Color>(colors);

            this.entity.TextureSize = new Vector2(this.Texture2D.Width, this.Texture2D.Height);
            this.SourceRectangle = this.Texture2D.Bounds;
        }
    }
}
