using System;
using GXPEngine;

public struct Vector2
{
    public float x;
    public float y;

    public Vector2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public void Normalize()
    {
        float length = Length();
        if (length != 0)
        {
            x /= length;
            y /= length;
        }
    }

    public Vector2 Normalized()
    {
        float length = Length();
        if (length != 0)
        {
            float normalizedX = x / length;
            float normalizedY = y / length;
            return new Vector2(normalizedX, normalizedY);
        }
        else
        {
            return new Vector2(0, 0);
        }
    }

    public float Distance(Vector2 target)
    {
        Vector2 distanceVec = this - target;
        return distanceVec.Length();
    }

    public void SetXY(float newX, float newY)
    {
        x = newX;
        y = newY;
    }

    public void SetAngleDegrees(float deg)
    {
        RotateDegrees(deg - GetAngleDegrees());
    }

    public void SetAngleRadians(float rad)
    {
        RotateRadians(rad - GetAngleRadians());
    }

    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }

    public void RotateDegrees(float deg)
    {
        float radCos = Mathf.Cos(Deg2Rad(deg));
        float radSin = Mathf.Sin(Deg2Rad(deg));
        this = new Vector2(x * radCos - y * radSin, x * radSin + y * radCos);
    }

    public void RotateRadians(float rad)
    {
        float radCos = Mathf.Cos(rad);
        float radSin = Mathf.Sin(rad);
        this = new Vector2(x * radCos - y * radSin, x * radSin + y * radCos);
    }

    public void RotateAroundDegrees(Vector2 target, float angle)
    {
        this -= target;
        RotateDegrees(angle);
        this += target;
    }

    public void RotateAroundRadians(Vector2 target, float angle)
    {
        this -= target;
        RotateRadians(angle);
        this += target;
    }

    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        return new Vector2(left.x + right.x, left.y + right.y);
    }

    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        return new Vector2(left.x - right.x, left.y - right.y);
    }

    public static Vector2 operator *(Vector2 vector, float amount)
    {
        return new Vector2(vector.x * amount, vector.y * amount);
    }

    public static Vector2 operator *(float amount, Vector2 vector)
    {
        return new Vector2(vector.x * amount, vector.y * amount);
    }
    public static bool operator ==(Vector2 left, Vector2 right)
    {
        return left.x == right.x && left.y == right.y;
    }
    public static bool operator !=(Vector2 left, Vector2 right)
    {
        return left.x != right.x || left.y != right.y;
    }
    public static float Rad2Deg(float rad)
    {
        return rad * (180 / Mathf.PI);
    }

    public static float Deg2Rad(float deg)
    {
        return deg * (Mathf.PI / 180);
    }

    public static Vector2 GetUnitVectorDeg(float deg)
    {
        float rad = Deg2Rad(deg);
        return new Vector2(pX: Mathf.Cos(rad), pY: Mathf.Sin(rad));
    }

    public static Vector2 GetUnitVectorRad(float rad)
    {
        return new Vector2(pX: Mathf.Cos(rad), pY: Mathf.Sin(rad));
    }

    public static Vector2 RandomUnitVector()
    {
        return GetUnitVectorDeg(Utils.Random(0, 360));
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }
}

