using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoXEngine.EntityComponents
{
    ///
    /// To add a new font, open Content.mgcb and add new item "Font Description" in the "Fonts" directory, then build
    ///

    /// <summary>
    /// Text component, set the SpriteFont to a .xnb loaded content. And set the text.
    /// </summary>
    class Text : Drawable
    {
        public string _text = "";
        public string text
        {
            get { return _text; }
            set { _text = value; this.entity.TextureSize = this.SpriteFont.MeasureString(_text); }
        }

        public Color Color = Color.Black;
        public float Opacity = 1;
        public SpriteFont SpriteFont = null;

        public Text SetSpriteFont(string FontAlias = "Arial-12")
        {
            this.SpriteFont = Global.Content.Load<SpriteFont>("Fonts/"+FontAlias);
            return this;
        }

        public override void Start()
        {
            this.SetSpriteFont();
            this.entity.TextureSize = this.SpriteFont.MeasureString(_text);
            this.entity.Origin = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (this.SpriteFont != null)
            {
                spriteBatch.DrawString(
                    this.SpriteFont,
                    this.text,
                    this.entity.Position,
                    this.Color,
                    this.entity.Rotation,
                    this.entity.Origin * this.entity.Size,
                    this.entity.Scale,
                    SpriteEffects.None,
                    0
                );
            }
        }
    }
}
