using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    public class Ball
    {
        private Texture2D _texture;
        public Vector2 Position;
        public Vector2 Speed;

        private readonly Vector2 _initialPosition;
        private readonly Vector2 _initialSpeed;
        private bool _isMoving;
        private double _timeSinceReset;

        public Ball(Texture2D texture, Vector2 position, Vector2 speed)
        {
            _texture = texture;
            _initialPosition = position;
            _initialSpeed = speed;
            Reset();
        }

        // Method to update the ball's position and handle collisions
        public void Update(GameTime gameTime, Paddle paddle1, Paddle paddle2)
        {
            // If the ball is not moving, wait for 1 second before starting
            if (!_isMoving)
            {
                _timeSinceReset += gameTime.ElapsedGameTime.TotalSeconds;
                if (_timeSinceReset >= 1.0) // 1 second delay
                {
                    _isMoving = true; // Start moving the ball
                    _timeSinceReset = 0; // Reset the timer
                }
                return;
            }

            // Move the ball by its speed
            Position += Speed;

            // Bounce off the top and bottom edges of the screen
            if (Position.Y <= 0 || Position.Y >= 480 - _texture.Height)
                Speed.Y *= -1;

            // Check for out of bounds on left and right edges
            if (Position.X <= 0 || Position.X >= 800 - _texture.Width)
                Reset(); // Reset the ball if it goes out of bounds

            // Check for collision with paddles
            Rectangle ballRect = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            Rectangle paddle1Rect = new Rectangle((int)paddle1.Position.X, (int)paddle1.Position.Y, paddle1.Texture.Width, paddle1.Texture.Height);
            Rectangle paddle2Rect = new Rectangle((int)paddle2.Position.X, (int)paddle2.Position.Y, paddle2.Texture.Width, paddle2.Texture.Height);

            // If the ball intersects with either paddle, reverse its horizontal direction
            if (ballRect.Intersects(paddle1Rect) || ballRect.Intersects(paddle2Rect))
                Speed.X *= -1;
        }

        // Method to draw the ball on the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the ball texture at its current position
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        // Method to reset the ball to its initial position and speed
        private void Reset()
        {
            Position = _initialPosition;
            Speed = _initialSpeed;
            _isMoving = false; // Stop the ball from moving initially
            _timeSinceReset = 0;  // Reset the timer

            // Optionally, randomize the direction of the ball
            if (Speed.X > 0)
                Speed.X = -Speed.X;
        }
    }
}
