# Cell Simulation App

This project simulates the life cycle of single-cell organisms in a simple 2D grid. The cells move, consume food, divide, and may die due to hunger, low health, or combat. The program is implemented in C# and provides a console-based visualization of the simulation.

## Features
- **Cells:** Each cell has unique attributes such as hunger, health, speed, vision, generation, and position.
- **Food:** Food items with varying values are randomly placed on the map.
- **Map:** A 2D grid-based map tracks the locations of cells and food.
- **Movement:** Cells move based on their vision and scan for food or other cells.
- **Eating:** Cells consume food to restore hunger and health.
- **Division:** When conditions are met, cells divide to create offspring with mutated attributes.
- **Combat:** If two cells occupy the same position, the weaker one dies.
- **Death:** Cells can die from hunger, low health, or combat.
- **Visualization:** The grid is displayed in the console, showing cells, food, and empty spaces.

## Classes and Their Responsibilities

### `Cell`
Represents a single-cell organism with attributes:
- **Name:** Unique identifier for the cell.
- **Hunger:** Decreases with movement, increases when eating food.
- **Health:** Decreases over time and due to combat; increases when eating food.
- **Speed:** Determines how much hunger is consumed per move.
- **Vision:** Determines the cell's range of sight to scan for food or other cells.
- **Generation:** Tracks the number of divisions the cell has undergone.
- **Position (X, Y):** Current location on the map.

#### Methods:
- `Move(int mapWidth, int mapHeight, int[,] FOV)`: Moves the cell based on its vision and returns its new position and whether it is dead.
- `Eat(int foodValue)`: Consumes food and may trigger division.
- `Division(Cell cell)`: Creates a new cell with mutated attributes if conditions are met.
- `Scan(int[,] FOV)`: Scans the cell's field of view for food and determines the best direction to move.

### `Food`
Represents food items on the map.
- **Position (X, Y):** Location on the map.
- **Value:** Amount of hunger and health restored when consumed.

### `Map`
Handles the 2D grid-based environment.
- **Map Data:** A 2D array tracks cells and food locations.
- **Dimensions (X, Y):** Width and height of the grid.

#### Methods:
- `Generate()`: Initializes the map with empty cells.
- `Write()`: Displays the map in the console.
- `PlaceCell(Cell cell)`: Places a cell on the map.
- `PlaceFood(Food food)`: Places food on the map.
- `MoveCell(Cell cell, int newX, int newY, List<Cell> allCells, List<(Cell cell, string cause)> deathCells)`: Moves a cell to a new position, handling combat and updating the map.
- `GetRadious(int x, int y, int radius)`: Retrieves the field of view around a specific position.
- `IsOccupied(int x, int y)`: Checks if a position is occupied by another cell or food.

### `Program`
The entry point of the application.

#### Responsibilities:
1. Initialize the map, cells, and food items.
2. Place cells and food on the map.
3. Simulate the movement, eating, and division of cells in a loop until all cells are dead.
4. Track and display information about dead cells and their causes of death.

#### Key Variables:
- `globalRandom`: A global random number generator.
- `w, h`: Width and height of the map.
- `map`: The `Map` instance.
- `cells`: List of living cells.
- `foods`: Dictionary of food items by their coordinates.
- `deathCells`: List of dead cells and their causes of death.

## Simulation Flow
1. **Initialization:**
   - The map is created.
   - A list of cells is initialized with random attributes and positions.
   - Food items are randomly placed on the map.

2. **Simulation Loop:**
   - Each cell moves based on its vision and attributes.
   - Cells consume food if available at their new position.
   - Cells may divide if they meet division criteria.
   - Combat occurs if two cells occupy the same position.
   - Cells die due to hunger, low health, or losing a fight.

3. **Termination:**
   - The loop ends when all cells are dead.
   - Information about dead cells and their causes of death is displayed.

## Visualization
- **Green:** Represents cells.
- **Red:** Represents food.
- **0:** Represents empty spaces.

## How to Run
1. Compile the program using a C# compiler (e.g., Visual Studio).
2. Run the application from the console.
3. Observe the simulation as it updates the console output.

## Future Enhancements
- Add more complex behavior for cells, such as cooperation or specialization.
- Introduce obstacles on the map.