using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoXEngine.EntityComponents
{
    public class Drawable : EntityComponent
    {
        private Texture2D Texture2D;

        public Drawable()
        {

        }

        public void LoadTexture(string file)
        {
            this.Texture2D = MonoXEngineGame.Instance.Content.Load<Texture2D>(file);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(this.Texture2D != null)
            {
                spriteBatch.Draw(
                    this.Texture2D,
                    new Rectangle(this.entity.Position.ToPoint(), new Point(this.Texture2D.Width, this.Texture2D.Height)),
                    this.Texture2D.Bounds,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0
                );
            }
        }
    }
}
