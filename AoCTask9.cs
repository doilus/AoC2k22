using System;
using System.Reflection;

namespace AocTasks
{
	public class AoCTask9
	{
		//moves
		const string RIGHT = "R";
        const string LEFT = "L";
        const string DOWN = "D";
        const string UP = "U";

        const int START_POINT = 0;

        public void ResolveFirst()
		{
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/bridge_moves.txt");
            string[] moves = File.ReadAllLines(path);

            Dictionary<Points, int> mapValues = new Dictionary<Points, int>();
            Points headPosition = new Points(0,0);

            Points tailPosition = new Points(0,0);

            mapValues.Add(new Points(0, 0), 0);

            foreach (string move in moves)
            {
                string[] value = move.Split(' ');
                // value[0] - direction
                // value[1] - count

                for (int i = 0; i < Int32.Parse(value[1]); i++)
                {
                    if (value[0] == RIGHT) headPosition.X++;

                    if (value[0] == LEFT) headPosition.X--;

                    if (value[0] == UP) headPosition.Y++;

                    if (value[0] == DOWN) headPosition.Y--;

                    
                    //position of Y
                    //if touching (1. x near, 2. y near, 3. overlap)
                    if (tailPosition.X >= headPosition.X - 1 && tailPosition.X <= headPosition.X + 1
                        && tailPosition.Y >= headPosition.Y - 1 && tailPosition.Y <= headPosition.Y + 1)
                    {
                        continue;
                    }

                    //if in the same x line
                    if (tailPosition.X == headPosition.X)
                    {
                        if (tailPosition.Y < headPosition.Y)
                        {
                            tailPosition.Y++;
                        }
                        else
                        {
                            tailPosition.Y--;
                        }
                    }
                    //if in the same y line
                    else if (tailPosition.Y == headPosition.Y)
                    {
                        if (tailPosition.X < headPosition.X)
                        {
                            tailPosition.X++;
                        }
                        else
                        {
                            tailPosition.X--;
                        }
                    }
                    //if are totally differnt places 
                    else
                    {
                        if (headPosition.X > tailPosition.X && headPosition.Y > tailPosition.Y)
                        {
                            tailPosition.X++;
                            tailPosition.Y++;
                        }
                        else if (headPosition.X > tailPosition.X && headPosition.Y < tailPosition.Y)
                        {
                            tailPosition.X++;
                            tailPosition.Y--;
                        }
                        else if (headPosition.X < tailPosition.X && headPosition.Y > tailPosition.Y)
                        {
                            tailPosition.X--;
                            tailPosition.Y++;
                        }
                        else if (headPosition.X < tailPosition.X && headPosition.Y < tailPosition.Y)
                        {
                            tailPosition.X--;
                            tailPosition.Y--;
                        }
                    }

                    //add new tailPosition to list
                    if (mapValues.Any(x => tailPosition.X == x.Key.X && tailPosition.Y == x.Key.Y))
                    {
                        var key = mapValues.FirstOrDefault(x => tailPosition.X == x.Key.X && tailPosition.Y == x.Key.Y).Key;
                        mapValues[key]++;
                    }
                    else
                    {
                        mapValues.Add(new Points(tailPosition.X, tailPosition.Y), 0);
                    }
                    
                }
            }
            Console.WriteLine("How many visited: " + mapValues.Count());
        }

        public void ResolveSecond()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/bridge_moves.txt");
            string[] moves = File.ReadAllLines(path);

            Dictionary<Points, int> mapValues = new Dictionary<Points, int>();
            Points headPosition = new Points(0, 0);

            Points tailPosition = new Points(0, 0);
            Dictionary<int, Points> tailPoints = new Dictionary<int, Points>()
            {
                { 1, new Points(0,0) },
                { 2, new Points(0,0) },
                { 3, new Points(0,0) },
                { 4, new Points(0,0) },
                { 5, new Points(0,0) },
                { 6, new Points(0,0) },
                { 7, new Points(0,0) },
                { 8, new Points(0,0) },
                { 9, new Points(0,0) }
            };

            mapValues.Add(new Points(0, 0), 0);

