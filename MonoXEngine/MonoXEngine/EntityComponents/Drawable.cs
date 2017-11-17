using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoXEngine.EntityComponents
{
    public class Drawable : EntityComponent
    {
        protected Texture2D Texture2D;

        private Rectangle sourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return this.sourceRectangle; }
            set
            {
                this.sourceRectangle = value;
                this.entity.TextureSize = this.sourceRectangle.Size.ToVector2();
            }
        }

        public void MoveSourceRectangle(Rectangle moveRect)
        {
            this.SourceRectangle = new Rectangle(
                this.sourceRectangle.X + moveRect.X,
                this.sourceRectangle.Y + moveRect.Y,
                this.sourceRectangle.Width + moveRect.Width,
                this.sourceRectangle.Height + moveRect.Height
            );
        }

        public void LoadTexture(string file)
        {
            this.Texture2D = MonoXEngineGame.Instance.Content.Load<Texture2D>(file);
            this.entity.TextureSize = new Vector2(this.Texture2D.Width, this.Texture2D.Height);
            this.SourceRectangle = this.Texture2D.Bounds;
        }

        public void LoadTexture(Texture2D texture)
        {
            this.Texture2D = texture;
            this.entity.TextureSize = new Vector2(this.Texture2D.Width, this.Texture2D.Height);
            this.SourceRectangle = this.Texture2D.Bounds;
        }

        public void BuildRectangle(Rectangle rectangle, Color color)
        {
            this.Texture2D = new Texture2D(MonoXEngineGame.Instance.GraphicsDevice, rectangle.Width, rectangle.Height, false, SurfaceFormat.Color);

            Color[] colors = new Color[rectangle.Width * rectangle.Height];
            for (int I = 0; I < rectangle.Width * rectangle.Height; I++)
                colors[I] = color;

            this.Texture2D.SetData<Color>(colors);

            this.entity.TextureSize = new Vector2(this.Texture2D.Width, this.Texture2D.Height);
            this.SourceRectangle = this.Texture2D.Bounds;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.Texture2D != null)
            {
                spriteBatch.Draw(
                    this.Texture2D,
                    new Rectangle(this.entity.Position.ToPoint(), this.entity.Size.ToPoint()),
                    this.SourceRectangle,
                    Color.White * this.entity.Opacity,
                    this.entity.Rotation,
                    this.entity.Origin * this.entity.Size,
                    SpriteEffects.None,
                    0
                );
            }
        }
    }
}
