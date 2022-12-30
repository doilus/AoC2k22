using System;
using System.Reflection;

namespace AocTasks
{
    public class AoCTask12
    {
        public void Resolve()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/hill_climbing.txt");
            string[] lines = File.ReadAllLines(path);

            int arrayHeight = lines.Length;
            int arrayWidth = lines[0].Length;

            int[] startPoint = new int[2]; //cordinates for startPoint x-0, y-1
            int[] endPoint = new int[2]; //cordinates for endPoint x-0, y-1
            int[,] map = new int[arrayHeight, arrayWidth];

            //find start and end point and set to map
            for (int i = 0; i < arrayHeight; i++)
            {
                for (int j = 0; j < arrayWidth; j++)
                {
                    if (lines[i][j] == 'S')
                    {
                        startPoint[0] = i;
                        startPoint[1] = j;
                        map[i, j] = 1;
                    }
                    else if (lines[i][j] == 'E')
                    {
                        endPoint[0] = i;
                        endPoint[1] = j;
                        map[i, j] = 26;
                    }
                    else
                    {
                        map[i, j] = lines[i][j] - 'a' + 1;
                    }
                }
            }

            //queue with only one starting point
            Queue<((int, int), int)> queue = new Queue<((int, int), int)>();
            queue.Enqueue(((startPoint[0], startPoint[1]), 0));
            Console.WriteLine("Number of steps for I part: " + FindShortestPath(arrayHeight, arrayWidth, map, endPoint, startPoint, queue));

            //queue with all 'a' as starting points
            queue = new Queue<((int, int), int)>();
            for (int x = 0; x < arrayHeight; x++)
            {
                for (int y = 0; y < arrayWidth; y++)
                    if (map[x, y] == 1)
                        queue.Enqueue(((x, y), 0));
            }
            Console.WriteLine("Number of steps for II part: " + FindShortestPath(arrayHeight, arrayWidth, map, endPoint, startPoint, queue));
        }

        public int FindShortestPath(int arrayHeight, int arrayWidth, int[,] map, int[] endPoint, int[] startPoint, Queue<((int, int), int)> queue)
        {
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            List<(int x, int y)> closestPoints = new List<(int x, int y)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };

            while (queue.Any())
            {
                ((int x, int y), int steps) = queue.Dequeue();

                //checks if value can be added
                if (!visited.Add((x, y)))
                    continue;

                if (x == endPoint[0] && y == endPoint[1])
                {
                    return steps;
                }

                foreach ((int dx, int dy) in closestPoints)
                {
                    int positionX = x + dx;
                    int positionY = y + dy;

                    if ((positionX >= 0 && positionX < arrayHeight) && (positionY >= 0 && positionY < arrayWidth))
                    {
                        var parentNode = map[x, y];
                        var childNode = map[positionX, positionY];

                        if (childNode - parentNode <= 1)
                            queue.Enqueue(((positionX, positionY), steps + 1));
                    }
                }
            }
            return 0;
        }
    }

}