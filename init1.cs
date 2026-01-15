
namespace ProceduralGen
{
    public static class MapGenerator
    {
        static int type;
        static string tileSet;
        static void Tile()
        {
            // Tile properties can be defined here
            // e.g., isLand, isWater, isShore, etc.
            

            switch (type)
            {
                //each tile type corresponds to a specific tileset
                //tile set is a 3x3 grid of characters

                case 0:
                    tileSet = "..." +
                              "..." +
                              "...";
                    break;
                case 1:
                    tileSet = "###" +
                              "###" +
                              "###";
                    break;
                case 2:
                    tileSet = "+++" +
                              "+++" +
                              "+++";
                    break;
                case 3:
                    tileSet = "..+" +
                              ".++" +
                              "+++";
                    break;
                case 4:
                    tileSet = "+.." +
                              "++." +
                              "+++";
                    break;
                case 5:
                    tileSet = "+++" +
                              ".++" +
                              "..+";
                    break;
                case 6:
                    tileSet = "+++" +
                              "++." +
                              "+..";
                    break;
                default:
                    tileSet = "..." +
                              "..." +
                              "...";
                    break;

            }

        }
        public static void Initialize()
        {
            // Initialization logic for the map generator
            // 2d array of tiles, each tile can be true (land) or false(water)
            // Randomly generate a map with land and water tiles
            // count the neighbor lands of each tile
            // If a tile has more than 4 land neighbors, it becomes land
            // If a tile has less than 4 land neighbors, it becomes water
            // If a tile has both land and water neighbors, it becomes shore
            // Repeat this process for a number of iterations to smooth the map
            // 1 stand as land
            // 0 stand as water
            // 2 stand as shore (land tile with at least one water neighbor)
            // create a 2d string array to hold the tilesets
            // use the tile function to assign tilesets based on tile type
            // Print the final map to the console
            int width = 50;
            int height = 50;
            bool[,] map = new bool[width, height];

            Random rand = new Random();
            // Initial random fill
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = rand.NextDouble() < 0.45;
                }
            }
            // Smoothing iterations
            for (int iteration = 0; iteration < 5; iteration++)
            {
                bool[,] newMap = new bool[width, height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int landNeighbors = 0;
                        for (int ny = -1; ny <= 1; ny++)
                        {
                            for (int nx = -1; nx <= 1; nx++)
                            {
                                if (nx == 0 && ny == 0) continue;
                                int checkX = x + nx;
                                int checkY = y + ny;
                                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                                {
                                    if (map[checkX, checkY]) landNeighbors++;
                                }
                                else
                                {
                                    landNeighbors++; // Treat out-of-bounds as land
                                }
                            }
                        }
                        if (landNeighbors > 4)
                            newMap[x, y] = true;
                        else if (landNeighbors < 4)
                            newMap[x, y] = false;
                        else
                            newMap[x, y] = map[x, y];
                    }
                }
                map = newMap;
            }
            // Print the map with tilesets
            // each tile is a 3x3 grid of characters
            // shore tiles are land tiles with at least one water neighbor
            for (int y = 0; y < height; y++)
            {
                for (int row = 0; row < 3; row++) // each tile has 3 rows
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Determine tile type
                        if (map[x, y])
                        {
                            // Check for shore
                            bool isShore = false;
                            for (int ny = -1; ny <= 1; ny++)
                            {
                                for (int nx = -1; nx <= 1; nx++)
                                {
                                    if (nx == 0 && ny == 0) continue;
                                    int checkX = x + nx;
                                    int checkY = y + ny;
                                    if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                                    {
                                        if (!map[checkX, checkY])
                                        {
                                            isShore = true;
                                            break;
                                        }
                                    }
                                }
                                if (isShore) break;
                            }
                            type = isShore ? 2 : 1;
                        }
                        else
                        {
                            type = 0;
                        }
                        Tile();
                        // Print the corresponding row of the tileset
                        Console.Write(tileSet.Substring(row * 3, 3));
                    }
                    Console.WriteLine();
                }
            }
            // Print small scale map for reference
            // Print the map
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map[x, y])
                    {
                        // Check for shore
                        bool isShore = false;
                        for (int ny = -1; ny <= 1; ny++)
                        {
                            for (int nx = -1; nx <= 1; nx++)
                            {
                                if (nx == 0 && ny == 0) continue;
                                int checkX = x + nx;
                                int checkY = y + ny;
                                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                                {
                                    if (!map[checkX, checkY])
                                    {
                                        isShore = true;
                                        break;
                                    }
                                }
                            }
                            if (isShore) break;
                        }
                        if (isShore)
                            System.Console.Write("+");
                        else
                            System.Console.Write("#");
                    }
                    else
                    {
                        System.Console.Write(".");
                    }
                }
                System.Console.WriteLine();
            }
        }

    }
}
/*
 public static void Initialize()
        {
            // Initialization logic for the map generator
            // 2d array of tiles, each tile can be true (land) or false(water)
            // Randomly generate a map with land and water tiles
            // count the neighbor lands of each tile
            // If a tile has more than 4 land neighbors, it becomes land
            // If a tile has less than 4 land neighbors, it becomes water
            // If a tile has both land and water neighbors, it becomes shore
            // Repeat this process for a number of iterations to smooth the map
            // 1 stand as land
            // 0 stand as water
            // 2 stand as shore (land tile with at least one water neighbor)
            // use the tile function to assign tilesets based on tile type
            // Print the final map to the console

            int width = 50;
            int height = 20;
            bool[,] map = new bool[width, height];
            Random rand = new Random();
            // Initial random fill
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = rand.NextDouble() < 0.45;
                }
            }
            // Smoothing iterations
            for (int iteration = 0; iteration < 5; iteration++)
            {
                bool[,] newMap = new bool[width, height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int landNeighbors = 0;
                        for (int ny = -1; ny <= 1; ny++)
                        {
                            for (int nx = -1; nx <= 1; nx++)
                            {
                                if (nx == 0 && ny == 0) continue;
                                int checkX = x + nx;
                                int checkY = y + ny;
                                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                                {
                                    if (map[checkX, checkY]) landNeighbors++;
                                }
                                else
                                {
                                    landNeighbors++; // Treat out-of-bounds as land
                                }
                            }
                        }
                        if (landNeighbors > 4)
                            newMap[x, y] = true;
                        else if (landNeighbors < 4)
                            newMap[x, y] = false;
                        else
                            newMap[x, y] = map[x, y];
                    }
                }
                map = newMap;
            }
            // Print the map
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map[x, y])
                    {
                        // Check for shore
                        bool isShore = false;
                        for (int ny = -1; ny <= 1; ny++)
                        {
                            for (int nx = -1; nx <= 1; nx++)
                            {
                                if (nx == 0 && ny == 0) continue;
                                int checkX = x + nx;
                                int checkY = y + ny;
                                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                                {
                                    if (!map[checkX, checkY])
                                    {
                                        isShore = true;
                                        break;
                                    }
                                }
                            }
                            if (isShore) break;
                        }
                        if (isShore)
                            System.Console.Write("+");
                        else
                            System.Console.Write("#");
                    }
                    else
                    {
                        System.Console.Write(".");
                    }
                }
                System.Console.WriteLine();
            }
        }
 */
