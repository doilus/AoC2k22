using System;
using System.Linq;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace AocTasks
{
	public class AoCTask11
	{
		const string START_ITEMS = "Starting items: ";
        const string OPERATION = "Operation: new = ";
        const string TEST = "Test: divisible by ";
        const string TRUE = "If true: throw to monkey ";
        const string FALSE = "If false: throw to monkey ";

        readonly string[] OPERATIONS = { "old +", "old -", "old *", "old /" };


        //change this values to have first result
        const int ROUNDS = 10000; //20
        const int DIVIDER = 1; //3 

        public void Resolve()
		{
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/monkey_throws.txt");
            string[] lines = File.ReadAllLines(path);

			Dictionary<int, Queue<long>> monkeyThrows = new Dictionary<int, Queue<long>>();
            Dictionary<int, string> monkeyOperation = new Dictionary<int, string>();
            Dictionary<int, List<int>> monkeyLogic = new Dictionary<int, List<int>>(); //first - divided, second - true, third - false

            Dictionary<int, int> monkeyInspects = new Dictionary<int, int>();

            int monkeyCount = 0;
            int maxValue = 1;

            //read data
            foreach (string line in lines)
			{
				//check if empty line
				if (string.IsNullOrWhiteSpace(line))
				{
                    monkeyCount++;
                    continue;
                }

				//for starting items:
				if(line.Contains(START_ITEMS))
				{
                    string items = line.Substring(START_ITEMS.Length + 1);
                    long[] arr = items.Split(',').Select(long.Parse).ToArray<long>();
                    monkeyThrows[monkeyCount] = new Queue<long>();
                    foreach(long a in arr)
                    {
                        monkeyThrows[monkeyCount].Enqueue(a);
                    }
                    continue;
				}

                if (line.Contains(OPERATION))
                {
                    string operation = line.Substring(OPERATION.Length + 1);
					monkeyOperation[monkeyCount] = operation;
                    continue;
                }

                if (line.Contains(TEST))
                {
                    string divided = line.Substring(TEST.Length + 1);
                    monkeyLogic[monkeyCount] = new List<int>();
                    monkeyLogic[monkeyCount].Add(Int32.Parse(divided));
                    continue;
                }

                if (line.Contains(TRUE))
                {
                    string value = line.Substring(TRUE.Length + 3);
                    monkeyLogic[monkeyCount].Add(Int32.Parse(value));
                    continue;
                }

                if (line.Contains(FALSE))
                {
                    string value = line.Substring(FALSE.Length + 3);
                    monkeyLogic[monkeyCount].Add(Int32.Parse(value));
                    continue;
                }
               
            }

            //get max value for dividers
            foreach(KeyValuePair<int, List<int>> value in monkeyLogic)
            {
                maxValue *= value.Value.First();
            }


            for (int j = 0; j < monkeyThrows.Count; j++)
            {
                monkeyInspects[j] = 0;
            }


            for (int i = 0; i < ROUNDS; i++)
            {
                foreach (KeyValuePair<int, Queue<long>> monkeyThrow in monkeyThrows)
                {
                    while(monkeyThrow.Value.Count > 0)
                    {
                        monkeyInspects[monkeyThrow.Key]++;
                        long investigatedValue = parseOperation(monkeyOperation[monkeyThrow.Key], monkeyThrow.Value.Dequeue())/DIVIDER;

                        if(DIVIDER == 1) investigatedValue = investigatedValue % maxValue;

                        bool isDivided = investigatedValue % monkeyLogic[monkeyThrow.Key][0] == 0;

                        int monkeyToThrow = ThrowToMonkey(isDivided, monkeyLogic[monkeyThrow.Key]);

                        monkeyThrows[monkeyToThrow].Enqueue(investigatedValue);
                    }
                    
                }
            }

            long max = monkeyInspects.Values.Max();
            long secondMax = monkeyInspects.Values.Where(x => x < max).Max();

            Console.WriteLine("Level of monkey business: " + max * secondMax);
        }

        public long parseOperation(string operation, long item)
        {
            if (operation.Contains(OPERATIONS[0]))
            {
                if(Int32.TryParse(operation.Substring(OPERATIONS[0].Length + 1), out int number))
                {
                    return item + number;
                }
                else
                {
                    return item*2;
                }

            }

            if (operation.Contains(OPERATIONS[1]))
            {
                if (Int32.TryParse(operation.Substring(OPERATIONS[1].Length + 1), out int number))
                {
                    return item - number;
                }
                else
                {
                    return 0;
                }

            }
            if (operation.Contains(OPERATIONS[2]))
            {
                
                if (Int32.TryParse(operation.Substring(OPERATIONS[2].Length + 1), out int number))
                {
                    return item * number;
                }
                else
                {
                    return item*item;
                }

            }
            if (operation.Contains(OPERATIONS[3]))
            {
                if (Int32.TryParse(operation.Substring(OPERATIONS[3].Length + 1), out int number))
                {
                    return item / number;
                }
                else
                {
                    return 1;
                }

            }
            return 0;
        }

        public int ThrowToMonkey(bool isDivided, List<int> monkeyLogic)
        {
            if(isDivided)
            {
                return monkeyLogic[1];
            } else
            {
                return monkeyLogic[2];
            }
        }
	}
}

