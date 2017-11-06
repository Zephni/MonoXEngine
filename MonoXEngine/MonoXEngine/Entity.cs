using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MonoXEngine.EntityComponents;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoXEngine
{
    public class Entity
    {
        private List<EntityComponent> EntityComponents;

        public Entity()
        {
            this.EntityComponents = new List<EntityComponent>();
        }

        public T AddComponent<T>()
        {
            T newComponent = (T)Activator.CreateInstance(Type.GetType("MonoXEngine.EntityComponents.Drawable"));
            this.EntityComponents.Add((EntityComponent)(object)newComponent);
            return (T)Convert.ChangeType(this.EntityComponents[this.EntityComponents.Count - 1], typeof(T));
        }
        
        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(Drawable drawable in this.EntityComponents)
            {
                drawable.Draw(gameTime, spriteBatch);
            }
        }
    }
}
