using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace kMissCluster
{
    class Level
    {
        private Tile[,] tiles;

        public static string Name { get; private set; }

        public int Width
        {
            get { return tiles.GetLength(0); }
        }
        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        public static Vector2 start;
        public Queue<OpenDoorControl> OpenedDoors;
        private Point exit = InvalidPosition;
        private static readonly Point InvalidPosition = new Point(-1, -1);

        public Tile[,] GetTiles
        {
            get { return tiles; }
        }


        public Level(Stream fileStream, string name)
        {
            LoadTiles(fileStream);
            OpenedDoors = new Queue<OpenDoorControl>();
            Name = name;
        }

        private void LoadTiles(Stream fileStream)
        {
            // Load the level and ensure all of the lines are the same length.
            int width;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            // Allocate the tile grid.
            tiles = new Tile[width, lines.Count];

            // Loop over every tile position,
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // to load each tile.
                    char tileType = lines[y][x];
                    tiles[x, y] = LoadTile(tileType, x, y);
                }
            }

        }

        private Tile LoadTile(char tileType, int x, int y)
        {
            switch (tileType)
            {
                // Blank space
                case '.':
                    return new Tile(null, TileCollision.Passable);

                // Exit
                case 'X':
                    return LoadExitTile(x, y);

                // Wall
                case 'W':
                    return LoadWallTile(x, y, Art.Wall);

                case 'w':
                    return LoadWallTile(x, y, Art.Wall);

                // Fuck
                case 'F':
                    return LoadWallTile(x, y, Art.Fuck);

                case 'f':
                    return LoadWallTile(x, y, Art.Fuck);

                // Spawn point
                case 'P':
                    return LoadWallTile(x, y, Art.Frog);

                case 'p':
                    return LoadWallTile(x, y, Art.Frog);

                // door
                case 'D':
                    return LoadDoorTile(x, y);

                case 'd':
                    return LoadDoorTile(x, y);

                // Spawn point
                case 'S':
                    return SetSpawnPoint(x, y);

                case 's':
                    return SetSpawnPoint(x, y);

                // Unknown tile type character
                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }

        private Tile LoadWallTile(int x, int y, Texture2D art)
        {
            //var wall = GetBounds(x, y).Center;

            return new Tile(art, TileCollision.Impassable);
        }

        private Tile LoadDoorTile(int x, int y)
        {
            var wall = GetBounds(x, y).Center;

            return new Tile(Art.Door, TileCollision.ClosedDoor);
        }

        private Tile LoadExitTile(int x, int y)
        {
            exit = GetBounds(x, y).Center;

            return new Tile(Art.Wall, TileCollision.Passable);
        }

        private Tile SetSpawnPoint(int x, int y)
        {
            start = new Vector2(x * Tile.Width + Tile.Width / 2, y * Tile.Height + Tile.Height / 2);

            return new Tile(null, TileCollision.Passable);
        }

        public Rectangle GetBounds(int x, int y)
        {
            return new Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch);
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            // For each tile position
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    // If there is a visible tile in that position
                    Texture2D texture = tiles[x, y].Texture;
                    if (texture != null)
                    {
                        // Draw it in screen space.
                        Vector2 position = new Vector2(x, y) * Tile.Size;
                        spriteBatch.Draw(texture, position, Color.White);
                    }
                }
            }
        }
    }
}