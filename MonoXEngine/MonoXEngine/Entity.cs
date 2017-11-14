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

        public T AddComponent<T>()
        {
            T newComponent = (T)Activator.CreateInstance(typeof(T));
            this.EntityComponents.Add((EntityComponent)(object)newComponent);
            this.EntityComponents[this.EntityComponents.Count-1].entity = this;
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

        public void MoveToLayer(string newLayerName)
        {
            if(this.LayerName != null)
                SpriteBatchLayer.Get(this.LayerName).Entities.Remove(this);
            
            this.layerName = newLayerName;
            SpriteBatchLayer.Get(newLayerName).Entities.Add(this);
        }
    }
}
