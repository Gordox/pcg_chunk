using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCG
{
    class Camera2D
    {

        #region Fields

        protected float _zoom;
        protected Matrix _transform;
        protected Matrix _inverseTransform;
        protected Vector2 _pos;
        protected float _rotation;
        protected Viewport ViewPort;
        protected MouseState MState;
        protected KeyboardState KBS;
        protected Int32 Scroll;

        public float Zoom_Max = 1.0f;
        public float Zoom_Min = 0.1f;


        private float Speed_M = 10.0f;
        private float Speed_R = 0.01f;
        private float Speed_Z = 0.1f;
        #endregion

        #region Properties

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }
        /// <summary>
        /// Camera View Matrix Property
        /// </summary>
        public Matrix Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }
        /// <summary>
        /// Inverse of the view matrix, can be used to get objects screen coordinates
        /// from its object coordinates
        /// </summary>
        public Matrix InverseTransform
        {
            get { return _inverseTransform; }
        }
        public Vector2 Position
        {
            get { return _pos; }
            set { _pos = value; }
        }
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        #endregion

        #region Constructor

        public Camera2D(Viewport viewport)
        {
            _zoom = 1.0f;
            Scroll = 1;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            ViewPort = viewport;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the camera view
        /// </summary>
        public void Update()
        {
            //Call Camera Input
            Input();
            //Clamp zoom value
            _zoom = MathHelper.Clamp(_zoom, Zoom_Min, Zoom_Max);
            //Clamp rotation value
            _rotation = ClampAngle(_rotation);
            //Create view matrix
            _transform = Matrix.CreateRotationZ(_rotation) *
                            Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                            Matrix.CreateTranslation(_pos.X, _pos.Y, 0);
            //Update inverse matrix
            _inverseTransform = Matrix.Invert(_transform);
        }

        /// <summary>
        /// Example Input Method, rotates using cursor keys and zooms using mouse wheel
        /// </summary>
        protected virtual void Input()
        {
            MState = Mouse.GetState();
            KBS = Keyboard.GetState();
            //Check zoom
            if (MState.ScrollWheelValue > Scroll)
            {
                _zoom += Speed_Z;
                Scroll = MState.ScrollWheelValue;
            }
            else if (MState.ScrollWheelValue < Scroll)
            {
                _zoom -= Speed_Z;
                Scroll = MState.ScrollWheelValue;
            }
            //Check rotation
            if (KBS.IsKeyDown(Keys.Q))
            {
                _rotation -= Speed_R;
            }
            if (KBS.IsKeyDown(Keys.E))
            {
                _rotation += Speed_R;
            }
            //Check Move
            if (KBS.IsKeyDown(Keys.A))
            {
                _pos.X += Speed_M;
            }
            if (KBS.IsKeyDown(Keys.D))
            {
                _pos.X -= Speed_M;
            }
            if (KBS.IsKeyDown(Keys.W))
            {
                _pos.Y += Speed_M;
            }
            if (KBS.IsKeyDown(Keys.S))
            {
                _pos.Y -= Speed_M;
            }
        }

        /// <summary>
        /// Clamps a radian value between -pi and pi
        /// </summary>
        /// <param name="radians">angle to be clamped</param>
        /// <returns>clamped angle</returns>
        protected float ClampAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        #endregion
    }
}
