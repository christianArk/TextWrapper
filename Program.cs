using System;
using System.IO;

namespace TextWrapper
{
    class Program
    {
        private static string buildStr {get; set; } = string.Empty;

        public static void Main (string[] args) {
            WrapFileContent();
        }

        public static void WrapFileContent()
        {
            try
            {
                Console.Write("Enter file path: ");
                // get file input from user
                var filePath = Console.ReadLine();
                // validate file extension
                var paths = filePath.Split(new char[]{'.'});
                var ext = paths[paths.Length - 1];
                if (ext != "txt")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid file format. Only .txt are allowed!");
                    Console.ResetColor();
                    return;
                }
                // read file contents
                var fileContents = File.ReadAllText(filePath);

                Console.Write("Enter maximum text length per line: ");
                // get max length per line from user and convert to number
                var len = Convert.ToInt32(Console.ReadLine());
                // Validate max length per line input
                while (len <= 0) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Length per line must be greater than zero(0)");
                    Console.ResetColor();
                    Console.Write("Enter maximum text length per line: ");
                    // get max length per line from user and convert to number
                    len = Convert.ToInt32(Console.ReadLine());
                }

                // call function to wrap file contents
                WrapText(fileContents, len);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        private static void WrapText(string input, int lenPerLine)
        {
            try
            {
                if (lenPerLine > input.Length)
                {
                    lenPerLine = input.Length;
                }
                if (input.Length <= 0)
                {
                    // using StreamWriter to write output to file
                    var outputpath = $"{Directory.GetCurrentDirectory()}/outputdir";
                    // Try to create directory
                    Directory.CreateDirectory(outputpath);
                    string[] paths = {Directory.GetCurrentDirectory(), "outputdir", GetCurrentTimestamp() + ".txt"};
                    string fullPath = Path.Combine(paths);
                    using (StreamWriter writer = new StreamWriter(fullPath))
                    {
                        writer.WriteLine(buildStr);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Done! Successfully wrapped file contents. ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(fullPath);
                        Console.ResetColor();
                    }
                    return;
                }
                buildStr += input.Substring(0, lenPerLine) + "\n";
                string remainingStr = input.Substring(lenPerLine, input.Length - lenPerLine);

                WrapText(remainingStr, lenPerLine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetCurrentTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }
    }
}
