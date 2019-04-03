using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNameParser
{
    class Program
    {
        static void Main(string[] args)
        {
            bool parseLine = false;

            foreach (var file in args)
            {
                //Extract all lines
                var lines = File.ReadAllLines(file);

                //Iterate through the lines

                foreach (var line in lines)
                {
                    if(parseLine)
                    {
                        //Parse the line, remove protection modifiers, return types and parentheses.
                        var splitLine = line.Split(' ');

                        var words = splitLine.Where(x => !string.IsNullOrEmpty(x));

                        var testName = words.ElementAt(2);

                        testName = testName.Remove(testName.Length - 2);
                        testName = SplitForCamelCase(testName);

                        Console.Out.Write(testName + "\n");

                        //Reset parse line flag 
                        parseLine = false;
                    }
                    else if(LineContainsTestTag(line))
                    {
                        //If line contains text tag, then we will want to parse the next line.
                        parseLine = true;
                    }
                }
            }

            Console.Out.Write("Test Case Extraction Complete! Press any button to exit the program...");
            Console.ReadKey();
        }

        static bool LineContainsTestTag(String line)
        {
            return line.Contains("[TestMethod]");
        }

        static string SplitForCamelCase(string testName)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                testName, 
                "([A-Z])", 
                " $1", 
                System.Text.RegularExpressions.RegexOptions.Compiled)
                .Trim();
        }
    }
}
