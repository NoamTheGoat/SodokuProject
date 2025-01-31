using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    public static class GlobalConstants
    {
        public static int BoxLength = 3;
        public static int BoardLength = BoxLength * BoxLength;
        public static int PairSize = 2;
        public static int StandardBoardSize = 9;
        public static void UpdateConstants(int newBoxLength)
        {
            BoxLength = newBoxLength;
            BoardLength = BoxLength * BoxLength;
        }
    }
}
