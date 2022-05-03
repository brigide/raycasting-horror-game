using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace kMissCluster
{
    class Scene
    {
        public List<float> Pixels;
        public List<bool> IsVerticalWall;

        public Scene()
        {
            Pixels = new List<float>();
            IsVerticalWall = new List<bool>();
        }

        public List<float> Normalize()
        {
            List<float> normalized = new List<float>();
            for (int i = 0; i < Pixels.Count; i++)
                normalized.Add((Pixels[i] - 100) / (512 - 100));

            return normalized;
        }

        public List<float> Heights()
        {
            List<float> heights = new List<float>();
            for (int i = 0; i < Pixels.Count; i++)
                heights.Add((Pixels[i] - 0) / (512 - 0));

            return heights;
        }
        public void Draw(SpriteBatch spriteBatch, Level level)
        {
            int wOffset = Game1.isDevelopment ? level.Width * Tile.Width : 0; // w offset will be applied only when developing
            int w = ((int)Game1.ScreenSize.X - wOffset) / Pixels.Count;

            var normalizedPixels = Normalize();
            for (int i = 0; i < Pixels.Count; i++)
            {
                //float lineHeight = 512 - Pixels[i];
                float lineHeight = (Tile.Width * Game1.ScreenSize.Y) / Pixels[i];
                if (lineHeight > Game1.ScreenSize.Y) lineHeight = Game1.ScreenSize.Y;

                float lineOffset = (Game1.ScreenSize.Y / 2) - lineHeight / 2;


                Color color = new Color((float)1, 0, 0, 1 - normalizedPixels[i]);
                if (IsVerticalWall[i])
                    color = new Color((float)0.7, 0, 0, 1 - normalizedPixels[i]);

                spriteBatch.Draw(Art.Pixel, new Rectangle(i * w + wOffset, (int)lineOffset, w, (int)lineHeight), color); //walls
                spriteBatch.Draw(Art.Pixel, new Rectangle(i * w + wOffset, (int)lineHeight + (int)lineOffset, w, (int)lineOffset), Color.Blue); //floor
                spriteBatch.Draw(Art.Pixel, new Rectangle(i * w + wOffset, 0, w, (int)lineOffset), Color.TransparentBlack);//celling
            }
        }

        private float Min()
        {
            float min = 10000000;
            foreach (float value in Pixels) if (value < min) min = value;
            return min;
        }

        private float Max()
        {
            float max = 0;
            foreach (float value in Pixels) if (value > max) max = value;
            return max;
        }
    }
}