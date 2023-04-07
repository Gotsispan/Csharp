// See https://aka.ms/new-console-template for more information
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

namespace NumberGuesser

{


    class Program
    {
        static string GetFlag(string country)
        {
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(country));
            if (englishRegion == null) return "🏳";
            var countryAbbrev = englishRegion.TwoLetterISORegionName;
            return IsoCountryCodeToFlagEmoji(countryAbbrev);
        }
        static string IsoCountryCodeToFlagEmoji(string countryCode) => string.Concat(countryCode.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
        static void printscoreboard(IOrderedEnumerable<KeyValuePair<string, int>> scoreboard, Dictionary<string,string> songnames) 
        {
            int count = 0;
            foreach (KeyValuePair<string, int> kvp in scoreboard)
            {
                count++;
                Console.WriteLine("{0}. {1} - {2} - {3} points",count, GetFlag(kvp.Key), songnames[kvp.Key],kvp.Value);
            }
        }

        static string[] addarr(string[] firstArray, string[] secondArray)
        {
            var combinedArray = new string[firstArray.Length + secondArray.Length];
            Array.Copy(firstArray, combinedArray, firstArray.Length);
            Array.Copy(secondArray, 0, combinedArray, firstArray.Length, secondArray.Length);
            return combinedArray;
        }
        static void centertype(string s)
        {
            //string str = new string(' ', (Console.WindowWidth - s.Length) / 2);
            int no = Decimal.ToInt32((Console.WindowWidth - s.Length) / 2);
            string str = new string(' ', no);
            string snew = str + s;
            Console.WriteLine(snew);
        }

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

        static void decarraytype(decimal[] arr)
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

        static string[] qualifiers(Dictionary<string, int> scoreboard)
        {
            int qualicount = 0;
            string[] qualifiers = { };
            var sortedscoreboard = from entry in scoreboard orderby entry.Value descending select entry;
            foreach (KeyValuePair<string, int> kvp in sortedscoreboard)
            {
                if (qualicount < 10)
                {
                    qualifiers = qualifiers.Append(kvp.Key.ToString()).ToArray();
                    qualicount++;
                }
                else
                {
                    break;
                }
            }
            return qualifiers;
        }


        static Dictionary<string, int> voting(string[] participants, string country,int telejury)
        {
            decimal[] likeness = new decimal[participants.Length];
            //int[] scoreboard = new int[participants.Length];
            Random rnd = new Random();
            Dictionary<string, int> scoreboard = new Dictionary<string, int>();
            for (int j=0; j<participants.Length; j++)
            {
                scoreboard.Add(participants[j], 0);
            }

             Dictionary<string, decimal> telejurydict = new Dictionary<string, decimal>();

            for (int j = 0; j < participants.Length; j++)
            {
                Decimal randomnesslevel = 0.25m;
                //relation to quality
                Decimal[] juryskew = { 0.3m, 0.7m };
                Decimal[] teleskew = { 0.7m, 0.3m };
                int randomnesstele = rnd.Next(Decimal.ToInt32(randomnesslevel * 100));
                int randomnessjury = rnd.Next(Decimal.ToInt32(randomnesslevel * 100));
                int relation = rnd.Next(100);
                int quality = rnd.Next(100);
                if (telejury == 0)
                {
                    likeness[j] = randomnessjury + (1 - randomnesslevel) * juryskew[0] * quality + (1 - randomnesslevel) * juryskew[1] * relation;
                }
                else { 
                    likeness[j] = randomnesstele + (1 - randomnesslevel) * teleskew[0] * quality + (1 - randomnesslevel) * teleskew[1] * relation;
                }
            }

            for (int j = 0; j < participants.Length; j++)
            {
                telejurydict.Add(participants[j], likeness[j]);
            }

            var sortedgeneric = from entry in telejurydict orderby entry.Value descending select entry;
            int countgeneric = 0;
            int[] pointarray = { 12, 10, 8, 7, 6, 5, 4, 3, 2, 1 };

            foreach (KeyValuePair<string, decimal> kvp in sortedgeneric)
            {
                if (countgeneric < pointarray.Length) { 
                    scoreboard[kvp.Key.ToString()] += pointarray[countgeneric];
                    countgeneric++;
                }
                else
                {
                    break;
                }
            }

            return scoreboard;
        }

        static Dictionary<string, int> finaleurovision(string[] participants, string[] voters)
        {
            var CountrytoCapital = new Dictionary<string, string>() {
                {"Albania","Tiranna" },{"Andorra","Andorra la vela"},{"Armenia","Yerevan"},{"Australia","Canberra"},{"Austria","Wien"},
                {"Azerbaijan","Baku"},{"Belarus","Minsk"},{"Belgium","Brussels" },{"Bosnia","Sarajevo"},{"Bulgaria","Sofia"},
                {"Croatia","Zagreb"},{"Cyprus","Nicosia"},{"Czechia","Prague"},{"Denmark","Copenhaven"},{"Estonia","Talinn"},
                {"Finland","Helsinki"},{"France","Paris"},{"Georgia","Tbilisi"},{"Germany","Berlin"},{"Greece","Athens"},
                {"Hungary","Budapest"},{"Iceland","Reykjavik"},{"Ireland","Dublin"},{"Israel","Jerusalem"},{"Italy","Rome"},
                {"Latvia","Riga"},{"Lithuania","Vilnius"},{"Luxembourg","Luxembourg"},{"Malta","Valetta"},{"Moldova","Chisinau"},
                {"Monaco","Monaco"},{"Montenegro","Podgorica"},{"Morocco","Rabat"},{"Netherlands","Amsterdam"},{"Kazakhstan","Astana"},
                {"North Macedonia","Skopje"},{"Norway","Oslo"},{"Poland","Warsaw"},{"Portugal","Lisbon"},{"Romania","Bucharest"},
                {"Russia","Moscow"},{"San Marino","San Marino"},{"Serbia","Belgrade"},{"Slovakia","Bratislava"},{"Slovenia","Ljubljana"},
                {"Spain","Madrid"},{"Sweden","Stockholm"},{"Switzerland","Bern"},{"Turkey","Ankara"},{"Ukraine","Kiev"},
                {"United Kingdom","London"},{"Yugoslavia","Belgrade"}
            };


            Dictionary<string, int> scoreboard = new Dictionary<string, int>();
            for (int j = 0; j < participants.Length; j++)
            {
                scoreboard.Add(participants[j], 0);
            }

            for (int i = 0; i < voters.Length; i++)
            {
                Dictionary<string, int> teleeach = voting(participants, voters[i], 0);
                Dictionary<string, int> juryeach = voting(participants, voters[i], 1);
                foreach (KeyValuePair<string, int> kvp in teleeach)
                {
                    scoreboard[kvp.Key.ToString()] += teleeach[kvp.Key.ToString()] + juryeach[kvp.Key.ToString()];
                }
            }
            return scoreboard;
        }

        static Dictionary<string, int> semifinaleurovision(string[] participants, string[] voters)
        {

            Dictionary<string, int> scoreboard = new Dictionary<string, int>();
            for (int j = 0; j < participants.Length; j++)
            {
                scoreboard.Add(participants[j], 0);
            }

            for (int i = 0; i < voters.Length; i++)
            {
                Dictionary<string, int> teleeach = voting(participants, voters[i], 0);
                Dictionary<string, int> juryeach = voting(participants, voters[i], 1);
                foreach (KeyValuePair<string, int> kvp in teleeach)
                {
                    scoreboard[kvp.Key.ToString()] += teleeach[kvp.Key.ToString()] + juryeach[kvp.Key.ToString()];
                }
            }
            return scoreboard;
        }

        static string pickasong(string country)
        {

            var LangtoSource = new Dictionary<string, string>(){
                {"Albanian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/sq/sq_50k.txt"},
                {"Catalan","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/ca/ca_50k.txt"},
                {"German","https://en.wiktionary.org/wiki/User:Matthias_Buchmeier/German_frequency_list-1-5000"},
                {"Turkish","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/tr/tr_50k.txt"},
                {"Russian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/ru/ru_50k.txt"},
                {"French","https://raw.githubusercontent.com/Taknok/French-Wordlist/master/francais.txt"},
                {"Serbian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/sr/sr_50k.txt"},
                {"English","https://raw.githubusercontent.com/first20hours/google-10000-english/master/google-10000-english.txt"},
                {"Arabic","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/ar/ar_50k.txt"},
                {"Bulgarian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/bg/bg_50k.txt"},
                {"Czech","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/cs/cs_50k.txt"},
                {"Danish","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/da/da_50k.txt"},
                {"Dutch","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/nl/nl_50k.txt"},
                {"Estonian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/et/et_50k.txt"},
                {"Finnish","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/fi/fi_50k.txt"},
                {"Greek","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/el/el_50k.txt"},
                {"Hebrew","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/he/he_50k.txt"},
                {"Hungarian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/hu/hu_50k.txt"},
                {"Italian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/it/it_full.txt"},
                {"Kazakh","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/kk/kk_50k.txt"},
                {"Korean","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/ko/ko_50k.txt"},
                {"Lithuanian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/lt/lt_50k.txt"},
                {"Latvian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/lv/lv_50k.txt"},
                {"Macedonian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/mk/mk_50k.txt"},
                {"Norwegian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/no/no_50k.txt"},
                {"Polish","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/pl/pl_50k.txt"},
                {"Portuguese","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/pt_br/pt_br_50k.txt"},
                {"Romanian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/ro/ro_50k.txt"},
                {"Slovak","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/sk/sk_50k.txt"},
                {"Slovenian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/sl/sl_50k.txt"},
                {"Spanish","https://raw.githubusercontent.com/mazyvan/most-common-spanish-words/master/most-common-spanish-words-v4.txt"},
                {"Swedish","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/sv/sv_50k.txt"},
                {"Ukranian","https://raw.githubusercontent.com/hermitdave/FrequencyWords/master/content/2016/uk/uk_50k.txt"}
            };

            var CountrytoLang = new Dictionary<string, string>() {
                {"Albania","Albanian" },{"Andorra","Catalan"},{"Armenia","English"},{"Australia","English"},{"Austria","German"},
                {"Azerbaijan","Turkish"},{"Belarus","Russian"},{"Belgium","French" },{"Bosnia","Serbian"},{"Bulgaria","Bulgarian"},
                {"Croatia","Serbian"},{"Cyprus","Greek"},{"Czechia","Czech"},{"Denmark","Danish"},{"Estonia","Estonian"},
                {"Finland","Finnish"},{"France","French"},{"Georgia","English"},{"Germany","German"},{"Greece","Greek"},
                {"Hungary","Hungarian"},{"Iceland","English"},{"Ireland","English"},{"Israel","English"},{"Italy","Italian"},
                {"Latvia","Latvian"},{"Lithuania","Lithuanian"},{"Luxembourg","French"},{"Malta","English"},{"Moldova","Romanian"},
                {"Monaco","French"},{"Montenegro","Serbian"},{"Morocco","Arabic"},{"Netherlands","Dutch"},{"Kazakhstan","Kazakh"},
                {"North Macedonia","Macedonian"},{"Norway","Norwegian"},{"Poland","Polish"},{"Portugal","Portuguese"},
                {"Romania","Romanian"},{"Russia","Russian"},{"San Marino","Italian"},{"Serbia","Serbian"},{"Slovakia","Slovak"},
                {"Slovenia","Slovenian"},{"Spain","Spanish"},{"Sweden","Swedish"},{"Switzerland","French"},
                {"Turkey","Turkish"},{"Ukraine","Ukranian"},{"United Kingdom","English"},{"Yugoslavia","Serbian"}
            };

            var ENGposs = new Dictionary<string, int>() {
                {"Albania",10},{"Andorra",15},{"Armenia",100},{"Australia",100},{"Austria",35},{"Azerbaijan",95},
                {"Belarus",50},{"Belgium",0},{"Bosnia",30}
            };

            Random rnd = new Random();
            string sourcee = "https://raw.githubusercontent.com/first20hours/google-10000-english/master/google-10000-english.txt";
            int engpickrand = rnd.Next(100);
            if (engpickrand > 0)
            {
                sourcee = LangtoSource[CountrytoLang[country]];
            }
            string fileContent = new WebClient().DownloadString(sourcee);


            int noofwords = rnd.Next(3);
            string songname = "";
            if (LangtoSource[CountrytoLang[country]][8] == 'r')
            {
                string[] words = fileContent.Split("\n");
                for (int j = 0; j < noofwords + 2; j++)
                {
                    songname = songname + words[rnd.Next(words.Length)].Split(' ')[0] + " ";
                }
            }
            else
            {
                string[] words = fileContent.Split(" ");
                string[] words2 = { };

                for (int i = 0; i < words.Length; i++)
                {
                    if (i % 2 == 1)
                    {
                        words2.Append(words[i]);
                    }
                }
                for (int j = 0; j < noofwords + 2; j++)
                {

                    songname = songname + words[rnd.Next(words2.Length)].Split(' ')[0] + " ";
                }
            }
            songname = songname.Remove(songname.Length - 1);
            return songname;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.Unicode;

            string appname = "Eurovision Simulator";
            string appversion = "1.0.0";
            string appauthor = "Gotsispan";

            string[] normalcountries = {"Albania","Andorra","Armenia","Australia","Austria","Azerbaijan","Belarus",
            "Belgium","Bosnia","Bulgaria","Croatia","Cyprus","Czechia","Denmark","Estonia","Finland","France",
            "Georgia","Germany","Greece","Hungary","Iceland","Ireland","Israel","Italy","Kazakhstan",
            "Latvia","Lithuania","Luxembourg","Malta","Moldova","Monaco","Montenegro","Morocco","Netherlands",
            "North Macedonia","Norway","Poland","Portugal","Romania","Russia","San Marino","Serbia","Slovakia",
             "Slovenia","Spain","Sweden","Switzerland","Turkey","Ukraine","United Kingdom","Yugoslavia"};
            string[] big5 = { "Italy", "United Kingdom", "Spain", "Germany", "France" };
            string pastwinner = "Ukraine";
            string[] banlist = { "Russia", "Yugoslavia", "Morocco" };
            
            // Change text color
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Red;

            //Console.SetCursorPosition((Console.WindowWidth - s.Length)/2,Console.CursorTop);
            string authortext = appname + " : version " + appversion + " by " + appauthor;
            centertype(authortext);

            Console.WriteLine();
            Console.WriteLine();

            //centertype("Give me a country");
            //string stralign = new string(' ', Decimal.ToInt32(Console.WindowWidth / 2));
            //Console.SetCursorPosition(Decimal.ToInt32(Console.WindowWidth / 2)- Decimal.ToInt32(authortext.Length/ 2), Console.CursorTop);
            //string theircountry = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Red;

            //for (int i = 0; i < normalcountries.Length; i++)
            //{
            //Console.WriteLine("{0}'s song is {1}", normalcountries[i], pickasong(normalcountries[i]));
            //}

            int semipartno = normalcountries.Length - banlist.Length - big5.Length - 1;
            int firstsemino = 0;
            int secondsemino = 0;
            if (semipartno % 2 == 0)
            {
                firstsemino = semipartno / 2;
                secondsemino = semipartno / 2;
            }
            else
            {
                firstsemino = Decimal.ToInt32(semipartno / 2) + 1;
                secondsemino = Decimal.ToInt32(semipartno / 2);
            }

            string[] semiparticipants = { };
            string[] allparticipants = { };
            Console.WriteLine(normalcountries);


                for (int i = 0; i < normalcountries.Length; i++)
            {
                
                if (!banlist.Contains(normalcountries[i]))
                {
                    //Console.WriteLine(normalcountries[i]);
                    allparticipants = allparticipants.Append(normalcountries[i]).ToArray();
                }
            }

            var watch = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Please wait a bit until all the song names are simulated");
            watch.Start();

            Dictionary<string, string> songnames = new Dictionary<string, string>();
            for (int j = 0; j < allparticipants.Length; j++)
            {
                //songnames.Add(allparticipants[j], pickasong(allparticipants[j]));
                songnames.Add(allparticipants[j], "NO SONG");
            }
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds/1000} s");

            for (int i = 0; i < allparticipants.Length; i++)
            {
                if (!big5.Contains(allparticipants[i]) && !banlist.Contains(allparticipants[i]) && pastwinner != allparticipants[i])
                {
                    semiparticipants = semiparticipants.Append(allparticipants[i]).ToArray();
                }
            }

            Random rng = new Random();
            semiparticipants = semiparticipants.OrderBy(a => rng.Next()).ToArray();

            string[] semi1countries = { };
            string[] semi2countries = { };
            string[] semi1voters = { };
            string[] semi2voters = { };
            string[] semi1votersout = {pastwinner,"United Kingdom","Spain"};
            string[] semi2votersout = {"Italy","Germany","France"};
            
            for (int i=0; i<semiparticipants.Length; i++)
            {
                if (i < firstsemino)
                {
                    semi1countries = semi1countries.Append(semiparticipants[i]).ToArray();
                    semi1voters = semi1voters.Append(semiparticipants[i]).ToArray();
                }
                else
                {
                    semi2countries = semi2countries.Append(semiparticipants[i]).ToArray();
                    semi2voters = semi2voters.Append(semiparticipants[i]).ToArray();
                }
            }

            for (int i = 0; i < semi1votersout.Length; i++)
            {
                semi1voters = semi1voters.Append(semi1votersout[i]).ToArray();
                semi2voters = semi2voters.Append(semi2votersout[i]).ToArray();
            }

            Dictionary<string, int> semiscores1 = semifinaleurovision(semi1countries,semi1voters);
            Dictionary<string, int> semiscores2 = semifinaleurovision(semi2countries,semi2voters);
            string[] qual1 = qualifiers(semiscores1);
            string[] qual2 = qualifiers(semiscores2);
            string[] finalparticipants = addarr(addarr(qual1, qual2),big5).Append(pastwinner).ToArray();
            finalparticipants = finalparticipants.OrderBy(a => rng.Next()).ToArray();
            arraytype(finalparticipants);
            Dictionary<string, int> finalresults = finaleurovision(finalparticipants, normalcountries);
            var sortedfinal = from entry in finalresults orderby entry.Value descending select entry;
            
            printscoreboard(sortedfinal,songnames);

            Console.WriteLine("Press Space to continue");
            while (Console.ReadKey().Key != ConsoleKey.Spacebar) ;

        }
    }
}