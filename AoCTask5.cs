using System;
using System.Reflection;

namespace AocTasks
{
	public class AoCTask5
	{
        public void Resolve()
        {
            string lines = ReadFile();

            string[] records = lines.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<int, List<char>> stacks = PrepareStacks(records[0]);

            string[] moves = records[1].Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in moves)
            {
                //FIX THIS - GET ONLY NUMBERS
                int[] numbers = line.Split(' ').Select(p =>
                {
                    var numeric = int.TryParse(p, out int n);
                    return n;
                }).ToArray();

                int move = numbers[1];
                int from = numbers[3];
                int to = numbers[5];

                var elementsFromStack = stacks[from].Take(move);
                stacks[to].InsertRange(0, elementsFromStack.Reverse());
                //stacks[to].InsertRange(0, elementsFromStack); //Uncomment this to have solution for second
                stacks[from].RemoveRange(0, move);
            }

            Console.Write("Top of the stack: ");

            for (int i = 1; i <= stacks.Count; i++)
            {
                Console.Write(stacks[i].First());
            }
        }

        private string ReadFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/supply_stacks.txt");
            return File.ReadAllText(path);
        }

        private Dictionary<int, List<char>> PrepareStacks(string stacksOfCrates)
        {
            Dictionary<int, List<char>> stacks =  new Dictionary<int, List<char>>();

            int count = 0;
            int current = 1;

            foreach (char c in stacksOfCrates)
            {
                if (count >= 4)
                {
                    count = 0;
                    current++;
                }

                if (c.Equals('\n'))
                {
                    count = 0;
                    current = 1;
                    continue;
                }

                if (c.Equals('[') || c.Equals(']') || Char.IsWhiteSpace(c))
                {
                    count++;
                    continue;
                }


                if (Char.IsLetter(c))
                {
                    if (!stacks.ContainsKey(current)) stacks.Add(current, new List<char>());
                    stacks[current].Add(c);
                    count++;
                }
                else if (Char.IsNumber(c))
                {
                    break;
                }
            }

            return stacks;
        }

    }
}

