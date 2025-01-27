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
        public static int pairSize = 2;
        public static void UpdateConstants(int newBoxLength)
        {
            BoxLength = newBoxLength;
            BoardLength = BoxLength * BoxLength;
        }
    }
}