            foreach (string move in moves)
            {
                string[] value = move.Split(' ');
                // value[0] - direction
                // value[1] - count

                for (int i = 0; i < Int32.Parse(value[1]); i++)
                {
                    if (value[0] == RIGHT) headPosition.X++;

                    if (value[0] == LEFT) headPosition.X--;

                    if (value[0] == UP) headPosition.Y++;

                    if (value[0] == DOWN) headPosition.Y--;


                    //position of Y
                    //if touching (1. x near, 2. y near, 3. overlap)
                    if (tailPosition.X >= headPosition.X - 1 && tailPosition.X <= headPosition.X + 1
                        && tailPosition.Y >= headPosition.Y - 1 && tailPosition.Y <= headPosition.Y + 1)
                    {
                        continue;
                    }

                    //if in the same x line
                    if (tailPosition.X == headPosition.X)
                    {
                        if (tailPosition.Y < headPosition.Y)
                        {
                            tailPosition.Y++;
                        }
                        else
                        {
                            tailPosition.Y--;
                        }
                    }
                    //if in the same y line
                    else if (tailPosition.Y == headPosition.Y)
                    {
                        if (tailPosition.X < headPosition.X)
                        {
                            tailPosition.X++;
                        }
                        else
                        {
                            tailPosition.X--;
                        }
                    }
                    //if are totally differnt places 
                    else
                    {
                        if (headPosition.X > tailPosition.X && headPosition.Y > tailPosition.Y)
                        {
                            tailPosition.X++;
                            tailPosition.Y++;
                        }
                        else if (headPosition.X > tailPosition.X && headPosition.Y < tailPosition.Y)
                        {
                            tailPosition.X++;
                            tailPosition.Y--;
                        }
                        else if (headPosition.X < tailPosition.X && headPosition.Y > tailPosition.Y)
                        {
                            tailPosition.X--;
                            tailPosition.Y++;
                        }
                        else if (headPosition.X < tailPosition.X && headPosition.Y < tailPosition.Y)
                        {
                            tailPosition.X--;
                            tailPosition.Y--;
                        }
                    }

                    //tail position is first tail position
                    Points head = new Points(headPosition.X, headPosition.Y);

                    //check positions for all
                    foreach (KeyValuePair<int, Points> valuePair in tailPoints)
                    {
                        Points nextTail = valuePair.Value;

                        if(valuePair.Key > 1)
                        {
                            head = new Points(tailPoints[valuePair.Key - 1].X, tailPoints[valuePair.Key - 1].Y);
                        }

                        //if touching (1. x near, 2. y near, 3. overlap)
                        if (nextTail.X >= head.X - 1 && nextTail.X <= head.X + 1
                            && nextTail.Y >= head.Y - 1 && nextTail.Y <= head.Y + 1)
                        {
                            continue;
                        }

                        //if in the same x line
                        if (nextTail.X == head.X)
                        {
                            if (nextTail.Y < head.Y)
                            {
                                nextTail.Y++;
                            }
                            else
                            {
                                nextTail.Y--;
                            }
                        }
                        //if in the same y line
                        else if (nextTail.Y == head.Y)
                        {
                            if (nextTail.X < head.X)
                            {
                                nextTail.X++;
                            }
                            else
                            {
                                nextTail.X--;
                            }
                        }
                        //if are totally differnt places 
                        else
                        {
                            if (head.X > nextTail.X && head.Y > nextTail.Y)
                            {
                                nextTail.X++;
                                nextTail.Y++;
                            }
                            else if (head.X > nextTail.X && head.Y < nextTail.Y)
                            {
                                nextTail.X++;
                                nextTail.Y--;
                            }
                            else if (head.X < nextTail.X && head.Y > nextTail.Y)
                            {
                                nextTail.X--;
                                nextTail.Y++;
                            }
                            else if (head.X < nextTail.X && head.Y < nextTail.Y)
                            {
                                nextTail.X--;
                                nextTail.Y--;
                            }
                        }

                        tailPoints[valuePair.Key] = new Points(nextTail.X, nextTail.Y);
                    }

                    //add new tailPosition to list
                    if (mapValues.Any(x => tailPoints[9].X == x.Key.X && tailPoints[9].Y == x.Key.Y))
                    {
                        var key = mapValues.FirstOrDefault(x => tailPoints[9].X == x.Key.X && tailPoints[9].Y == x.Key.Y).Key;
                        mapValues[key]++;
                    }
                    else
                    {
                        mapValues.Add(new Points(tailPoints[9].X, tailPoints[9].Y), 0);
                    }

                }
            }
            Console.WriteLine("How many visited by 9: " + mapValues.Count());
        }
	}

    public class Points
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Points(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}

