using Raylib_cs;
using System;

namespace cse210game
{
    public class Bomb : GameObject
    {
        private int _speed;
        private static Random _random = new Random();

        public Bomb(int playerY) : base(_random.Next(0, Raylib.GetScreenWidth()), 0, Color.Red, 15, 15)
        {
            _speed = _random.Next(1, 3); // Random falling speed

            // Ensure bombs do not spawn near the player's jet
            while (Math.Abs(_y - playerY) < 50)
            {
                _x = _random.Next(0, Raylib.GetScreenWidth());
                _y = 0;
            }
        }

        public Bomb() : base(new Random().Next(50, GameManager.SCREEN_WIDTH - 50), 0, Color.Green, 20, 20)
        {
            _speed = 2; // Slow the descent speed
        }

        public override void Draw()
        {
            Raylib.DrawRectangle(_x, _y, _width, _height, _color);
        }

        public override void ProcessActions()
        {
            _y += _speed; // Move the bomb downward slowly

            if (_y > GameManager.SCREEN_HEIGHT)
            {
                ResetPosition(); // Reset bomb position when it goes off-screen
            }
        }

        private void ResetPosition()
        {
            _x = new Random().Next(50, GameManager.SCREEN_WIDTH - 50);
            _y = 0;
        }

        public override void HandleCollision(GameObject gameObject)
        {
            if (gameObject is EnemyFighterJet)
            {
                Console.WriteLine("Bomb destroyed an enemy fighter jet!");
            }
            else if (gameObject is FighterJet)
            {
                // Do nothing; bombs should not affect the player's fighter jet
            }
        }
    }
}