using System;
using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Scenes;
using TiledMapParser;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        public Vector2 position
        {
            get
            {
                return _position;
            }
        }
        MyGame myGame;

        Vector2 _position;
        Vector2 _velocity;
        Vector2 _acceleration;

        Vector2 _mouseStartPosition;
        Vector2 _mouseEndPosition;
        EasyDraw _easyDraw;

        public LevelCamera cam;
        public bool isMoving
        {
            get
            {
                return _velocity != new Vector2();
            }
        }
        bool charging = false;
        float maxSpeed = 5;

        public Action death;

        public Player(string fileName, int rows, int cols, TiledObject obj = null) 
            : base(obj.GetStringProperty("fileName"), obj.GetIntProperty("cols"), obj.GetIntProperty("rows"))
        {
            SetOrigin(width / 2, height / 2);
            Initialize(obj);
            SetCycle(0, 1);
        }

        public Player(Vector2 pPosition, Vector2 pVelocity) : base("PH_Airplane.png", 1, 1)
        {
            myGame = (MyGame)game;
            _easyDraw = new EasyDraw(game.width, game.height, false);
            _position = pPosition;
            _velocity = new Vector2();
            _acceleration = new Vector2(0f, .03f);
            charging = false;
            death = new Action(Death);
        }
        void Initialize(TiledObject obj)
        {
            myGame = (MyGame)game;
            _easyDraw = new EasyDraw(game.width, game.height,false);
            
            _position = new Vector2(obj.X, obj.Y);
            _acceleration = new Vector2(0.04f, .025f);
            death = new Action(Death);
        }

        void UpdateScreenPosition()
        {
            x = _position.x;
            y = _position.y;
        }

        void Update()
        {
            Animate(0.04f);
            if (_easyDraw.parent == null)
            {
                parent.AddChild(_easyDraw);
            }
            _easyDraw.ClearTransparent();
			if (Input.GetKeyDown(Key.R))
			{
                _position.SetXY(206.67f, 489.33f);
                _velocity = new Vector2();
                Level currentScene = (Level)SceneManager.instance.activeScene;
                currentScene.PlayerStoppedMoving();
			}

                
            float mouseX = cam.ScreenPointToGlobal(Input.mouseX, Input.mouseY).x;
            if(Input.GetMouseButtonDown(0) && (mouseX  > (x ) - width/2 && mouseX < (x) + width/2) && (Input.mouseY > y - height/2 && Input.mouseY < y + height/2) && !isMoving)
                {
                    _mouseStartPosition = new Vector2(Input.mouseX, Input.mouseY);
                    charging = true;
                    cam.canDrag = false;
                }

                _mouseEndPosition = new Vector2(Input.mouseX, Input.mouseY);
                Vector2 diffVec = _mouseStartPosition - _mouseEndPosition;
                Vector2 speed = Vector2.GetUnitVectorDeg(diffVec.GetAngleDegrees()) *
                                (Mathf.Clamp(diffVec.Length(), 0, maxSpeed));
                if (charging)
                {
                    Vector2 projection = _position;
                    Vector2 simulatedSpeed = speed;
                    for (int i = 2; i <= 16; i += 2)
                    {
                        simulatedSpeed += _acceleration * i;
                        projection += simulatedSpeed * i;
                        _easyDraw.Ellipse(projection.x, projection.y, 10 - (i / 2), 10 - (i / 2));
                    }
                }

                if (Input.GetMouseButtonUp(0) && charging)
                {
                    Level currentScene = (Level)SceneManager.instance.activeScene;
                    currentScene.PlayerStartedMoving();
                    _velocity = speed;
                    charging = false;
                    SetCycle(0, 8);
                }
            if (isMoving)
            {
                //TEMPORARY CODE PLEASE REMOVE AND CHANGE WITH GETACTIVE LEVEL AS SOON AS POSSIBLE BUT FOR TESTING SCENE THIS IS FINE
                Level currentScene = (Level)SceneManager.instance.activeScene;
                for (int i = 0; i < currentScene.GetNumberOfAppliers(); i++)
                {
                    ForceApplier applier = currentScene.GetForceApplier(i);
                    if (applier.IsInHorizontalReach(this, width, height) || applier.IsInVerticalReach(this, width, height))
                    {
                        _velocity += applier.force;
                    }
                }

                //Kill player when reaching border
                if(_position.x > currentScene.GetBorders().x || _position.x < 0 || _position.y > currentScene.GetBorders().y || _position.y < 0)
                {
                    death.Invoke();
                    LateDestroy();
                }

                //_acceleration += new Vector2(0, _acceleration.y / 2);
                _velocity += _acceleration;
                //_velocity.x = Mathf.Clamp(_velocity.x, 0, maxSpeed);
                _velocity.y = Mathf.Clamp(_velocity.y, -maxSpeed, maxSpeed);
                _velocity *= .995f;
                _position += _velocity;
            }
            UpdateScreenPosition();
        }
        void OnCollision(GameObject other)
        {
            if (other is Collectable)
            {
                Collectable collectable = (Collectable)other;
                collectable.Collect();
                return;
            }
            if (!(other is Goal) )
            {
				if (isMoving)
				{
                    //death += Death;
                    if(death != null)
                        death.Invoke();
				}
              
            }
        }

        static void Death()
        {
            Console.WriteLine("dead");
        } 
    }
}