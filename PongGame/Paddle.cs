using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    public class Paddle
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position;
        public Vector2 Speed;

        public Paddle(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Speed = new Vector2(0, 5); // Paddle moves vertically at a speed of 5 units per update
        }

        // Method to update the paddle's position based on keyboard input
        public void Update(KeyboardState keyboardState, Keys upKey, Keys downKey)
        {
            // Move paddle up if the upKey is pressed
            if (keyboardState.IsKeyDown(upKey))
                Position.Y -= Speed.Y;
            // Move paddle down if the downKey is pressed
            if (keyboardState.IsKeyDown(downKey))
                Position.Y += Speed.Y;

            // Keep the paddle within the screen bounds (assuming a screen height of 480 pixels)
            Position.Y = MathHelper.Clamp(Position.Y, 0, 480 - Texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}

