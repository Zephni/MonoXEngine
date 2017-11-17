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

        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Scale;
        public Vector2 TextureSize;
        public float Rotation;

        private float opacity = 1;
        public float Opacity
        {
            get { return opacity; }
            set { if (value < 0) value = 0; if (value > 1) value = 1; this.opacity = value; }
        }

        public Vector2 Size { get { return this.TextureSize * this.Scale; } }
        
        private string layerName;
        public string LayerName
        {
            get { return this.layerName; }
            set { this.MoveToLayer(value); }
        }

        public Entity(Action<Entity> action = null)
        {
            this.Position = Vector2.Zero;
            this.Origin = new Vector2(0.5f, 0.5f);
            this.Scale = new Vector2(1, 1);
            this.EntityComponents = new List<EntityComponent>();

            if(action != null)
                action(this);

            if (this.LayerName == null)
                this.LayerName = MonoXEngineGame.Instance.GetSetting<string>("Defaults", "Layer");
        }

        public T AddComponent<T>(Action<T> action = null)
        {
            T newComponent = (T)Activator.CreateInstance(typeof(T));
            this.EntityComponents.Add((EntityComponent)(object)newComponent);
            this.EntityComponents[this.EntityComponents.Count - 1].entity = this;
            T component = (T)Convert.ChangeType(this.EntityComponents[this.EntityComponents.Count - 1], typeof(T));
            if (action != null) action(component);
            return component;
        }

        public T GetComponent<T>()
        {
            foreach(EntityComponent component in this.EntityComponents)
                if (component.GetType() == typeof(T))
                    return (T)Convert.ChangeType(component, typeof(T));

            return default(T);
        }
        
        public virtual void Update()
        {
            foreach (EntityComponent component in this.EntityComponents)
            {
                component.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(Drawable drawable in this.EntityComponents)
            {
                drawable.Draw(spriteBatch);
            }
        }

        public void MoveToLayer(string newLayerName)
        {
            if(this.LayerName != null)
                SpriteBatchLayer.Get(this.LayerName).Entities.Remove(this);
            
            this.layerName = newLayerName;
            SpriteBatchLayer.Get(newLayerName).Entities.Add(this);
        }
    }
}
