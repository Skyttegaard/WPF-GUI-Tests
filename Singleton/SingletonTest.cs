using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class SingletonTest
    {
        private SingletonTest()
        {

        }
        private static readonly Lazy<SingletonTest> lazy = new(() => new SingletonTest());
        public static SingletonTest Instance => lazy.Value;

    }
}
