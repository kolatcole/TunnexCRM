using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> x = new List<int>();
            // x.AddRange({ 1,4,7,3,4});
            x.Add(1);
            x.Add(4);
            x.Add(7);
            x.Add(3);
            x.Add(4);
            Console.WriteLine(groupDivision(x,2));
        }

        public static int groupDivision(List<int> levels, int maxSpread)
        {
            int count = 1;
            for (int i = 0; i <= levels.Count - 1; i++)
            {
                int currentValue = levels[i];

                for (int j = i + 1; j < levels.Count; j++)
                {

                    if(currentValue-levels[j]>maxSpread|| levels[j]-currentValue>maxSpread)
                  //  if (count > 0)
                    {
                        count++;

                    }
                }

            }
            return count;

        }
    }
}
