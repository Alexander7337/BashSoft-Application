using System;
using System.Collections.Generic;

namespace BashSoft
{
    //OutputWriter is the class which prints information on the console.

    public static class OutputWriter
    {

        public static void WriteMessage(string message)
        {
            Console.Write(message);
        }

        public static void WriteMessageOnNewLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        public static void DisplayException(string message)
        {
            ConsoleColor currentColour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColour;

        }

        //public static void PrintStudent(KeyValuePair<string, List<int>> student)
        //{
        //    OutputWriter.WriteMessageOnNewLine(string.Format($"{student.Key} - {string.Join(", ", student.Value)}"));
        //}

        public static void PrintStudent(KeyValuePair<string, double> student)
        {
            OutputWriter.WriteMessageOnNewLine(string.Format($"{student.Key} - {student.Value}"));
        }

    }
}
