
using System.Reflection;

namespace AocTasks
{
    public class AoCTask3
    {
        //ASCII counterparts for letters and points
        readonly Dictionary<string, int> SmallLetters = new Dictionary<string, int>(){
            {"Start", 97},
            {"End", 122},
            {"Minus", 96}, //this value needs to be substract from ascii value
        };

        readonly Dictionary<string, int> BigLetters = new Dictionary<string, int>(){
            {"Start", 65},
            {"End", 90},
            {"Minus", 38},
        };

        public void Resolve()
        {
            string[] lines = ReadFile();

            int points = 0;

            foreach (string line in lines)
            {
                List<char> sameCharacters = new List<char>();
                int medium = line.Length / 2;

                for (int i = 0; i < medium; i++)
                {
                    for (int j = medium; j < line.Length; j++)
                    {
                        if (line[i] == line[j] && !sameCharacters.Contains(line[i]))
                        {
                            sameCharacters.Add(line[i]);
                        }
                    }
                }

                //count points for same characters
                foreach (char character in sameCharacters)
                {
                    points += CountPoints((int)character);
                }
            }
            Console.WriteLine("Points for first part: " + points);
        }

        public void ResolveSecond()
        {
            string[] lines = ReadFile();

            int points = 0;

            int countLines = 0;

            List<string> elfRuckSacks = new List<String>();
            Dictionary<char, bool> sameCharacters = new Dictionary<char, bool>();

            foreach (string line in lines)
            {
                if (countLines == 2)
                {
                    elfRuckSacks.Add(line);

                    string first = elfRuckSacks[0];
                    string second = elfRuckSacks[1];
                    string third = elfRuckSacks[2];

                    for (int i = 0; i < first.Length; i++)
                    {
                        for (int j = 0; j < second.Length; j++)
                        {
                            if (first[i] == second[j] && !sameCharacters.ContainsKey(first[i]))
                            {
                                sameCharacters.Add(first[i], false);
                            }
                        }
                    }

                    foreach (KeyValuePair<char,bool> valuePair in sameCharacters)
                    {
                        for (int i = 0; i < third.Length; i++)
                        {
                            if (valuePair.Key == third[i])
                            {
                                sameCharacters[valuePair.Key] = true;
                            }
                        }
                    }

                    //count points
                    foreach (KeyValuePair<char, bool> valuePair in sameCharacters)
                    {
                        if (!valuePair.Value) continue;
                        points += CountPoints((int)valuePair.Key);
                    }

                    //reset
                    countLines = 0;
                    elfRuckSacks = new List<String>();
                    sameCharacters = new Dictionary<char, bool>();
                }
                else
                {
                    countLines++;
                    elfRuckSacks.Add(line);
                }
               
            }
            Console.WriteLine("Points for second task: " + points);
        }

        private string[] ReadFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/rucksack_compart.txt");

            return File.ReadAllLines(path);
        }

        private int CountPoints(int charValue)
        {
            //for 97 - 122 ascii small letters
            if (charValue >= SmallLetters["Start"] && charValue <= SmallLetters["End"])
            {
                return charValue - SmallLetters["Minus"];
            }

            //for 65 - 90 ascii small letters
            if (charValue >= BigLetters["Start"] && charValue <= BigLetters["End"])
            {
                return charValue - BigLetters["Minus"];
            }

            return 0;
        }
    }
}