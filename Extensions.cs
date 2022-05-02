using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace kMissCluster
{
    static class Extensions
    {
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 2f)
        {
            Vector2 delta = end - start;
            spriteBatch.Draw(Art.Pixel, start, null, color, delta.ToAngle(), new Vector2(0, 0.5f), new Vector2(delta.Length(), thickness), SpriteEffects.None, 0f);
        }

        public static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 ScaleTo(this Vector2 vector, float length)
        {
            return vector * (length / vector.Length());
        }

        public static Point ToPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static float NextFloat(this Random rand, float minValue, float maxValue)
        {
            return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static Vector2 NextVector2(this Random rand, float minLength, float maxLength)
        {
            double theta = rand.NextDouble() * 2 * Math.PI;
            float length = rand.NextFloat(minLength, maxLength);
            return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
        }

        public static float Distance(Vector2 start, Vector2 end, double angle)
        {
            return (float)Math.Sqrt((end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y));
        }

        public static Vector2 RadToVector(float angleRad)
        {
            return new Vector2((float)Math.Cos(angleRad), (float)Math.Sin(angleRad));
        }

        public static float ToRadians(float angle)
        {
            return (float)(Math.PI / 180) * angle;
        }
    }
}