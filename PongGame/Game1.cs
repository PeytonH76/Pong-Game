using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    public class Game1 : Game
    {
        // Handles graphics
        private GraphicsDeviceManager _graphics;
        // Draws textures to the screen
        private SpriteBatch _spriteBatch;

        private Paddle _paddle1;
        private Paddle _paddle2;
        private Ball _ball;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        // Load the game content
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create the paddle texture
            Texture2D paddleTexture = new Texture2D(GraphicsDevice, 10, 100);
            Color[] data = new Color[10 * 100];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            paddleTexture.SetData(data);

            // Initialize paddles with their textures and starting positions
            _paddle1 = new Paddle(paddleTexture, new Vector2(50, 190));
            _paddle2 = new Paddle(paddleTexture, new Vector2(740, 190));

            // Create the ball texture
            Texture2D ballTexture = new Texture2D(GraphicsDevice, 20, 20);
            data = new Color[20 * 20];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            ballTexture.SetData(data);

            // Initialize ball with its texture, starting position, and velocity
            _ball = new Ball(ballTexture, new Vector2(390, 230), new Vector2(4, 4));
        }

        // Method called every frame to update game logic
        protected override void Update(GameTime gameTime)
        {
            // Exit the game if the back button or Escape key is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState(); // Get the current state of the keyboard

            // Update paddles based on keyboard input
            _paddle1.Update(keyboardState, Keys.W, Keys.S);
            _paddle2.Update(keyboardState, Keys.Up, Keys.Down);
            // Update ball position and handle collisions
            _ball.Update(gameTime, _paddle1, _paddle2);

            base.Update(gameTime);
        }

        // Method called every frame to draw the game to the screen
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); // Change the background color here

            _spriteBatch.Begin();

            _paddle1.Draw(_spriteBatch);
            _paddle2.Draw(_spriteBatch);
            _ball.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
