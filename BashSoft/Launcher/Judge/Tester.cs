using System;
using System.IO;

namespace BashSoft
{
    //Tester is the class for comparing files.

    public class Tester
    {
        public void CompareContent(string userOutputPath, string expectedOutputPath)
        {
            OutputWriter.WriteMessageOnNewLine("Reading files...");
            try
            {
                string mismatchPath = GetMismatchPath(expectedOutputPath);

                string[] actualOutputLines = File.ReadAllLines(userOutputPath);
                string[] expectedOutputLines = File.ReadAllLines(expectedOutputPath);

                bool hasMismatch;
                string[] mismatches = GetLinesWithPossibleMismatches(actualOutputLines, expectedOutputLines, out hasMismatch);

                PrintOutput(mismatches, hasMismatch, mismatchPath);

                OutputWriter.WriteMessageOnNewLine("Files read!");
            }
            catch (IOException io)
            {
                OutputWriter.DisplayException(io.Message);
            }
            //catch (DirectoryNotFoundException)
            //{
            //    OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            //}
            catch (ArgumentException ae)
            {
                OutputWriter.DisplayException(ae.Message);
            }
        }

        private string GetMismatchPath(string expectedOutputPath)
        {
            int indexOf = expectedOutputPath.LastIndexOf('\\');
            string directoryPath = expectedOutputPath.Substring(0, indexOf);
            string finalPath = directoryPath + @"\Mismatches.txt";
            return finalPath;
        }

        private string[] GetLinesWithPossibleMismatches(
            string[] actualOutputString, string[] expectedOutputString, out bool hasMismatch)
        {
            hasMismatch = false;
            string output = string.Empty;

            OutputWriter.WriteMessageOnNewLine("Comparing files...");

            int minOutputLines = actualOutputString.Length;
            if (actualOutputString.Length != expectedOutputString.Length)
            {
                hasMismatch = true;
                minOutputLines = Math.Min(actualOutputString.Length, expectedOutputString.Length);
                //throw new ArgumentException(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
                OutputWriter.DisplayException(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
            }

            string[] mismatches = new string[minOutputLines];

            for (int index = 0; index < minOutputLines; index++)
            {
                string actualLine = actualOutputString[index];
                string expectedLine = expectedOutputString[index];

                if (!actualLine.Equals(expectedLine))
                {
                    output = string.Format("Mismatch at line {0} -- expected: \"{1}\", actual: \"{2}\"",
                        index, expectedLine, actualLine);
                    output += Environment.NewLine;
                    hasMismatch = true;
                }
                else
                {
                    output = actualLine;
                    output += Environment.NewLine;
                }

                mismatches[index] = output;
            }
            return mismatches;
        }

        private void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchesPath)
        {
            if (hasMismatch)
            {
                foreach (var item in mismatches)
                {
                    OutputWriter.WriteMessageOnNewLine(item);
                }

                try
                {
                    File.WriteAllLines(mismatchesPath, mismatches);
                }
                catch
                {
                    throw new IOException(ExceptionMessages.InvalidPath);
                    //OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                }

                return;
            }

            OutputWriter.WriteMessageOnNewLine("Files are identical. There are no mismatches.");
        }


    }
}
