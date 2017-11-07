using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine
{
    public class RenderViewportTexture
    {
        public RenderTarget2D RenderTarget;

        public RenderViewportTexture(GraphicsDevice graphicsDevice)
        {
            this.RenderTarget = new RenderTarget2D(
               graphicsDevice,
               256,
               248,
               false,
               graphicsDevice.PresentationParameters.BackBufferFormat,
               DepthFormat.Depth24
            );
        }

        private void BeginCapture(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(this.RenderTarget);
            graphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
        }

        private void EndCapture(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(null);
        }

        private void RenderToViewport(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
               SamplerState.LinearClamp, DepthStencilState.Default,
               RasterizerState.CullNone);

            spriteBatch.Draw(this.RenderTarget, new Rectangle(0, 0, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight), Color.Red);

            spriteBatch.End();
        }

        public void CaptureAndRender(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Action drawCalls)
        {
            this.BeginCapture(graphicsDevice);
            drawCalls();
            this.EndCapture(graphicsDevice);
            this.RenderToViewport(graphicsDevice, spriteBatch);
        }
    }
}
