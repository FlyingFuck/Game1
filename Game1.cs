using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Roleplay;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Vector2 translation;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tex;
        RenderTarget2D rt;
        Point virtDim;
        Point winDim;
        Vector2 scale;
        Vector2 rtPos;
        bool IsReleased;
        MagicTexture test;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            translation = new Vector2(-400, -400);
            virtDim = new Point(1920, 1080);
            winDim = new Point(960, 540);
            rt = new RenderTarget2D(GraphicsDevice, virtDim.X, virtDim.Y);
            ResizeWindow();
        }

        public void CalcScale()
        {
            float scaleX = (float)winDim.X / virtDim.X;
            float scaleY = (float)winDim.Y / virtDim.Y;
            if (scaleX > scaleY)
            {
                scale = new Vector2(scaleY);
                rtPos = new Vector2((winDim.X - virtDim.X * scaleY), 0);
            }
            else
            {
                scale = new Vector2(scaleX);
                rtPos = new Vector2(0, (winDim.Y - virtDim.Y * scaleX));
            }
        }

        public void ResizeWindow()
        {
            graphics.PreferredBackBufferHeight = winDim.Y;
            graphics.PreferredBackBufferWidth = winDim.X;
            graphics.ApplyChanges();
            CalcScale();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tex = Content.Load<Texture2D>("862675");
            test = new MagicTexture(tex, new Rectangle(0, 0, tex.Width, tex.Height), Facing.N);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.U) && IsReleased)
            {
                winDim.Y = (int)(winDim.Y * 1.2f);
                winDim.X = (int)(winDim.X * 1.2f);
                ResizeWindow();
                IsReleased = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.U))
                IsReleased = true;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.SetRenderTarget(rt);
            spriteBatch.Begin();
            test.Draw(spriteBatch, new Vector2(0, 0));
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            var scaler = Matrix.CreateScale(scale.X, scale.Y, 0);
            spriteBatch.Begin(transformMatrix: scaler);
            spriteBatch.Draw(rt, Vector2.Zero, null);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
