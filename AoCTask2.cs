using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace AocTasks
{
    public class AoCTask2
    {
        const int WIN = 6;
        const int DRAW = 3;
        const int LOSE = 0;

        readonly Dictionary<char, char> Counterparts = new Dictionary<char, char>(){
            {'A', 'X'},
            {'B', 'Y'},
            {'C', 'Z'},
        };

        readonly Dictionary<char, int> Points = new Dictionary<char, int>(){
            {'X', 1},
            {'Y', 2},
            {'Z', 3},
        };

        readonly Dictionary<char, char> Beats = new Dictionary<char, char>(){
            {'A', 'Y'},
            {'B', 'Z'},
            {'C', 'X'},
        };

        readonly Dictionary<char, char> Loses = new Dictionary<char, char>(){
            {'A', 'Z'},
            {'B', 'X'},
            {'C', 'Y'},
        };

        public void Resolve()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/strategy_guide.txt");
            string[] lines = File.ReadAllLines(path);

            int winPoints = 0;

            foreach (string line in lines)
            {
                char scoreElf = line[0];
                char result = line[2];
                char scoreMe;

                switch (result)
                {
                    //if I need to lose
                    case 'X':
                        scoreMe = Loses[scoreElf];
                        break;
                    //if I need to draw
                    case 'Y':
                        scoreMe = Counterparts[scoreElf];
                        break;
                    //if I need to win
                    case 'Z':
                        scoreMe = Beats[scoreElf];
                        break;
                    default:
                        throw new Exception("Elf score not found!");
                }

                //check if I won
                if (scoreMe == Beats[scoreElf])
                {
                    winPoints += WIN + Points[scoreMe];
                    continue;
                }

                //check draw
                if (Counterparts[scoreElf] == scoreMe)
                {
                    winPoints += DRAW + Points[scoreMe];
                    continue;
                }

                //i lost
                winPoints += LOSE + Points[scoreMe];
            }

            Console.WriteLine("Your win points: " + winPoints);

        }
    }
}