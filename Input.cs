using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System;

namespace kMissCluster
{
    static class Input
    {
        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;
        private static GamePadState gamepadState, lastGamepadState;

        private static bool isAimingWithMouse = false;

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

        public static void Update()
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
            lastGamepadState = gamepadState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            gamepadState = GamePad.GetState(PlayerIndex.One);

            // If the player pressed one of the arrow keys or is using a gamepad to aim, we want to disable mouse aiming. Otherwise,
            // if the player moves the mouse, enable mouse aiming.
            if (new[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down }.Any(x => keyboardState.IsKeyDown(x)) || gamepadState.ThumbSticks.Right != Vector2.Zero)
                isAimingWithMouse = false;
            else if (MousePosition != new Vector2(lastMouseState.X, lastMouseState.Y))
                isAimingWithMouse = true;
        }

        // Checks if a key was just pressed down
        public static bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

        public static bool WasButtonPressed(Buttons button)
        {
            return lastGamepadState.IsButtonUp(button) && gamepadState.IsButtonDown(button);
        }

        public static Player GetMovementByPlayerInput(Player player, Level level)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                player.Angle -= 0.05;
                if (player.Angle < 0) player.Angle += 2 * Math.PI;
                player.Delta.X = (float)Math.Cos(player.Angle) * 2;
                player.Delta.Y = (float)Math.Sin(player.Angle) * 2;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                player.Angle += 0.05;
                if (player.Angle > 2 * Math.PI) player.Angle -= 2 * Math.PI;
                player.Delta.X = (float)Math.Cos(player.Angle) * 2;
                player.Delta.Y = (float)Math.Sin(player.Angle) * 2;
            }


            int xOffset = 0;
            if (player.Delta.X < 0) xOffset = -20; else xOffset = 20;
            int yOffset = 0;
            if (player.Delta.Y < 0) yOffset = -20; else yOffset = 20;
            Vector2 offset = new Vector2(xOffset, yOffset);

            Vector2 gridPosition = player.Position;
            gridPosition /= Tile.Size;
            gridPosition = new Vector2((int)gridPosition.X, (int)gridPosition.Y); //player's grid position
            Vector2 gridAddOffset = (player.Position + offset) / Tile.Size; gridAddOffset = new Vector2((int)gridAddOffset.X, (int)gridAddOffset.Y);
            Vector2 gridSubOffset = (player.Position - offset) / Tile.Size; gridSubOffset = new Vector2((int)gridSubOffset.X, (int)gridSubOffset.Y);


            if (keyboardState.IsKeyDown(Keys.W))
            {
                if (level.GetTiles[(int)gridAddOffset.X, (int)gridPosition.Y].Collision == TileCollision.Passable
                    || level.GetTiles[(int)gridAddOffset.X, (int)gridPosition.Y].Collision == TileCollision.OpenDoor)
                    player.Position.X += player.Delta.X;
                if (level.GetTiles[(int)gridPosition.X, (int)gridAddOffset.Y].Collision == TileCollision.Passable
                    || level.GetTiles[(int)gridPosition.X, (int)gridAddOffset.Y].Collision == TileCollision.OpenDoor)
                    player.Position.Y += player.Delta.Y;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                if (level.GetTiles[(int)gridSubOffset.X, (int)gridPosition.Y].Collision == TileCollision.Passable
                    || level.GetTiles[(int)gridSubOffset.X, (int)gridPosition.Y].Collision == TileCollision.OpenDoor)
                    player.Position.X -= player.Delta.X;
                if (level.GetTiles[(int)gridPosition.X, (int)gridSubOffset.Y].Collision == TileCollision.Passable
                    || level.GetTiles[(int)gridPosition.X, (int)gridSubOffset.Y].Collision == TileCollision.OpenDoor)
                    player.Position.Y -= player.Delta.Y;
            }

            if (keyboardState.IsKeyDown(Keys.E))
            {
                if (level.GetTiles[(int)gridAddOffset.X, (int)gridAddOffset.Y].Collision == TileCollision.ClosedDoor)
                {
                    level.GetTiles[(int)gridAddOffset.X, (int)gridAddOffset.Y].Collision = TileCollision.OpenDoor;
                }

            }

            return player;
        }

        public static Vector2 GetMovementDirection()
        {

            Vector2 direction = gamepadState.ThumbSticks.Left;
            direction.Y *= -1;  // invert the y-axis

            if (keyboardState.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D))
                direction.X += 1;
            if (keyboardState.IsKeyDown(Keys.W))
                direction.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S))
                direction.Y += 1;

            // Clamp the length of the vector to a maximum of 1.
            if (direction.LengthSquared() > 1)
                direction.Normalize();

            return direction;
        }

        public static Vector2 GetAimDirection()
        {
            if (isAimingWithMouse)
                return GetMouseAimDirection();

            Vector2 direction = gamepadState.ThumbSticks.Right;
            direction.Y *= -1;

            if (keyboardState.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (keyboardState.IsKeyDown(Keys.Right))
                direction.X += 1;
            if (keyboardState.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.Down))
                direction.Y += 1;

            // If there's no aim input, return zero. Otherwise normalize the direction to have a length of 1.
            if (direction == Vector2.Zero)
                return Vector2.Zero;
            else
                return Vector2.Normalize(direction);
        }

        private static Vector2 GetMouseAimDirection()
        {
            Vector2 direction = MousePosition - Player.Instance.Position;

            if (direction == Vector2.Zero)
                return Vector2.Zero;
            else
                return Vector2.Normalize(direction);
        }

        public static bool WasBombButtonPressed()
        {
            return WasButtonPressed(Buttons.LeftTrigger) || WasButtonPressed(Buttons.RightTrigger) || WasKeyPressed(Keys.Space);
        }
    }
}