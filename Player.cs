using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace kMissCluster
{
    class Player : Entity
    {

        private List<Ray2D> Rays { get; set; }
        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();

                return instance;
            }
        }

        private Player()
        {
            image = Art.Player;
            Position = new Vector2(128, 64 * 5);//Game1.ScreenSize;
            Radius = 10;

            Rays = new List<Ray2D>();

            for (int a = 0; a < 60; a++)
            {
                Rays.Add(new Ray2D(Position, Extensions.ToRadians(a)));
            }

            // float rayAngle = (float)Angle - Extensions.ToRadians(1) * 30;
            // if (rayAngle < 0) rayAngle += 2 * (float)Math.PI;
            // if (rayAngle > 2 * (float)Math.PI) rayAngle -= 2 * (float)Math.PI;
            // Rays.Add(new Ray2D(Position, rayAngle));
        }

        public override void Update(Level level)
        {
            // player logic goes here
            instance = Input.GetMovementByPlayerInput(instance);

            float rayAngle = (float)Angle - Extensions.ToRadians(1) * 30;
            if (rayAngle < 0) rayAngle += 2 * (float)Math.PI;
            if (rayAngle > 2 * (float)Math.PI) rayAngle -= 2 * (float)Math.PI;
            for (int i = 0; i < Rays.Count; i++)
            {
                Rays[i].SetAngle((float)rayAngle);
                rayAngle += Extensions.ToRadians(1);
                if (rayAngle < 0) rayAngle += 2 * (float)Math.PI;
                if (rayAngle > 2 * (float)Math.PI) rayAngle -= 2 * (float)Math.PI;

                Rays[i].Position = Position;
            }

            Tile[,] tiles = level.GetTiles;
            foreach (Ray2D ray in Rays)
            {
                ray.Closest = Vector2.Zero;
                ray.Distance = 100000000;
                // Loop over every tile position,
                for (int y = 0; y < level.Height; ++y)
                {
                    for (int x = 0; x < level.Width; ++x)
                    {
                        Tile tile = tiles[x, y];
                        if (tile.Collision != TileCollision.Passable)
                        {
                            Vector2 topLeft = new Vector2(x, y) * Tile.Size;
                            Vector2 topRight = new Vector2(x, y) * Tile.Size + new Vector2(Tile.Width, 0);
                            Vector2 bottomRight = new Vector2(x, y) * Tile.Size + new Vector2(Tile.Width, Tile.Height);
                            Vector2 bottomLeft = new Vector2(x, y) * Tile.Size + new Vector2(0, Tile.Height);

                            Vector2 point1 = ray.Cast(topLeft, topRight);
                            Vector2 point2 = ray.Cast(topRight, bottomRight);
                            Vector2 point3 = ray.Cast(bottomRight, bottomLeft);
                            Vector2 point4 = ray.Cast(bottomLeft, topLeft);

                            float distance = ray.Distance;
                            Vector2 closest = ray.Closest;
                            if (point1 != Vector2.Zero)
                            {
                                var dist1 = Vector2.Distance(Position, point1);
                                if (dist1 < distance)
                                {
                                    distance = dist1;
                                    closest = point1;
                                }
                            }
                            if (point2 != Vector2.Zero)
                            {
                                var dist2 = Vector2.Distance(Position, point2);
                                if (dist2 < distance)
                                {
                                    distance = dist2;
                                    closest = point2;
                                }
                            }
                            if (point3 != Vector2.Zero)
                            {
                                var dist3 = Vector2.Distance(Position, point3);
                                if (dist3 < distance)
                                {
                                    distance = dist3;
                                    closest = point3;
                                }
                            }
                            if (point4 != Vector2.Zero)
                            {
                                var dist4 = Vector2.Distance(Position, point4);
                                if (dist4 < distance)
                                {
                                    distance = dist4;
                                    closest = point4;
                                }
                            }

                            if (distance < ray.Distance)
                            {
                                ray.Distance = distance;
                                ray.Closest = closest;
                            }

                        }
                    }
                }
            }

            //HandleLevelCollisions(level);
        }

        public void DrawRays(SpriteBatch spriteBatch)
        {
            foreach (Ray2D ray in Rays)
            {
                //ray.DrawLine(spriteBatch, Position, Position + ray.Direction * 15);
                ray.DrawLine(spriteBatch, Position, ray.Closest);
            }
        }

        private void HandleLevelCollisions(Level level)
        {

        }
    }

    class Wall
    {
        public Wall(Vector2 s, Vector2 e)
        {
            start = s;
            end = e;
        }
        public Vector2 start;
        public Vector2 end;
    }
}