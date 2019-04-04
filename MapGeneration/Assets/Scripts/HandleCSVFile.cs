using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public class HandleCSVFile : MonoBehaviour
{

   
    public static List<string[]> ReadCSVToListOfStringArrays(string path)
    {   
        StreamReader reader = new StreamReader(File.OpenRead(@path));
        List<string[]> listOfRows = new List<string[]>();
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (!String.IsNullOrEmpty(line))
            {
                string[] values = line.Split(',');
                listOfRows.Add(values);
            }
        }

        return listOfRows;
    }

    public static void WriteStringArrayToCSV(string[] map, string path)
    {

        using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
        {
            foreach(string row in map)
            {
                writer.WriteLine(row);
            }
            writer.Close();
        }
    }
}

