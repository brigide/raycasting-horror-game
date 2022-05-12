using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace kMissCluster
{
    enum TileCollision
    {
        Passable = 0,
        Impassable = 1,
        Platform = 2,
        OpenDoor = 3,
        ClosedDoor = 4,
        Exit = 5,
        Exit2 = 6
    }
    struct Tile
    {
        public Texture2D Texture;
        public TileCollision Collision;

        public const int Width = 64;
        public const int Height = 64;

        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Texture2D texture, TileCollision collision)
        {
            Texture = texture;
            Collision = collision;
        }

        public Rectangle GetBounds(int x, int y)
        {
            Vector2 start = new Vector2(x, y) * Tile.Size;
            Vector2 end = Tile.Size;
            return new Rectangle((int)start.X, (int)start.Y, (int)end.X, (int)end.Y);
        }
    }
}