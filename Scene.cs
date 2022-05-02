using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace kMissCluster
{
    class Scene
    {
        public List<float> Pixels;

        public Scene()
        {
            Pixels = new List<float>();
        }

        public List<float> Normalize()
        {
            List<float> normalized = new List<float>();
            for (int i = 0; i < Pixels.Count; i++)
                normalized.Add((Pixels[i] - 0) / (512 - 0));

            return normalized;
        }

        public List<float> Heights()
        {
            List<float> heights = new List<float>();
            for (int i = 0; i < Pixels.Count; i++)
                heights.Add((Pixels[i] - 0) / (512 - 0));

            return heights;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int wOffset = 512;
            int w = 512 / Pixels.Count;

            var normalizedPixels = Normalize();
            for (int i = 0; i < Pixels.Count; i++)
            {
                float h = 512 - Pixels[i];
                spriteBatch.Draw(Art.Pixel, new Rectangle(i * w + wOffset, (int)Pixels[i] / 2, w, (int)h), new Color(Color.White, 1 - normalizedPixels[i]));
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