using System.Reflection;

public class AoCTask2
{
    public void resolve()
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/strategy_guide.txt");

        string[] lines = File.ReadAllLines(path);

        const int WIN = 6;
        const int DRAW = 3;
        const int LOSE = 0;

        Dictionary<char, char> counterparts = new Dictionary<char, char>(){
            {'A', 'X'},
            {'B', 'Y'},
            {'C', 'Z'},
        };

        Dictionary<char, int> points = new Dictionary<char, int>(){
            {'X', 1},
            {'Y', 2},
            {'Z', 3},
        };

        Dictionary<char, char> beats = new Dictionary<char, char>(){
            {'A', 'Y'},
            {'B', 'Z'},
            {'C', 'X'},
        };

        Dictionary<char, char> loses = new Dictionary<char, char>(){
            {'A', 'Z'},
            {'B', 'X'},
            {'C', 'Y'},
        };

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
                    scoreMe = loses[scoreElf];
                    break;
                //if I need to draw
                case 'Y':
                    scoreMe = counterparts[scoreElf];
                    break;
                //if I need to win
                case 'Z':
                    scoreMe = beats[scoreElf];
                    break;
                default:
                    throw new Exception("Elf score not found!");
            }

            //check if I won
            if (scoreMe == beats[scoreElf])
            {
                winPoints += WIN + points[scoreMe];
                continue;
            }

            //check draw
            if (counterparts[scoreElf] == scoreMe)
            {
                winPoints += DRAW + points[scoreMe];
                continue;
            }

            //i lost
            winPoints += LOSE + points[scoreMe];
        }

        Console.WriteLine("Your win points: " + winPoints);

    }
}