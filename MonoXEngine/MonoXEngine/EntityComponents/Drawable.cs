using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoXEngine.EntityComponents
{
    public class Drawable : EntityComponent
    {
        protected Texture2D Texture2D;

        public void LoadTexture(string file)
        {
            this.Texture2D = MonoXEngineGame.Instance.Content.Load<Texture2D>(file);
            this.entity.TextureSize = new Vector2(this.Texture2D.Width, this.Texture2D.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.Texture2D != null)
            {
                spriteBatch.Draw(
                    this.Texture2D,
                    new Rectangle(this.entity.Position.ToPoint(), this.entity.Size.ToPoint()),
                    this.Texture2D.Bounds,
                    Color.White * this.entity.Opacity,
                    this.entity.Rotation,
                    this.entity.Position + (this.entity.Size / 2),
                    SpriteEffects.None,
                    0
                );
            }
        }
    }
}
