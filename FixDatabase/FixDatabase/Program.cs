
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;




string filePath = "C:\\Users\\pango\\Documents\\GitHub\\Csharp\\AMQMatcher\\AMQMatching\\AMQMatching\\AMQSongsDatabase2.txt";
Encoding fileEncoding = Encoding.UTF8;
string fileContents;
using (StreamReader reader = new StreamReader(filePath, fileEncoding))
{
    fileContents = reader.ReadToEnd();
}
string[] words = fileContents.Split('\n');


List<string> words2 = new List<string> { };
string strr;
int number;

    for (int i = 0; i < words.Length - 3; i = i + 4)
    {
    strr = "";
    if (int.TryParse(words[i + 3], out number))
    {
        string strmin = "Uknown/AMQ Bug";
        strr =  strr + words[i + 1].Substring(0, words[i + 1].Length - 1) + '|'  + words[i + 2].Substring(0, words[i + 2].Length - 1) + '|' + strmin ;
        i = i - 1;
    }
    else
    {	
		if(i > words.Length-5) {
			strr = strr + words[i + 1].Substring(0, words[i + 1].Length - 1) + '|' + words[i + 2].Substring(0, words[i + 2].Length - 1) + '|' + words[i + 3] ;
		}
		else {
			strr = strr + words[i + 1].Substring(0, words[i + 1].Length - 1) + '|' + words[i + 2].Substring(0, words[i + 2].Length - 1) + '|' + words[i + 3].Substring(0, words[i + 3].Length - 1) ;
		}
    }

    if (!words2.Contains(strr)) { words2.Add(strr); }

}

for (int i=0; i < words2.Count; i++)
{
    //Console.WriteLine(words2[i]);
}

WebClient client2 = new WebClient();

// Download the file as a string
client2.Encoding = Encoding.UTF8;
string fileContent2 = client2.DownloadString("https://raw.githubusercontent.com/Gotsispan/Csharp/main/AMQMatching/AMQMatching/AMQSongsDatabase2fixed.txt");
List<string>  fixedwords = fileContent2.Split('\n').ToList();

// Write each line to the file
foreach (string line in words2)
{
    if (!fixedwords.Contains(line) && !string.IsNullOrEmpty(line))
    {
        fixedwords.Add(line);
    }
}

File.WriteAllText("C:\\Users\\pango\\Documents\\GitHub\\Csharp\\AMQMatcher\\AMQMatching\\AMQMatching\\AMQSongsDatabase2fixed.txt", string.Empty);
StreamWriter writer = new StreamWriter("C:\\Users\\pango\\Documents\\GitHub\\Csharp\\AMQMatcher\\AMQMatching\\AMQMatching\\AMQSongsDatabase2fixed.txt");
int count = 1;
foreach (string line in fixedwords)
{
    count++;
    if (!string.IsNullOrEmpty(line) && line.Length > 3)
    {
        writer.Write(line);
        writer.Write('\n');
    }
}
writer.Close();

Console.WriteLine("The updating of the database was successful");
Console.WriteLine("Press Space to exit the program");
while (Console.ReadKey().Key != ConsoleKey.Spacebar) ;