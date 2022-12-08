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
                    bool[] isVisible = new bool[] { true, true, true, true };

                    int[] score = new int[] { 0, 0, 0, 0 };

                    int score1 = 0;
                    int score2 = 0;
                    int score3 = 0;
                    int score4 = 0;

                    for (int x = j + 1; x < linesX; x++)
                    {
                        if (currentTree <= Char.GetNumericValue(lines[i][x]))
                        {
                            isVisible[0] = false;
                            score[0] = x - j;
                            break;
                        }
                    }

                    for (int x = j - 1; x >= 0; x--)
                    {
                        if (currentTree <= Char.GetNumericValue(lines[i][x]))
                        {
                            isVisible[1] = false;
                            score[1] = j - x;
                            break;
                        }
                    }

                    for (int y = i + 1; y < linesY; y++)
                    {
                        if (currentTree <= Char.GetNumericValue(lines[y][j]))
                        {
                            isVisible[2] = false;
                            score[2] = y - i;
                            break;
                        }
                    }

                    for (int y = i - 1; y >= 0; y--)
                    {
                        if (currentTree <= Char.GetNumericValue(lines[y][j]))
                        {
                            isVisible[3] = false;
                            score[3] = i - y;
                            break;
                        }
                    }

                    if ( isVisible.Any(c => c == true))
                    {
                        count++;
                    }

                    if (!isVisible.All(c => c == true))
                    {
                        if (score[0] == 0) score[0] = linesX - j - 1;
                        if (score[1] == 0) score[1] = 0 + j;
                        if (score[2] == 0) score[2] = linesY - i - 1;
                        if (score[3] == 0) score[3] = 0 + i;

                        scenicScore = score.Aggregate(1, (a,b) => a * b);
                        if (scenicScore > scenicScoreMax) scenicScoreMax = scenicScore;
                    }
                }

            }
            Console.WriteLine("Is visible for: " + count);
            Console.WriteLine("Highest scenic Score: " + scenicScoreMax);
        }
    }
}

