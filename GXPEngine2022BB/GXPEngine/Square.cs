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


        Vector2 _position;
        Vector2 _velocity;
        Vector2 _acceleration;

        Vector2 _mouseStartPosition;
        Vector2 _mouseEndPosition;
        EasyDraw _easyDraw;
        bool isMoving
        {
            get
            {
                return _velocity != new Vector2();
            }
        }
        bool charging = false;
        float maxSpeed = 5;

        public Square(string fileName, int rows, int cols, TiledObject obj = null) : base(obj.GetStringProperty("fileName"), 1, 1)
        {
            SetOrigin(width / 2, height / 2);
            Initialize(obj);
        }

        public Square(Vector2 pPosition, Vector2 pVelocity) : base("square.png", 1, 1)
        {
            _easyDraw = new EasyDraw(game.width, game.height, false);
            _position = pPosition;
            _velocity = new Vector2();
            _acceleration = new Vector2(0f, .03f);
            charging = false;
            parent.AddChild(_easyDraw);
        }
        void Initialize(TiledObject obj)
        {
            _easyDraw = new EasyDraw(game.width, game.height,false);
            _position = new Vector2(obj.X, obj.Y);
            _acceleration = new Vector2(0.04f, .025f);
            game.AddChild(_easyDraw);
        }
        void UpdateScreenPosition()
        {
            x = _position.x;
            y = _position.y;
        }

        void Update()
        {
            
            _easyDraw.ClearTransparent();
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
            _mouseEndPosition = new Vector2(Input.mouseX, Input.mouseY);
            Vector2 diffVec = _mouseStartPosition - _mouseEndPosition;
            Vector2 speed = Vector2.GetUnitVectorDeg(diffVec.GetAngleDegrees()) * (Mathf.Clamp(diffVec.Length(), 0, maxSpeed));
            if (charging)
			{
                Vector2 projection = _position;
                Vector2 simulatedSpeed = speed;
                for(int i = 2; i <= 16; i += 2)
				{
                    simulatedSpeed += _acceleration * i;
                    projection += simulatedSpeed * i;
                    _easyDraw.Ellipse(projection.x,projection.y, 10 - (i/2), 10 -(i / 2));
				}
			}
            if (Input.GetMouseButtonUp(0) && charging)
            {

                _velocity = speed;
                charging = false;
            }

            if (isMoving)
            {
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