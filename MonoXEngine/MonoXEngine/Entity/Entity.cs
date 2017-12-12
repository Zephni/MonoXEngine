using MonoXEngine.EntityComponents;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoXEngine
{
    public class Entity : Prefab
    {
        public Action<Entity> UpdateAction;

        public Entity(Action<Entity> action = null) : base(action)
        {
            Global.Entities.Add(this);
            Start();
            InitialiseAction(this);
        }

        public void RunFunction(string alias)
        {
            Functions[alias](this);
        }

        public void Destroy()
        {
            Global.Entities.Remove(this);
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {
            if(UpdateAction != null)
                UpdateAction(this);

            foreach (EntityComponent component in this.EntityComponents)
                component.Update();
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(Drawable drawable in this.EntityComponents.FindAll(
                e => e.GetType().IsSubclassOf(typeof(Drawable)) || e.GetType() == typeof(Drawable)))
            {
                drawable.Draw(spriteBatch);
            }
        }
    }
}