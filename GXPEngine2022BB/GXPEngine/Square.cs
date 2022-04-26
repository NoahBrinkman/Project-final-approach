using System;
using GXPEngine;
using GXPEngine.Core;

public class Square : Sprite
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

    bool isMoving
    {
        get
        {
            return _velocity != new Vector2();
        }
    }
    bool charging = false;
    float maxSpeed = 5;
    public Square(Vector2 pPosition, Vector2 pVelocity) : base("square.png")
    {
        myGame = (MyGame)game;
        _position = pPosition;
        _velocity = new Vector2();
        _acceleration = new Vector2(0f, .01f);
        charging = false;
    }

    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }

    void Update()
    {
      
        if (Input.GetMouseButtonDown(0) && (Input.mouseX > x && Input.mouseX < x + width) && (Input.mouseY > y && Input.mouseY < y + height) && !isMoving)
        {
            _mouseStartPosition = new Vector2(Input.mouseX, Input.mouseY);
            charging = true;
        }
        if (Input.GetMouseButtonUp(0) && charging)
        {
            _mouseEndPosition = new Vector2(Input.mouseX, Input.mouseY);
            Vector2 diffVec = _mouseStartPosition - _mouseEndPosition;
            Vector2 speed = Vector2.GetUnitVectorDeg(diffVec.GetAngleDegrees()) * (Mathf.Clamp(diffVec.Length(), 0, maxSpeed) / 2);
            _velocity = speed;
            charging = false;
        }

        if (isMoving)
        {
            for (int  i = 0;  i < myGame.GetNumberOfAppliers();  i++)
            {
                ForceApplier applier = myGame.GetForceApplier(i);
                if (applier.IsInReach(this, width,height))
                {
                    Console.WriteLine("force applying");
                    _velocity += applier.force;
                }
            }


            _velocity += _acceleration;
            _position += _velocity;
        }
        UpdateScreenPosition();
    }
}