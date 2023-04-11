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
using System.ComponentModel.DataAnnotations.Schema;

namespace AMQMatcher

{
    class Program
    {
        static void findallchoices () {
            string fileContent = new WebClient().DownloadString("https://raw.githubusercontent.com/Gotsispan/Csharp/main/Eurovision%20Simulator/Eurovision%20Simulator/votes.csv");
            string[] words = fileContent.Split("\n");
            var words2 = new string[words.Length, 3];
        }
        static findmissing () { 

        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.Unicode;

            string appname = "AMQ Song/Artist matcher";
            string appversion = "1.0.0";
            string appauthor = "Gotsispan";
            string authortext = appname + " : version " + appversion + " by " + appauthor;
            Console.WriteLine(authortext);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}