using Raylib_cs;
using System;

namespace cse210game
{
    public class EnemyFighterJet : GameObject
    {
        private int _speed;

        public EnemyFighterJet() : base(Raylib.GetScreenWidth(), new Random().Next(0, GameManager.SCREEN_HEIGHT), Color.Green, 45, 20)
        {
            _speed = new Random().Next(1, 3); // Reduce the speed range to make them slower
        }

        public override void Draw()
        {
            Raylib.DrawRectangle(_x, _y, _width, _height, _color);
        }

        public override void ProcessActions()
        {
            _x -= _speed; // Move towards the left side of the screen

            // Reset position if it goes off-screen
            if (_x < -_width)
            {
                _x = Raylib.GetScreenWidth();
                _y = new Random().Next(0, GameManager.SCREEN_HEIGHT);
            }
        }

        public override void HandleCollision(GameObject gameObject)
        {
            if (gameObject is FighterJet)
            {
                Console.WriteLine("Enemy Fighter Jet hit the player!");
                GameManager.Instance.LoseLife(); // Use the singleton to decrease lives
            }
            else if (gameObject is Bullet)
            {
                Console.WriteLine("Enemy Fighter Jet destroyed by bullet!");
                GameManager.Instance.AddScore(10); // Add points for destroying an enemy
            }
            else if (gameObject is Bomb)
            {
                Console.WriteLine("Enemy Fighter Jet destroyed by bomb!");
            }
        }
    }
}