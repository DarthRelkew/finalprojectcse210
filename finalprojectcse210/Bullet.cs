using Raylib_cs;

public class Bullet : GameObject
{
    private int _speed;

    public Bullet(int x, int y, int speed) : base(x, y, Color.Black, 5, 2)
    {
        _speed = speed; // Set the speed of the bullet
    }

    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, _width, _height, _color);
    }

    public override void ProcessActions()
    {
        _x += _speed; // Move the bullet horizontally based on its speed
    }

    public bool IsOffScreen()
    {
        return _x > GameManager.SCREEN_WIDTH || _x < 0; // Check if the bullet is off-screen
    }
}