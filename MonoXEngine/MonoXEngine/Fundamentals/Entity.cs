﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MonoXEngine.EntityComponents;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoXEngine
{
    public class Entity
    {
        private List<EntityComponent> EntityComponents;

        public string Name;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Scale;
        public Vector2 TextureSize;
        public float Rotation;

        public bool Trigger = false;
        public Action<Entity> CollidedWithTrigger;

        private int sortingLayer;
        public int SortingLayer
        {
            get { return this.sortingLayer; }
            set { this.sortingLayer = value; Global.SpriteBatchLayers[this.LayerName].SortEntities(); }
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle(this.Position.ToPoint() + (this.Origin * this.Size).ToPoint(), this.Size.ToPoint()); }
        }

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

            if (this.LayerName == null)
                this.LayerName = MonoXEngineGame.Instance.MainSettings.Get<string>(new string[] { "Defaults", "Layer" });

            if(action != null)
                action.Invoke(this);

            this.Start();
        }

        private Action<Entity> prefabAction = null;
        public Entity(bool prefab, Action<Entity> action = null)
        {
            this.Position = Vector2.Zero;
            this.Origin = new Vector2(0.5f, 0.5f);
            this.Scale = new Vector2(1, 1);
            this.EntityComponents = new List<EntityComponent>();

            if (!prefab)
            {
                this.LayerName = MonoXEngineGame.Instance.MainSettings.Get<string>(new string[] { "Defaults", "Layer" });
                if(action != null)
                    action.Invoke(this);
                this.Start();
            }
            else
            {
                prefabAction = action;
            }
        }

        public Entity BuildPrefab(string layerName = null)
        {
            if (layerName == null)
                this.LayerName = MonoXEngineGame.Instance.MainSettings.Get<string>(new string[] { "Defaults", "Layer" });
            else
                this.LayerName = layerName;

            if (this.prefabAction != null)
                this.prefabAction(this);

            this.Start();
            return this;
        }

        public virtual void Start() { }

        public EntityComponent AddComponent(EntityComponent entityComponent)
        {
            this.EntityComponents.Add(entityComponent);
            entityComponent.Initialise(this);
            return entityComponent;
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

        public void Destroy()
        {
            SpriteBatchLayer.Get(this.LayerName).Entities.Remove(this);
        }

        public Action<Entity> UpdateAction;
        public virtual void Update()
        {
            if(this.UpdateAction != null)
                this.UpdateAction(this);

            foreach (EntityComponent component in this.EntityComponents)
            {
                component.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(Drawable drawable in this.EntityComponents.FindAll(
                e => e.GetType().IsSubclassOf(typeof(Drawable)) || e.GetType() == typeof(Drawable)))
            {
                drawable.Draw(spriteBatch);
            }
        }

        private void MoveToLayer(string newLayerName)
        {
            if(this.LayerName != null)
                SpriteBatchLayer.Get(this.LayerName).Entities.Remove(this);
            
            this.layerName = newLayerName;
            SpriteBatchLayer.Get(newLayerName).Entities.Add(this);
        }
    }
}