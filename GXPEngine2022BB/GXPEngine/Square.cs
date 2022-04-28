using System;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Scenes;
using TiledMapParser;

namespace GXPEngine
{
    public class Square : AnimationSprite
    {
        public Vector2 position
        {
            get
            {
                return _position;
            }
        }
        float timePassed = 0;

        Vector2 _position;
        Vector2 _velocity;
        Vector2 _acceleration;

        Vector2 _mouseStartPosition;
        Vector2 _mouseEndPosition;

        bool isMoving
        {
            get
            {
                return _velocity != new Vector2();
            }
        }
        bool charging = false;
        float maxSpeed = 3;

        public Square(string fileName, int rows, int cols, TiledObject obj = null) : base(obj.GetStringProperty("fileName"), 1, 1)
        {
            SetOrigin(width / 2, height / 2);
            Initialize(obj);
        }

        public Square(Vector2 pPosition, Vector2 pVelocity) : base("square.png", 1, 1)
        {
            timePassed = 0;
            _position = pPosition;
            _velocity = new Vector2();
            _acceleration = new Vector2(0f, .03f);
            charging = false;
        }
        void Initialize(TiledObject obj)
        {
            timePassed = 0;
            _position = new Vector2(obj.X, obj.Y);
            _acceleration = new Vector2(0.025f, .03f);
        }
        void UpdateScreenPosition()
        {
            x = _position.x;
            y = _position.y;
        }

        void Update()
        {

			if (Input.GetKeyDown(Key.R))
			{
                _position.SetXY(200, 100);
                _velocity = new Vector2();
			}
            if (Input.GetMouseButtonDown(0) && (Input.mouseX > x-width/2 && Input.mouseX < x + width/2) && (Input.mouseY > y - height/2 && Input.mouseY < y + height/2) && !isMoving)
            {
                _mouseStartPosition = new Vector2(Input.mouseX, Input.mouseY);
                charging = true;
            }
            if (Input.GetMouseButtonUp(0) && charging)
            {
                _mouseEndPosition = new Vector2(Input.mouseX, Input.mouseY);
                Vector2 diffVec = _mouseStartPosition - _mouseEndPosition;
                Vector2 speed = Vector2.GetUnitVectorDeg(diffVec.GetAngleDegrees()) * (Mathf.Clamp(diffVec.Length(), 0, maxSpeed));
                _velocity = speed;
                charging = false;
            }

            if (isMoving)
            {
                timePassed = (float)Time.deltaTime / 1000f;
                //TEMPORARY CODE PLEASE REMOVE AND CHANGE WITH GETACTIVE LEVEL AS SOON AS POSSIBLE BUT FOR TESTING SCENE THIS IS FINE
                Level currentScene = SceneManager.instance.GetActiveLevel();
                for (int i = 0; i < currentScene.GetNumberOfAppliers(); i++)
                {
                    ForceApplier applier = currentScene.GetForceApplier(i);
                    if (applier.IsInHorizontalReach(this, width, height) || applier.IsInVerticalReach(this, width, height))
                    {
                        _velocity += applier.force;
                    }
                }
                //_acceleration += new Vector2(0, _acceleration.y / 2);
                _velocity += _acceleration;
                _position += _velocity;
            }
            UpdateScreenPosition();
        }
    }
}