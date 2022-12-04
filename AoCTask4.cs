using System;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace AocTasks
{
    public class AoCTask4
    {
        public void ResolveFirst()
        {
            string[] lines = ReadFile();

            int count = 0;
            foreach (string line in lines)
            {
                string[] pairs = line.Split(',');

                //we always have two pairs
                int[] firstPair = pairs[0].Split('-').Select(Int32.Parse).ToArray();
                int[] secondPair = pairs[1].Split('-').Select(Int32.Parse).ToArray();

                //check if second Pair contains first Pair
                if (secondPair[0] >= firstPair[0] && secondPair[1] <= firstPair[1])
                {
                    count++;
                    continue;
                }

                //check if first Pair contains second Pair
                if (firstPair[0] >= secondPair[0] && firstPair[1] <= secondPair[1])
                {
                    count++;
                }
            }

            Console.WriteLine("Fully covered pairs: " + count);
        }

        public void ResolveSecond()
        {
            string[] lines = ReadFile();

            int count = 0;

            foreach (string line in lines)
            {
                string[] pairs = line.Split(',');

                //we always have two pairs
                int[] firstPair = pairs[0].Split('-').Select(Int32.Parse).ToArray();
                int[] secondPair = pairs[1].Split('-').Select(Int32.Parse).ToArray();

                if (firstPair[0] >= secondPair[0] && firstPair[0] <= secondPair[1])
                {
                    count++;
                    continue;
                }
                else if (firstPair[1] <= secondPair[1] && firstPair[1] >= secondPair[0])
                {
                    count++;
                    continue;
                }

                if (secondPair[0] >= firstPair[0] && secondPair[0] <= firstPair[1])
                {
                    count++;
                    continue;
                }
                else if (secondPair[1] <= firstPair[1] && secondPair[1] >= firstPair[0])
                {
                    count++;
                    continue;
                }
            }
            Console.WriteLine("Pairs that overlaps: " + count);
        }

        private string[] ReadFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/elf_pairs.txt");
            return File.ReadAllLines(path);
        }
    }
}