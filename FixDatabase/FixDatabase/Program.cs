
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

string filePath = "C:\\Users\\pango\\Documents\\GitHub\\Csharp\\AMQMatching\\AMQMatching\\AMQSongsDatabase2.txt";
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



// Download the file as a string
string filecontent2 =  File.ReadAllText("C:\\Users\\pango\\Documents\\GitHub\\Csharp\\AMQMatching\\AMQMatching\\AMQSongsDatabase2fixed.txt");

Encoding fileEncoding2 = Encoding.UTF8;
string fileContents2;
using (StreamReader reader2 = new StreamReader(filePath, fileEncoding))
{
    fileContents2 = reader2.ReadToEnd();
}
List<string>  fixedwordseh = filecontent2.Split('\n').ToList();
List<string> fixedwords = new List<string>() { };


foreach (string line in fixedwordseh)
{
    bool enable = true;
    for (int j = 0; j < fixedwords.Count; j++)
    {
        if (fixedwords[j].Substring(0, fixedwords[j].Length - 1) == line)
        {
            enable = false;
            break;
        }
    }
    if (!fixedwords.Contains(line) && !string.IsNullOrEmpty(line) && enable)
    {
        fixedwords.Add(line);
    }
}




foreach (string line in words2)
{
    bool enable = true;
    for (int j = 0; j < fixedwords.Count; j++)
    {
        if (fixedwords[j].Substring(0, fixedwords[j].Length - 1) == line)
        {
            enable = false;
            break;
        }
    }
    if (!fixedwords.Contains(line) && !string.IsNullOrEmpty(line) && enable)
    {
        fixedwords.Add(line);
    }

}


File.WriteAllText("C:\\Users\\pango\\Documents\\GitHub\\Csharp\\AMQMatching\\AMQMatching\\AMQSongsDatabase2fixed.txt", string.Empty);
StreamWriter writer = new StreamWriter("C:\\Users\\pango\\Documents\\GitHub\\Csharp\\AMQMatching\\AMQMatching\\AMQSongsDatabase2fixed.txt");
int count = 1;
foreach (string line in fixedwords)
{
    count++;
    if (!string.IsNullOrEmpty(line) && line.Length > 3) {
        writer.Write(line);
        if (line != fixedwords[fixedwords.Count - 1])
        {
            writer.Write('\n');
        }
    }
}
writer.Close();

Console.WriteLine("The updating of the database was successful");
Console.WriteLine("Press Space to exit the program");
while (Console.ReadKey().Key != ConsoleKey.Spacebar) ;