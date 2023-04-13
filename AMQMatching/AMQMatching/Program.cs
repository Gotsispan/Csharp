using AMQMatching;
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
using WindowsFormsApplication1;

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
            Debug.WriteLine(txt);
        }
        static string[] addtoarraystr(string[] array, string ele)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = ele;
            return array;
        }
        static List<string[]> readdatabase()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string fileContent = client.DownloadString("https://raw.githubusercontent.com/Gotsispan/Csharp/main/AMQMatching/AMQMatching/AMQSongsDatabase2.txt");
            string[] words = fileContent.Split('\n');


            List<string[]> words2 = new List<string[]> { }; 

            for (int i = 0; i < words.Length-3; i=i+4)
            {
              string[] strr;


               int number;
               if (int.TryParse(words[i+3], out number))
               {
                    string strmin = "Seira Kagami";
                    strr = new String[] {  words[i + 2].Substring(0, words[i+2].Length - 1), strmin , words[i + 1].Substring(0, words[i + 1].Length - 1) };
                    arraytype(strr);
                    i = i - 1;
                }
                else
                {
                    strr = new String[] { words[i + 2].Substring(0, words[i + 2].Length - 1), words[i + 3].Substring(0, words[i + 3].Length - 1), words[i + 1].Substring(0, words[i + 1].Length - 1) };
                }


              arraytype(strr);
              if (!words2.Contains(strr)) { words2.Add(strr); }

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
            string[] artistsall = { };
            string[] songsall = { };
            string[] animeall = { };
            string[] artistsalldupes = { };
            string[] songsalldupes = { };
            string[] animealldupes = { };
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
                artistsalldupes = addtoarraystr(artistsalldupes, words2[i][1]);
                songsalldupes = addtoarraystr(songsalldupes, words2[i][0]);
                animealldupes = addtoarraystr(animealldupes, words2[i][2]);
            }

            WindowsFormsApplication1.Form1 form = new WindowsFormsApplication1.Form1();
            form.artistsarray = artistsall;
            form.songsarray = songsall;
            form.animearray = animeall;
            form.artistsarraydupes = artistsalldupes;
            form.songsarraydupes = songsalldupes;
            form.animearraydupes = animealldupes;

            form.Show();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(form);
        }
    }
}
