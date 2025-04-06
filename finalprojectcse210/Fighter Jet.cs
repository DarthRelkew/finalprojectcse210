using Raylib_cs;

namespace cse210game
{
    public class FighterJet : GameObject
    {
        private const int MOVE_SPEED = 8;
        private const int BULLET_SPEED = 10;
        private List<Bullet> _bullets = new List<Bullet>();

        public FighterJet(int x, int y) : base(x, y, Color.Blue)
        {
            _width = 50;
            _height = 10;
        }

        public override void Draw()
        {
            Raylib.DrawRectangle(_x, _y, _width, _height, _color);
            foreach (var bullet in _bullets)
            {
                bullet.Draw();
            }
        }

        public override void HandleInput()
        {
            if (Raylib.IsKeyDown(KeyboardKey.Up) && _y > 0)
            {
                _y -= MOVE_SPEED;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Down) && _y < GameManager.SCREEN_HEIGHT - _height)
            {
                _y += MOVE_SPEED;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Bullet bullet = new Bullet(_x + _width, _y + _height / 2, 10); // Pass the bullet speed
            _bullets.Add(bullet);
        }

        public override void ProcessActions()
        {
            foreach (var bullet in _bullets)
            {
                bullet.ProcessActions();
            }

            _bullets.RemoveAll(b => b.IsOffScreen());
        }
    }

    
}