using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace kMissCluster
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Game1 Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }

        private Level level;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _graphics.PreferredBackBufferWidth = 2 * 8 * 64;
            _graphics.PreferredBackBufferHeight = 8 * 64;
            _graphics.ApplyChanges();

            EntityManager.Add(Player.Instance);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Art.Load(Content);

            LoadLevel("forest");
        }

        private void LoadLevel(string levelName)
        {
            // Load the level.
            string levelPath = $"Content/Levels/{levelName}.txt";
            using (Stream fileStream = TitleContainer.OpenStream(levelPath))
                level = new Level(fileStream);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here  
            Input.Update();

            EntityManager.Update(level);

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
            player.DrawRays(_spriteBatch);
            level.Draw(gameTime, _spriteBatch);
            player.DrawVision(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
