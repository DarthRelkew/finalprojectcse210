using cse210game;
using Raylib_cs;
using System.Collections.Generic;

class GameManager
{
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;

    private string _title;
    private List<GameObject> _gameObjects = new List<GameObject>();
    private int _score = 0;
    private int _lives = 3;

    public static GameManager Instance { get; private set; }

    public GameManager()
    {
        _title = "Jet Fighter Escape";
        Instance = this;
    }

    // Game Loop
    public void Run()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, _title);

        InitializeGame();

        while (!Raylib.WindowShouldClose())
        {
            if (_lives > 0)
            {
                HandleInput();
                ProcessActions();
                HandleCollisions();
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            if (_lives > 0)
            {
                DrawElements();
                DrawHUD();
            }
            else
            {
                DrawGameOver();
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private void InitializeGame()
    {
        int playerY = SCREEN_HEIGHT / 2;
        FighterJet player = new FighterJet(50, playerY);
        _gameObjects.Add(player);

        for (int i = 0; i < 3; i++) // Reduce the number of enemy fighter jets to 3
        {
            _gameObjects.Add(new EnemyFighterJet());
        }

        SpawnBombs();
    }

    private void SpawnBombs()
    {
        for (int i = 0; i < 3; i++)
        {
            Bomb bomb;
            do
            {
                bomb = new Bomb(SCREEN_HEIGHT / 2);
            } while (IsNearPlayer(bomb));
            _gameObjects.Add(bomb);
        }
    }

    private bool IsNearPlayer(Bomb bomb)
    {
        FighterJet player = (FighterJet)_gameObjects.Find(obj => obj is FighterJet);
        if (player == null) return false;

        Rectangle bombBounds = bomb.GetBounds();
        Rectangle playerBounds = player.GetBounds();

        // Check if the bomb is within a radius of 100 pixels from the player
        return Raylib.CheckCollisionRecs(bombBounds, playerBounds) ||
               (System.Math.Sqrt(System.Math.Pow(bombBounds.X + bombBounds.Width / 2 - (playerBounds.X + playerBounds.Width / 2), 2) +
                                 System.Math.Pow(bombBounds.Y + bombBounds.Height / 2 - (playerBounds.Y + playerBounds.Height / 2), 2)) < 100);
    }

    private void HandleInput()
    {
        foreach (GameObject item in _gameObjects)
        {
            item.HandleInput();
        }
    }

    private void ProcessActions()
    {
        foreach (GameObject item in _gameObjects)
        {
            item.ProcessActions();
        }
    }

    private void HandleCollisions()
    {
        // Use a temporary list to store objects to be removed
        List<GameObject> objectsToRemove = new List<GameObject>();

        for (int i = 0; i < _gameObjects.Count; i++)
        {
            for (int j = i + 1; j < _gameObjects.Count; j++)
            {
                if (_gameObjects[i].CollidesWith(_gameObjects[j]))
                {
                    GameObject obj1 = _gameObjects[i];
                    GameObject obj2 = _gameObjects[j];

                    // Handle bullet hitting an enemy fighter jet
                    if (obj1 is Bullet && obj2 is EnemyFighterJet)
                    {
                        objectsToRemove.Add(obj1); // Mark the bullet for removal
                        objectsToRemove.Add(obj2); // Mark the enemy fighter jet for removal
                        AddScore(10); // Add points for destroying an enemy
                        continue;
                    }
                    else if (obj2 is Bullet && obj1 is EnemyFighterJet)
                    {
                        objectsToRemove.Add(obj2); // Mark the bullet for removal
                        objectsToRemove.Add(obj1); // Mark the enemy fighter jet for removal
                        AddScore(10); // Add points for destroying an enemy
                        continue;
                    }

                    // Handle bomb hitting an enemy fighter jet
                    if (obj1 is Bomb && obj2 is EnemyFighterJet)
                    {
                        objectsToRemove.Add(obj2); // Mark the enemy fighter jet for removal
                        continue;
                    }
                    else if (obj2 is Bomb && obj1 is EnemyFighterJet)
                    {
                        objectsToRemove.Add(obj1); // Mark the enemy fighter jet for removal
                        continue;
                    }

                    // Prevent bombs from taking lives from the player's fighter jet
                    if ((obj1 is Bomb && obj2 is FighterJet) || (obj2 is Bomb && obj1 is FighterJet))
                    {
                        // Do nothing; bombs should not affect the player's fighter jet
                        continue;
                    }

                    // Handle enemy fighter jet hitting the player's fighter jet
                    if ((obj1 is EnemyFighterJet && obj2 is FighterJet) || (obj2 is EnemyFighterJet && obj1 is FighterJet))
                    {
                        LoseLife(); // Decrease player's life by 1
                        continue;
                    }

                    // Handle other collisions
                    obj1.HandleCollision(obj2);
                    obj2.HandleCollision(obj1);
                }
            }
        }

        // Remove all objects marked for removal
        foreach (GameObject obj in objectsToRemove)
        {
            _gameObjects.Remove(obj);
        }
    }

    private void DrawElements()
    {
        foreach (GameObject item in _gameObjects)
        {
            item.Draw();
        }
    }

    private void DrawHUD()
    {
        Raylib.DrawText($"Score: {_score}", 10, 10, 20, Color.Black);
        Raylib.DrawText($"Lives: {_lives}", 10, 40, 20, Color.Black);
    }

    private void DrawGameOver()
    {
        Raylib.DrawText("GAME OVER", SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2 - 20, 40, Color.Red);
        Raylib.DrawText($"Final Score: {_score}", SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2 + 30, 20, Color.Black);
    }

    public void AddScore(int points)
    {
        _score += points;
    }

    public void LoseLife()
    {
        _lives--;
        if (_lives <= 0)
        {
            Console.WriteLine("Game Over!"); // Optional: Add a log for debugging
        }
    }
}