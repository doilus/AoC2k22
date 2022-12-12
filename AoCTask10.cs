using System;
using System.Reflection;

namespace AocTasks
{
	public class AoCTask10
	{
        List<string> queue = new List<string>();
        int x = 1;
        int[] cyclesCount = { 20, 60, 100, 140, 180, 220 };
        char[] spritePosition = new char[40];
        char[] canvas = new char[240];

        public void Resolve()
		{
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/cathode-ray-tube.txt");
            string[] cycles = File.ReadAllLines(path);

			int cycle = 1;
            int count = 0;

            //set sprite
            spritePosition[0] = '#';
            spritePosition[1] = '#';
            spritePosition[2] = '#';

            foreach (string value in cycles)
			{
                if (value == "noop")
				{
                    //check canvas
                    CheckCanvas(cycle);

                    //do sth from the loop if empty break
                    ExecuteAddx();

                    if (cyclesCount.Contains(cycle))
                    {
                        count += cycle * x;
                    }

                    cycle++;
                    //check canvas
                    CheckCanvas(cycle);
                    continue;
				}

                //check canvas
                CheckCanvas(cycle);

                ExecuteAddx();
                queue.Add(value);

                //check canvas
                CheckCanvas(cycle);

                count += AddToSum(cycle);
                cycle++;

                //check canvas
                CheckCanvas(cycle);

                count += AddToSum(cycle);
                ExecuteAddx();
                cycle++;

                //check canvas
                CheckCanvas(cycle);
            }

            //last time
            ExecuteAddx();
            //check canvas
            CheckCanvas(cycle);

            Console.WriteLine("Get value: " + count);

            int helper = 0;
            foreach (char c in canvas)
            {
                if (helper > 39)
                {
                    Console.WriteLine();
                    helper = 0;
                }

                Console.Write(c);
                helper++;
            }
        }

		public void ExecuteAddx()
		{
            var addsInLine = queue.FirstOrDefault();
            //spr czy coś istnieje w kolejce
            if (addsInLine != null)
            {
                string[] adds = addsInLine.Split(' ');
                this.x = this.x + Int32.Parse(adds[1]);
                queue.Remove(addsInLine);

                //set new sprite
                spritePosition = new char[40];

                if(this.x >= 1 && this.x <= 38)
                {
                    spritePosition[this.x - 1] = '#';
                    spritePosition[this.x] = '#';
                    spritePosition[this.x + 1] = '#';
                }
            }
        }

        public int AddToSum(int cycle)
        {
            if (cyclesCount.Contains(cycle))
            {
                return cycle * x;
            }
            return 0; 
        }

        public void CheckCanvas(int cycle)
        {
            int cycleHelper = cycle;

            if(cycle == 41 || cycle == 81 || cycle == 121 || cycle == 161 || cycle == 181 || cycle == 221)
            {
                //reset sprite
                spritePosition[0] = '#';
                spritePosition[1] = '#';
                spritePosition[2] = '#';
            }

            if (cycle > 40)
            {
                while (cycleHelper >= 40)
                {
                    cycleHelper -= 40;
                }
            }
            int positionCanvas = cycle - 1;

            if (positionCanvas >= 240) return;

            if (cycleHelper == 0) cycleHelper = 40;

            if (cycleHelper > 0) cycleHelper -= 1;

            //check canvas
            if (spritePosition[cycleHelper] == '#')
            {
                this.canvas[positionCanvas] = '#';
            }
            else
            {
                this.canvas[positionCanvas] = '.';
            }
        }
	}
}

