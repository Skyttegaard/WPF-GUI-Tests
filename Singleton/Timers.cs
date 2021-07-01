using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class Timers
    {
        private Timers()
        {

        }
        public static string name;
        public static string getData()
        {
            return name;
        }
    }
}
