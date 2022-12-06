using System;
using System.Linq;
using System.Reflection;

namespace AocTasks
{
	public class AoCTask6
	{
		const int DIST = 14; //Change to 4 for first part
        public void Resolve()
		{
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/datastream_buffer.txt");
            string datastream = File.ReadAllText(path);

			List<char> memory = new List<char>();

			for(int i=0; i< datastream.Length; i++)
			{
				if(memory.Count < DIST)
				{
					memory.Add(datastream[i]);
				}

				if(memory.Count >= DIST) 
                {
                    var allUnique = memory.GroupBy(x => x).All(g => g.Count() == 1);

					if (allUnique)
					{
						Console.WriteLine("Unique for: " + ++i);
						break;
					}
					memory.RemoveAt(0);
                }
			}
        }
	}
}

