﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoXEngine
{
    public class Scene
    {
        public bool Active = false;

        public Scene()
        {
            this.Initialise();
        }

        public virtual void Initialise() { }

        public virtual void Update() { }

        public void Destroy()
        {
            foreach(KeyValuePair<string, SpriteBatchLayer> sbl in Global.SpriteBatchLayers)
            {
                sbl.Value.Entities.Clear();
            }
        }
    }
}