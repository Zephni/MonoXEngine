using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoXEngine
{
    public class Prefab
    {
        public string Name;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Scale;
        public Vector2 TextureSize;
        public float Rotation;
        public string LayerName;
        public bool Trigger = false;
        public int SortingLayer;
        public Action<Entity> CollidedWithTrigger { get; set; }
        public Vector2 Size { get { return this.TextureSize * this.Scale; } }
        public Rectangle BoundingBox
        {
            get { return new Rectangle(this.Position.ToPoint() - (this.Origin * this.Size).ToPoint(), this.Size.ToPoint()); }
        }

        protected List<EntityComponent> EntityComponents;
        protected Dictionary<string, Action<Entity>> Functions = new Dictionary<string, Action<Entity>>();
        protected Action<Entity> InitialiseAction = null;

        private float opacity = 1;
        public float Opacity
        {
            get { return opacity; }
            set { if (value < 0) value = 0; if (value > 1) value = 1; this.opacity = value; }
        }

        public Prefab(Action<Entity> action = null)
        {
            Position = Vector2.Zero;
            Origin = new Vector2(.5f, .5f);
            Scale = new Vector2(1, 1);
            EntityComponents = new List<EntityComponent>();
            LayerName = Global.MainSettings.Get<string>("Defaults", "Layer");
            InitialiseAction = action;
        }

        public EntityComponent AddComponent(EntityComponent entityComponent)
        {
            EntityComponents.Add(entityComponent);
            entityComponent.Initialise(this as Entity);
            return entityComponent;
        }

        public T AddComponent<T>(Action<T> action)
        {
            EntityComponent entityComponent = Activator.CreateInstance<T>() as EntityComponent;
            EntityComponents.Add(entityComponent);
            entityComponent.Initialise(this as Entity);
            T typeComponent = (T)Convert.ChangeType(entityComponent, typeof(T));
            action(typeComponent);
            return typeComponent;
        }

        public T GetComponent<T>()
        {
            foreach (EntityComponent component in this.EntityComponents)
                if (component.GetType() == typeof(T))
                    return (T)Convert.ChangeType(component, typeof(T));

            return default(T);
        }

        public bool HasComponent<T>()
        {
            foreach (EntityComponent component in this.EntityComponents)
                if (component.GetType() == typeof(T))
                    return true;

            return false;
        }

        public void AddFunction(string alias, Action<Entity> action)
        {
            Functions.Add(alias, action);
        }

        public Entity Build()
        {
            // This should really copy EVERYTHING that the prefab has
            return new Entity(InitialiseAction);
        }
    }
}
