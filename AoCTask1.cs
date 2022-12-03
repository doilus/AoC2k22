using System.Reflection;

namespace AocTasks
{
    public class AoCTask1
    {
        public void Resolve()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/elf_calories.txt");

            string[] lines = File.ReadAllLines(path);

            //Comments - it was only part I
            //int max = 0;
            int value = 0;

            List<int> elfCalories = new List<int>();


            foreach (string line in lines)
            {
                if (line == "")
                {
                    elfCalories.Add(value);

                    //if (value > max)
                    //{
                    //    max = value;
                    //}

                    value = 0;

                }
                else
                {
                    value = value + Int32.Parse(line);
                }
            }

            elfCalories.Add(value);

            //if (value > max)
            //{
            //    max = value;
            //}

            Console.WriteLine("Max value: " + elfCalories.Max());

            int maxThree = 0;

            for (int i = 0; i < 3; i++)
            {
                int maxFromList = elfCalories.Max();
                maxThree += maxFromList;
                elfCalories.Remove(maxFromList);
            }

            Console.Write("Max three combined: " + maxThree + "\n");
        }
    }
}
