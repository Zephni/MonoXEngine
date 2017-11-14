using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine
{
    public class SpriteBatchLayer
    {
        private SpriteBatch SpriteBatch;

        public List<Entity> Entities;

        public static SpriteBatchLayer Get(string Key)
        {
            return MonoXEngineGame.Instance.SpriteBatchLayers[Key];
        }

        public SpriteBatchLayer(GraphicsDevice graphicsDevice)
        {
            this.SpriteBatch = new SpriteBatch(graphicsDevice);
            this.Entities = new List<Entity>();
        }

        public void Draw(GameTime gameTime, Point resolution)
        {
            this.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                null,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Camera.Main.GetTransformation(resolution)
            );

            foreach (Entity entity in Entities)
                entity.Draw(gameTime, this.SpriteBatch);

            this.SpriteBatch.End();
        }
    }
}
