using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the path of the input folder");
        string oldpath = Console.ReadLine();
        Dictionary<string, int> dicFileTypes = new Dictionary<string, int>();
        List<string> fileextn = new List<string>();
        string f = (DateTime.Now.ToString("dd-MM-yyyy   hh-mm-ss-tt"));
        Directory.CreateDirectory("D:\\" + f);
        File.AppendAllText("D:\\" + f + "\\COUNT.txt", "FILE_TYPE\tFILE_COUNT" + Environment.NewLine);
        try
        {
            foreach (string filename in Directory.EnumerateFiles(oldpath, "*.*"))
            {
                string e = Path.GetExtension(filename);
                if (!fileextn.Contains(e))
                    fileextn.Add(e);
            }
            int count;
            foreach (string extn in fileextn)
            {
                count = 0;
                foreach (string filename in Directory.EnumerateFiles(oldpath, "*.*"))
                {
                    string newpath = "D:\\" + f + "\\" + (extn.Remove(0, 1)).ToUpper();
                    Directory.CreateDirectory(newpath);
                    if (extn == Path.GetExtension(filename))
                    {
                        count++;
                        Program p = new Program();
                        string length = p.getFileSize(filename);
                        File.Copy(filename, Path.Combine(newpath, Path.GetFileNameWithoutExtension(filename) + length + extn));
                    }
                }
                File.AppendAllText("D:\\" + f + "\\COUNT.txt", (extn.Remove(0, 1)).ToUpper() + "\t\t" + count + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        Console.WriteLine("\nFiles are Successfully Copied to Diff folders");
        Console.Read();
    }
    string getFileSize(string file)
    {
        FileInfo fi = new FileInfo(file);
        if (fi.Length < 1024)
            return (string)(fi.Length + "bytes");
        else if (fi.Length / 1024 < 1024)
            return (string)(Math.Round((fi.Length / 1024.0), 2) + "Kbytes");
        else
            return (string)(Math.Round(((fi.Length / 1024.0) / 1024.0), 2) + "Mbytes");
    }
}


