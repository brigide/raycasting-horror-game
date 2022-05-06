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

        public List<float> Distances;

        public List<Color[]> Textures;

        public static Color floor;
        public static Color celling;

        public Scene()
        {
            Pixels = new List<float>();
            IsVerticalWall = new List<bool>();
            Textures = new List<Color[]>();
            Distances = new List<float>();

            floor = new Color(Normalize(120), Normalize(120), Normalize(120));
            celling = new Color(Normalize(0), Normalize(0), Normalize(41));
            // floor = new Color(Normalize(0), Normalize(0), Normalize(0));
            // celling = new Color(Normalize(0), Normalize(0), Normalize(0));
        }

        public List<float> Normalize()
        {
            List<float> normalized = new List<float>();
            for (int i = 0; i < Pixels.Count; i++)
                normalized.Add((Pixels[i] - 100) / (512 - 100));

            return normalized;
        }

        public float Normalize(float a)
        {
            return (a - 0) / (255 - 0);
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
                //Textures[i] = GetTexture(spriteBatch, Textures[i]);
                float tyStep = Tile.Height / lineHeight; // y step to draw the textures
                float tyOffset = 0; // control to fix bugged y textures
                if (lineHeight > Game1.ScreenSize.Y)
                {
                    tyOffset = (lineHeight - Game1.ScreenSize.Y) / 2;
                    lineHeight = Game1.ScreenSize.Y;
                }

                float lineOffset = (Game1.ScreenSize.Y / 2) - lineHeight / 2;

                float ty = tyOffset * tyStep;
                float tx = (int)(Distances[i] / 1) % Tile.Width;

                float a = 1;
                float alpha = 1 - normalizedPixels[i];
                if (IsVerticalWall[i])
                    a = (float)0.9;

                //spriteBatch.Draw(Textures[i], new Rectangle(i * w + wOffset, (int)lineOffset, w, (int)lineHeight), color); //walls

                for (int y = 0; y < lineHeight; y++)
                {
                    Color color = GetPixel(Textures[i], (int)tx, (int)ty, a, alpha);

                    spriteBatch.Draw(Art.Pixel, new Rectangle(i * w + wOffset, (int)lineOffset + y, w, 1), color); //walls
                    ty += tyStep;
                }

                spriteBatch.Draw(Art.Pixel, new Rectangle(i * w + wOffset, (int)lineHeight + (int)lineOffset, w, (int)lineOffset), floor); //floor
                spriteBatch.Draw(Art.Pixel, new Rectangle(i * w + wOffset, 0, w, (int)lineOffset), celling);//celling
            }
        }

        private Color GetPixel(Color[] colors, int X, int Y, float a, float alpha)
        {
            if (Y > Tile.Height - 1) Y = Tile.Height - 1;
            if (X > Tile.Width - 1) X = Tile.Width - 1;
            if (X < 0) X = 0;
            Color color = colors[X + Y * Tile.Width];
            if (color.R == 255 && color.G == 0 && color.B == 255)
                return new Color(0, 0, 0, 0);
            float r = Normalize(color.R);
            float g = Normalize(color.G);
            float b = Normalize(color.B);
            return new Color(r * a, g * a, b * a, alpha);
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


        private Texture2D GetTexture(SpriteBatch spriteBatch, Texture2D texture)
        {
            var array = TextureTo2DArray(texture);
            var farray = Texture2DArraySlice(array);
            Texture2D tx = new Texture2D(spriteBatch.GraphicsDevice, 1, Tile.Height);
            tx.SetData<Color>(farray);
            return tx;
        }
        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);
            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * texture.Width];
                }
            }
            return colors2D;
        }
        private Color[] Texture2DArraySlice(Color[,] array)
        {
            Color[] colors = new Color[Tile.Height];
            for (int x = 0; x < 1; x++)
            {
                for (int y = 0; y < Tile.Height; y++)
                {
                    colors[y] = array[x, y];
                }
            }
            return colors;
        }
    }
}