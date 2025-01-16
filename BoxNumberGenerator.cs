using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku
{
    public static class BoxNumberGenerator
    {
        public static Dictionary<int, Tuple<int, int>> BoxNumberDict;
        static BoxNumberGenerator()
        {
            BoxNumberDict = new Dictionary<int, Tuple<int, int>>();
            for (int i = 0; i < BoxLength; i++)
            {
                for (int j = 0; j < BoxLength; j++)
                {
                    int boxNumber = 3 * i + j + 1;
                    int xCoordinate = i * j + j;
                    int yCoordinate = j;
                    BoxNumberDict.Add(boxNumber, new Tuple<int, int>(xCoordinate, yCoordinate));
                }
            }
        }

        public static int GetKeyByValue(Dictionary<int, Tuple<int, int>> dict, Tuple<int, int> coordinates)
        {
            foreach (var element in dict)
            {
                if (element.Value.Item1 == coordinates.Item1 && element.Value.Item2 == coordinates.Item2)
                {
                    return element.Key;
                }
            }
            return -1;
        }
    }
}
