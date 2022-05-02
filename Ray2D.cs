using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace kMissCluster
{
    class Ray2D
    {
        public Vector2 Position;
        public Vector2 Direction;
        public Vector2 Closest;
        public float Distance;
        public float Angle;
        public bool isVerticalWall;
        public Ray2D(Vector2 position, float angle)
        {
            Position = position;
            Direction = Extensions.RadToVector(angle);

            Angle = angle;
        }

        public void LookAt(float x, float y)
        {
            Direction.X = x - Position.X;
            Direction.Y = y - Position.Y;
            Direction.Normalize();
        }

        public Vector2 Cast(Vector2 targetStart, Vector2 targetEnd)
        {
            float x1 = targetStart.X;
            float y1 = targetStart.Y;
            float x2 = targetEnd.X;
            float y2 = targetEnd.Y;

            float x3 = Position.X;
            float y3 = Position.Y;
            float x4 = Position.X + Direction.X;
            float y4 = Position.Y + Direction.Y;

            float denomiator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4); //the formula's denominator
            if (denomiator == 0) return Vector2.Zero;

            float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denomiator; //t's formula
            float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / denomiator; //u's formula

            if (t > 0 && t < 1 && u > 0)
            {
                return new Vector2(
                    x1 + t * (x2 - x1),
                    y1 + t * (y2 - y1)
                );
            }
            return Vector2.Zero;
        }

        public void SetAngle(float angle)
        {
            Direction = Extensions.RadToVector(angle);
            Angle = angle;
        }

        public void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            //spriteBatch.Begin();
            spriteBatch.DrawLine(start, end, Color.White);
            //spriteBatch.End();
        }
    }
}