//
//namespace ProceduralGen
//{
//    public static class MapGenerator
//    {
//        public static void Initialize()
//        {
//            // Initialization logic for the map generator
//            // 2d array of tiles, each tile can be true (land) or false(water)
//            // Randomly generate a map with land and water tiles
//            // count the neighbor lands of each tile
//            // If a tile has more than 4 land neighbors, it becomes land
//            // If a tile has less than 4 land neighbors, it becomes water
//            // Repeat this process for a number of iterations to smooth the map
//            // # stand as land
//            // . stand as water

//            int width = 50;
//            int height = 20;
//            bool[,] map = new bool[width, height];
//            Random rand = new Random();
//            // Randomly initialize the map
//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    map[x, y] = rand.NextDouble() < 0.45;
//                }
//            }
//            // Smooth the map
//            for (int i = 0; i < 5; i++)
//            {
//                bool[,] newMap = new bool[width, height];
//                for (int x = 0; x < width; x++)
//                {
//                    for (int y = 0; y < height; y++)
//                    {
//                        int landNeighbors = 0;
//                        for (int nx = -1; nx <= 1; nx++)
//                        {
//                            for (int ny = -1; ny <= 1; ny++)
//                            {
//                                if (nx == 0 && ny == 0) continue;
//                                int checkX = x + nx;
//                                int checkY = y + ny;
//                                if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
//                                {
//                                    if (map[checkX, checkY]) landNeighbors++;
//                                }
//                                else
//                                {
//                                    landNeighbors++; // Treat out-of-bounds as land
//                                }
//                            }
//                        }
//                        if (landNeighbors > 4)
//                            newMap[x, y] = true;
//                        else if (landNeighbors < 4)
//                            newMap[x, y] = false;
//                        else
//                            newMap[x, y] = map[x, y];
//                    }
//                }
//                map = newMap;
//            }
//            // Print the map
//            for (int y = 0; y < height; y++)
//            {
//                for (int x = 0; x < width; x++)
//                {
//                    System.Console.Write(map[x, y] ? '#' : '.');
//                }
//                System.Console.WriteLine();
//            }
//        }
//    }
//}

