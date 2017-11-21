using Microsoft.Xna.Framework;
using MonoXEngine.EntityComponents;
using System.Collections.Generic;

namespace MonoXEngine
{
    /// <summary>
    /// I need to make this move with the gameobject on update, and a method for checking collision
    /// </summary>
    class BoxCollider : EntityComponent
    {
        private Vector2 origin = Vector2.Zero;
        public Vector2 Origin
        {
            get { return this.origin; }
            set { this.origin = new Vector2(MathHelper.Clamp(value.X, 0f, 1f), MathHelper.Clamp(value.Y, 0f, 1f)); }
        }

        public Rectangle Rect;

        public override void Start()
        {

        }

        public override void Update()
        {
            this.Rect = new Rectangle(this.entity.Position.ToPoint(), this.entity.Size.ToPoint());
        }

        public bool CollidingWith(BoxCollider collider)
        {
            this.Update();
           return this.Rect.Intersects(collider.Rect);
        }

        public bool CollidingWith(List<BoxCollider> colliders)
        {
            return this.CollidingWith(colliders.ToArray());
        }

        public bool CollidingWith(BoxCollider[] colliders)
        {
            this.Update();

            foreach (BoxCollider bc in colliders)
                if (this.Rect.Intersects(bc.Rect))
                    return true;

            return false;
        }
    }
}