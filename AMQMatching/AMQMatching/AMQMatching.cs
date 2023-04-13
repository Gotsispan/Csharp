using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Globalization;
using static System.Net.WebRequestMethods;
using System.Collections;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace AMQMatcher

{
    class Program
    {


        public void arraytype(string[] arr)
        {
            string txt = "[";
            foreach (var item in arr)
            {
                txt = txt + item;
                txt = txt + ",";
            }
            txt = txt.Remove(txt.Length - 1);
            txt = txt + "] \n";
            Console.WriteLine(txt);
        }

        public List<string[]> readdatabase()
        {
            string fileContent = new WebClient().DownloadString("https://raw.githubusercontent.com/Gotsispan/Csharp/main/AMQSongDatabase.txt");
            string[] words = fileContent.Split("\n");
            var words2 = new List<string[]>();
            for (int i = 0; i < words.Length; i++)
            {
                words2.Add(words[i].Split("|"));
            }
            return words2;
        }


        static void printlist(List<string[]> list)
        {

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Length; j++)
                {
                    Console.Write(list[i][j]);
                    Console.Write(' ');
                }
                Console.Write("\n");
            }
        }
        static string[] addtoarraystr(string[] array, string ele)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = ele;
            return array;
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
            var words2 = readdatabase();
            Dictionary<int, string> indextoartist = new Dictionary<int, string>();
            Dictionary<int, string> indextosong = new Dictionary<int, string>();
            Dictionary<int, string> indextoanime = new Dictionary<int, string>();
            string[] artistsall = { };
            string[] songsall = { };
            string[] animeall = { };

            for (int i = 0; i < words2.Count - 1; i++)
            {
                indextoartist.Add(key: i, value: words2[i][0]);
                if (!artistsall.Contains(words2[i][0]))
                {
                    artistsall = addtoarraystr(artistsall, words2[i][0]);
                }
                if (!songsall.Contains(words2[i][0]))
                {
                    songsall = addtoarraystr(artistsall, words2[i][0]);
                }
                if (!animeall.Contains(words2[i][0]))
                {
                    animeall = addtoarraystr(artistsall, words2[i][0]);
                }
                indextosong.Add(key: i, value: words2[i][1]);
                indextoanime.Add(key: i, value: words2[i][2]);
            }


            AutoCompleteStringCollection sourceName = new AutoCompleteStringCollection();

            foreach (string name in artistsall)
            {
                sourceName.Add(name);
            }

        }
    }
}