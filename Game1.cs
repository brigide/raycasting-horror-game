using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace kMissCluster
{
    public enum GameState
    {
        InitStory = 0,
        Playing = 1,
        Ending1 = 2,
        Ending2 = 3
    }
    public class Game1 : Game
    {
        public static readonly bool isDevelopment = false;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Game1 Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }

        private Level level;

        public static GameState state;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;

            state = GameState.InitStory;
            //state = GameState.Playing;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            EntityManager.Add(Player.Instance);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Story.LoadStory();

            // TODO: use this.Content to load your game content here
            Art.Load(Content);

            LoadLevel("forest");

            int width = 1024;
            int height = 768;
            if (isDevelopment) width += level.Width * Tile.Width;
            _graphics.PreferredBackBufferWidth = width;

            //if (isDevelopment) height += level.Height * Tile.Height;
            _graphics.PreferredBackBufferHeight = height;

            _graphics.ApplyChanges();

            Player.Instance.Position = Level.start;
        }

        private void LoadLevel(string levelName)
        {
            // Load the level.
            string levelPath = $"Content/Levels/{levelName}.txt";
            //string levelPath = $"Content/Levels/{levelName}.txt";
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
                level = new Level(fileStream, levelName);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here  
            Input.Update();

            EntityManager.Update(level, gameTime);

            if (level.OpenedDoors.Count > 0)
            {
                var door = level.OpenedDoors.Peek();
                if (door.CloseSecond < gameTime.TotalGameTime.TotalSeconds)
                {
                    level.GetTiles[door.X, door.Y].Collision = TileCollision.ClosedDoor;
                    level.OpenedDoors.Dequeue();
                }
            }

            if (Player.Instance.ReachedForestEnd && Level.Name == "forest")
            {
                LoadLevel("building1");
                Player.Instance.Position = Level.start;
            }

            if (Player.Instance.ReachedBuilding1End && Level.Name == "building1")
            {
                LoadLevel("building2");
                Player.Instance.Position = Level.start;
            }

            if (Player.Instance.ReachedBuilding2End && Level.Name == "building2")
            {
                LoadLevel("choice");
                Player.Instance.Position = Level.start;
            }

            if (Player.Instance.PickedAltEnd && Level.Name == "choice")
            {
                LoadLevel("backforest");
                Player.Instance.Position = Level.start;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Player player = Player.Instance;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            EntityManager.Draw(_spriteBatch);

            if (isDevelopment) // draw 2d map only if is in dev mode
            {
                player.DrawRays(_spriteBatch);
                level.Draw(gameTime, _spriteBatch);
            }

            if (state == GameState.Playing)
            {
                player.DrawVision(_spriteBatch, level);
                if (Story.PlayingStory.Count > 0 && Level.Name == "forest")
                    Story.DrawPlayingLines(_spriteBatch, gameTime);
                else if (Story.BuildingStory.Count > 0 && Level.Name == "building1")
                    Story.DrawBuildingLines(_spriteBatch, gameTime);
                else if (Story.ChoiceLines.Count > 0 && Level.Name == "choice")
                    Story.DrawChoiceLine(_spriteBatch, gameTime);
            }
            if (state == GameState.InitStory)
            {
                if (Story.StoryLines.Count > 0)
                    Story.DrawStoryLine(_spriteBatch, gameTime);

                else
                {
                    state = GameState.Playing;
                    Story.currentDraw = Story.PlayingStory.Peek();
                }
            }


            if (Player.Instance.Building1PaperCount > Level.PapersRead1)
            {
                Story.DrawPaperLines(_spriteBatch, gameTime);
            }

            if (Player.Instance.Building2PaperCount > Level.PapersRead2)
            {
                Story.DrawPaperLines(_spriteBatch, gameTime);
            }

            if (Level.OpenedLockedDoor)
            {
                Story.DrawLockedLine(_spriteBatch, gameTime);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
