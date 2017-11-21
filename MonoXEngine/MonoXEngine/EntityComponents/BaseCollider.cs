using Microsoft.Xna.Framework;

namespace MonoXEngine.EntityComponents
{
    public class BaseCollider : EntityComponent
    {
        public override void Start()
        {

        }

        public virtual bool CollidingRect(Rectangle rectOffset)
        {
            return false;
        }
    }
}
