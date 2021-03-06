using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

static class Art
{
    public static Texture2D Player { get; private set; }
    public static Texture2D Seeker { get; private set; }
    public static Texture2D Wanderer { get; private set; }
    public static Texture2D Bullet { get; private set; }
    public static Texture2D Pointer { get; private set; }

    public static Texture2D Pixel { get; private set; }
    public static SpriteFont Font { get; private set; }

    public static Texture2D Wall { get; private set; }
    public static Texture2D Fuck { get; private set; }
    public static Texture2D Frog { get; private set; }
    public static Texture2D Fence { get; private set; }
    public static Texture2D Door { get; private set; }
    public static Texture2D Gate { get; private set; }
    public static Texture2D Paper { get; private set; }
    public static Texture2D Window { get; private set; }
    public static Texture2D Fai { get; private set; }
    public static Texture2D Leaves { get; private set; }
    public static Texture2D ExitSign { get; private set; }
    public static Texture2D BaseSign { get; private set; }
    public static Texture2D Message1 { get; private set; }
    public static Texture2D Message2 { get; private set; }
    public static Texture2D DarkWall { get; private set; }
    public static Texture2D BoneWall { get; private set; }


    public static void Load(ContentManager content)
    {
        Player = content.Load<Texture2D>("Art/Player");
        Seeker = content.Load<Texture2D>("Art/Seeker");
        Wanderer = content.Load<Texture2D>("Art/Wanderer");
        Bullet = content.Load<Texture2D>("Art/Bullet");
        Pointer = content.Load<Texture2D>("Art/Pointer");

        Pixel = new Texture2D(Player.GraphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.White });
        Font = content.Load<SpriteFont>("Story");

        Wall = content.Load<Texture2D>("Tiles/Wall");
        Fence = content.Load<Texture2D>("Tiles/Fence");
        Frog = content.Load<Texture2D>("Tiles/Sapo");
        Fence = content.Load<Texture2D>("Tiles/Fence");
        Door = content.Load<Texture2D>("Tiles/Door");
        Gate = content.Load<Texture2D>("Tiles/Gate");
        Paper = content.Load<Texture2D>("Tiles/Paper");
        Window = content.Load<Texture2D>("Tiles/Window");
        Fai = content.Load<Texture2D>("Tiles/Fai");
        Leaves = content.Load<Texture2D>("Tiles/Leaves");
        ExitSign = content.Load<Texture2D>("Tiles/Exit");
        BaseSign = content.Load<Texture2D>("Tiles/Basement");
        Message1 = content.Load<Texture2D>("Tiles/Message1");
        Message2 = content.Load<Texture2D>("Tiles/Message2");
        DarkWall = content.Load<Texture2D>("Tiles/DarkWall");
        BoneWall = content.Load<Texture2D>("Tiles/BoneWall");
    }
}