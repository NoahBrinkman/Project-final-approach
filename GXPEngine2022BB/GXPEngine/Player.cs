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
        private MyGame myGame;

        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _acceleration;

        private Vector2 _mouseStartPosition;
        private Vector2 _mouseEndPosition;
        private EasyDraw _easyDraw;

        public Level level;
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


            float mouseX = level.levelCamera.ScreenPointToGlobal(Input.mouseX, Input.mouseY).x;
            if(Input.GetMouseButtonDown(0) && (mouseX  > (x ) - width/2 && mouseX < (x) + width/2) && (Input.mouseY > y - height/2 && Input.mouseY < y + height/2) && !isMoving)
                {
                    _mouseStartPosition = new Vector2(Input.mouseX, Input.mouseY);
                    charging = true;
                    level.levelCamera.canDrag = false;
                }

                _mouseEndPosition = new Vector2(Input.mouseX, Input.mouseY);
                Vector2 diffVec = _mouseStartPosition - _mouseEndPosition;
                Vector2 speed = Vector2.GetUnitVectorDeg(diffVec.GetAngleDegrees()) *
                                (Mathf.Clamp(diffVec.Length() / 4, 0, maxSpeed));
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
                    level.PlayerStartedMoving();
                    _velocity = speed;
                    charging = false;
                    SetCycle(0, 8);
                }
            if (isMoving)
            {
                for (int i = 0; i < level.GetNumberOfAppliers(); i++)
                {
                    ForceApplier applier = level.GetForceApplier(i);
                    if (applier.IsInHorizontalReach(this, width, height) || applier.IsInVerticalReach(this, width, height))
                    {
                        _velocity += applier.force;
                    }
                }

                //Kill player when reaching border
                if(_position.x > level.GetBorders().x || _position.x < 0 || _position.y > level.GetBorders().y || _position.y < 0)
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
                    if(death != null)
                        death.Invoke();
				}
              
            }

            if(other is TogglableForceApplier)
            {
                TogglableForceApplier forceApplier = (TogglableForceApplier)other;
                if(forceApplier.threat == "shred" && forceApplier.activated)
                    SetCycle(18, 9);
                else if(forceApplier.threat == "fire" && forceApplier.activated)
                    SetCycle(9, 9);
            }
            else
            {
                SetCycle(9, 0);
            }
        }

        public void StopSimulating()
        {
            _acceleration = new Vector2(0,0);
            _velocity = new Vector2(0,0);
        }

        static void Death()
        {
            Console.WriteLine("dead");
        } 
    }
}