using System;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace AocTasks
{
    public class AoCTask8
    {
        public void Resolve()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/grid_trees.txt");
            string[] lines = File.ReadAllLines(path);

            int linesX = lines[0].Length;
            int linesY = lines.Length;

            int count = linesX * 2 + linesY * 2 - 4;
            int scenicScoreMax = 0;
            for (int i = 1; i < linesY - 1; i++)
            {
                int scenicScore = 0;
                for (int j = 1; j < linesX - 1; j++)
                {
                    int currentTree = lines[i][j] - '0';
                    bool isVisible1 = true;
                    bool isVisible2 = true;
                    bool isVisible3 = true;
                    bool isVisible4 = true;

                    int score1 = 0;
                    int score2 = 0;
                    int score3 = 0;
                    int score4 = 0;

                    for (int x = j + 1; x < linesX; x++)
                    {
                        if (currentTree <= Char.GetNumericValue(lines[i][x]))
                        {
                            isVisible1 = false;
                            score1 = x - j;
                            break;
                        }
                    }

                    for (int x = j - 1; x >= 0; x--)
                    {
                        if (currentTree <= Char.GetNumericValue(lines[i][x]))
                        {
                            isVisible2 = false;
                            score2 = j - x;
                            break;
                        }
                    }

                    for (int y = i + 1; y < linesY; y++)
                    {
                        if (currentTree <= Char.GetNumericValue(lines[y][j]))
                        {
                            isVisible3 = false;
                            score3 = y - i;
                            break;
                        }
                    }

                    for (int y = i - 1; y >= 0; y--)
                    {
                        if (currentTree <= Char.GetNumericValue(lines[y][j]))
                        {
                            isVisible4 = false;
                            score4 = i - y;
                            break;
                        }
                    }

                    if (isVisible1 || isVisible2 || isVisible3 || isVisible4)
                    {
                        count++;
                    }

                    if (!(isVisible1 && isVisible2 && isVisible3 && isVisible4))
                    {
                        if (score1 == 0) score1 = linesX - j - 1;
                        if (score2 == 0) score2 = 0 + j;
                        if (score3 == 0) score3 = linesY - i - 1;
                        if (score4 == 0) score4 = 0 + i;

                        scenicScore = score1 * score2 * score3 * score4;
                        if (scenicScore > scenicScoreMax) scenicScoreMax = scenicScore;
                    }
                }

            }
            Console.WriteLine("Is visible for: " + count);
            Console.WriteLine("Highest scenic Score: " + scenicScoreMax);
        }
    }
}

