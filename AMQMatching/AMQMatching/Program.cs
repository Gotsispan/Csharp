using AMQMatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace AMQMatching
{
     public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void arraytype(string[] arr)
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
        static string[] addtoarraystr(string[] array, string ele)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = ele;
            return array;
        }
        static List<string[]> readdatabase()
        {
            string fileContent = new WebClient().DownloadString("https://raw.githubusercontent.com/Gotsispan/Csharp/main/AMQMatching/AMQMatching/AMQSongsDatabase2.txt");
            string[] words = fileContent.Split('\n');


            List<string[]> words2 = new List<string[]> { };

            for (int i = 0; i < words.Length-3; i=i+4)
            {
                string[] strr;
              if (words[i+1] == "Kaiba")
              {
                    strr = new String[] { "Seira Kagami", words[i + 1], words[i + 2] };
                    i=i-1;
              }
             else
               {
                    strr = new String[] { words[i + 3], words[i + 1], words[i + 2] };
               }

              words2.Add(strr) ;
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




        [STAThread]



        public static void Main(string[] args)
        {
            var words2 = readdatabase();
            Dictionary<int, string> indextoartist = new Dictionary<int, string>();
            Dictionary<int, string> indextosong = new Dictionary<int, string>();
            Dictionary<int, string> indextoanime = new Dictionary<int, string>();
            string[] artistsall = { };
            string[] songsall = { };
            string[] animeall = { };
            arraytype(artistsall);

            for (int i = 0; i < words2.Count - 1; i++)
            {
                
                if (!artistsall.Contains(words2[i][1]))
                {
                    artistsall = addtoarraystr(artistsall, words2[i][1]);
                }
                if (!songsall.Contains(words2[i][0]))
                {
                    songsall = addtoarraystr(songsall, words2[i][0]);
                }
                if (!animeall.Contains(words2[i][2]))
                {
                    animeall = addtoarraystr(animeall, words2[i][2]);
                }
                indextoartist.Add(key: i, value: words2[i][0]);
                indextosong.Add(key: i, value: words2[i][1]);
                indextoanime.Add(key: i, value: words2[i][2]);
            }

            Form1 form = new Form1();
            form.artistsarray = artistsall;
            form.songsarray = songsall;
            form.animearray = animeall;
            arraytype(artistsall);
            arraytype(songsall);
            arraytype(animeall);
            form.Show();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(form);
        }
    }
}
