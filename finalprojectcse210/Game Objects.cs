using Raylib_cs;

public abstract class GameObject
{
    protected int _x;
    protected int _y;
    protected int _width;
    protected int _height;
    protected Color _color;

    public GameObject(int x, int y, Color color, int width = 0, int height = 0)
    {
        _x = x;
        _y = y;
        _color = color;
        _width = width;
        _height = height;
    }

    public abstract void Draw();

    public virtual void HandleInput() { }

    public virtual void ProcessActions() { }

    public virtual void HandleCollision(GameObject gameObject) { }

    public Rectangle GetBounds()
    {
        return new Rectangle(_x, _y, _width, _height);
    }

    public bool CollidesWith(GameObject other)
    {
        return Raylib.CheckCollisionRecs(this.GetBounds(), other.GetBounds());
    }
}