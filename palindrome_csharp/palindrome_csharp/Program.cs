using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSSE6_4
{
    class stringfunctionality
    {
        public bool checkpalindrom(string input)
        {
            char[] aikaur = input.ToCharArray();//.Reverse().ToString();
            Array.Reverse(aikaur);

            string reverse = new string(aikaur);
            bool isequal = input.Equals(reverse);
            return isequal;
        }

        public bool WriteToFile(string inputstring, bool palindromcheck, string filepath)
        {
            StreamWriter obj = new StreamWriter(filepath);
            obj.AutoFlush = true;
            string formattedstring = inputstring + ":" + palindromcheck;
            Console.WriteLine(formattedstring);
            obj.WriteLine(formattedstring);
            return true;
        }

        public string ReadFile(string path)
        {
            StreamReader obj = new StreamReader(path);
            return obj.ReadToEnd();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string input = "Hello";
            string filePath = "C:\\Users\\rabia\\Desktop\\palindrom.txt";
            stringfunctionality obj = new stringfunctionality();
            bool ispalindrom = obj.checkpalindrom(input);
            obj.WriteToFile(input, ispalindrom, filePath);
            //Console.WriteLine(obj.ReadFile(filePath));
            Console.ReadKey();
        }
    }
}