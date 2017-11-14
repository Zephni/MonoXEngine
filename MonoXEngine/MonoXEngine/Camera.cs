﻿using Microsoft.Xna.Framework;

namespace MonoXEngine
{
    public class Camera
    {
        private static Camera main;
        public static Camera Main
        {
            get
            {
                if(Camera.main == null)
                    Camera.main = new Camera();

                return Camera.main;
            }
        }

        protected float _zoom;
        private Matrix _transform;
        private Vector2 _pos;
        protected float _rotation;

        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Position
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Camera()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }

        public Matrix GetTransformation(Point resolution)
        {
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(resolution.X * 0.5f, resolution.Y * 0.5f, 0));
            return _transform;
        }
    }
}
