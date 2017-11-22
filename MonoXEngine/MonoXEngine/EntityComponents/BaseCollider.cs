using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoXEngine.EntityComponents
{
    public abstract class BaseCollider : EntityComponent
    {
        public virtual bool CollidingRect(Rectangle rectOffset)
        {
            return false;
        }

        public override void Start()
        {

        }

        public override void Update()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
