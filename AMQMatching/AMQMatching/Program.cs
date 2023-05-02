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
            string fileContent = client.DownloadString("https://raw.githubusercontent.com/Gotsispan/Csharp/main/AMQMatching/AMQMatching/AMQSongsDatabase2fixed.txt");
            string[] words = fileContent.Split('\n');

            List<string[]> words2 = new List<string[]> { };

            for (int i = 0; i < words.Length - 1; i++)
            {
                words2.Add(words[i].Split('|'));
                //for (int j = 0; j < words[i].Split('|').Length; j++)
                //{
                //    Debug.WriteLine(words[i].Split('|')[j]);
                //}
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


            for (int i = 0; i < words2.Count - 1; i++)
            {
                Debug.WriteLine("{0},{1}", words2[i][0], words2[i][1]);
                if (!artistsall.Contains(words2[i][2]))
                {
                    artistsall = addtoarraystr(artistsall, words2[i][2]);
                }
                if (!songsall.Contains(words2[i][1]))
                {
                    songsall = addtoarraystr(songsall, words2[i][1]);
                }
                if (!animeall.Contains(words2[i][0]))
                {
                    animeall = addtoarraystr(animeall, words2[i][0]);
                }
                artistsalldupes = addtoarraystr(artistsalldupes, words2[i][2]);
                songsalldupes = addtoarraystr(songsalldupes, words2[i][1]);
                animealldupes = addtoarraystr(animealldupes, words2[i][0]);
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
