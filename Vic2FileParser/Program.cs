using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace Vic2FileParser {

    // Victoria 2 Population File parser - written by pthfndr
    // Version 0.21
    class Program {
        static void Main(string[] args) {

            string fileToRead = "../../../readFile.txt";
            string fileToWrite = "../../../writeFile.txt";
            PopParser(fileToRead, fileToWrite);
        }

        static void PopParser(string Read, string Write) {
            bool farmers = false;

            StreamReader sr = new StreamReader(Read);
            StreamWriter sw = new StreamWriter(Write);
            /*            string provinceID = @"(\d+) = {"; // useful for future applic.
            */

            string line = sr.ReadLine();
            sw.WriteLine(line);
            string culture = "null";
            string religion = "null";



            while (line != null) {


                if (line.Contains("farmers") && !farmers) { // If this line defines a farmer pop..
                    int count = 0;
                    float newNum = 0;
                    string size = null;
                    farmers = true;
                    line = sr.ReadLine();
                    culture = line; // Grab the pops culture

                    sw.WriteLine(line);

                    while (farmers) { // While we are within the farmers pop.


                        line = sr.ReadLine();


                        if (count == 0) {
                            religion = line;  // Grab the pops religion
                            count++;
                        }

                        if (line.Contains("size")) {
                            string[] split = line.Split(' ');


                            int originalNum = Int32.Parse(split[2]);  // Convert population size from string to int so we can do math on it.


                            newNum = (float)Math.Round(originalNum * 0.2f);  // Change percentage
                            float otherNum = originalNum - newNum;
                            sw.WriteLine("\t\tsize = " + otherNum);

                            size = split[0] + " " + split[1] + " " + newNum;

                        }

                        else if (line.Contains("}")) {
                            sw.WriteLine(line);
                            farmers = false;
                        }
                        else {
                            sw.WriteLine(line);

                        }
                    }
                    sw.WriteLine("\tserfs = {\n" + culture + "\n" + religion + " \t\n" + size + "\n\t}");

                }

                line = sr.ReadLine();
                sw.WriteLine(line);
            }
            sw.Close();

        }
    }
}
