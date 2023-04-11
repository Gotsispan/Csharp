using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using static System.Net.WebRequestMethods;
using System.Collections;
using System.Security.Cryptography;

namespace AMQMatcher

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.Unicode;

            string appname = "Eurovision Simulator";
            string appversion = "1.0.0";
            string appauthor = "Gotsispan";
            string authortext = appname + " : version " + appversion + " by " + appauthor;
            Console.WriteLine(authortext);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}