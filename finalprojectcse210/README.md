# Fighter Jet Escape Game

## Overview
Fighter Jet Escape is a 2D arcade-style game where players control a fighter jet, navigating through enemy aircraft and avoiding bombs. The objective is to survive as long as possible while shooting down enemy jets and collecting points.

## Project Structure
The project consists of the following files:

- **Bomb.cs**: Defines the Bomb class, which represents bombs falling from the top of the screen. It includes methods for drawing, processing actions (falling), and handling collisions with the player's jet.

- **Enemy Fighter Jet.cs**: Defines the EnemyFighterJet class, which represents enemy jets moving towards the player. It includes methods for drawing, processing actions (moving towards the player), and handling collisions with the player's jet.

- **Fighter Jet.cs**: Defines the FighterJet class, which represents the player's jet. It allows the player to move up and down on the left side of the window and shoot projectiles. It includes methods for drawing and handling input.

- **Game Objects.cs**: Contains the abstract GameObject class, which serves as a base for all game objects. It defines properties for position, dimensions, color, and methods for drawing, handling input, processing actions, and collision detection.

- **GameManager.cs**: Contains the GameManager class, which manages the game loop, initializes game objects, handles input, processes actions, detects collisions, and draws the game elements and HUD. It also tracks the score and lives.

- **Program.cs**: Contains the entry point of the application. It creates an instance of the GameManager and starts the game loop.

## Gameplay Instructions
1. **Controls**:
   - Move the player's jet up and down using the arrow keys.
   - Shoot projectiles at enemy jets (implement shooting functionality in the FighterJet class).

2. **Objective**:
   - Avoid bombs falling from the top of the screen.
   - Shoot down enemy jets to earn points.
   - Survive as long as possible while managing your lives.

3. **Game Over**:
   - The game ends when the player's jet collides with a bomb or an enemy jet.

## Setup Instructions
1. Ensure you have the necessary dependencies installed, including Raylib for graphics.
2. Clone the repository or download the project files.
3. Open the project in your preferred C# development environment.
4. Build and run the project to start playing.

## Future Enhancements
- Implement a scoring system to track player performance.
- Add sound effects and background music.
- Introduce power-ups and different enemy types for varied gameplay.