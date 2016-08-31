using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    //InputReader (CommandInterpreter alternatively) is the class which handles user's commands
    //and passes data to the other classes.

    public class InputReader
    {
        private CommandInterpreter interpreter;

        public InputReader(CommandInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        private const string endCommand = "quit";

        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
            string input = Console.ReadLine();
            input = input.Trim();
          
            while (input != endCommand)
            {
                //Interpret command

                //CommandInterpreter.InterpredCommand(input);
                this.interpreter.InterpredCommand(input);

                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                input = Console.ReadLine();
                input = input.Trim();              
            }

            if (SessionData.taskPool.Count != 0)
            {
                Task.WaitAll(SessionData.taskPool.ToArray());
            }
        }

        //public static void InterpredCommand(string input)
        //{
        //    string[] data = input.Split(' ');
        //    string command = data[0];
        //    //command = command.ToLower();
        //    switch (command)
        //    {
        //        case "open":
        //            TryOpenFile(input, data);
        //            break;
        //        case "mkdir":
        //            TryCreateDirectory(input, data);
        //            break;
        //        case "ls":
        //            TryTraverseFolders(input, data);
        //            break;
        //        case "cmp":
        //            TryCompareFiles(input, data);
        //            break;
        //        case "cdRel":
        //            TryChangePathRelatively(input, data);
        //            break;
        //        case "cdAbs":
        //            TryChangePathAbsolute(input, data);
        //            break;
        //        case "readDb":
        //            TryReadDatabaseFromFile(input, data);
        //            break;
        //        case "help":
        //            TryGetHelp(input, data);
        //            break;
        //        case "show":
        //            TryShowWantedData(input, data);
        //            break;
        //        case "filter":
        //            TryFilterAndTake(input, data);
        //            break;
        //        case "order":
        //            TryOrderAndTake(input, data);
        //            break;
        //        //case "decOrder":
        //        //    // to do
        //        //    break;
        //        case "download":
        //            TryDownloadRequestedFile(input, data);
        //            break;
        //        case "downloadAsynch":
        //            TryDownloadRequestedFileAsync(input, data);
        //            break;
        //        default:
        //            DisplayInvalidCommandMessage(input);
        //            break;
        //    }
        //}

        //private static void TryDownloadRequestedFileAsync(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string url = data[1];
        //        DownloadManager.DownloadAsync(url);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryDownloadRequestedFile(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string url = data[1];
        //        DownloadManager.Download(url);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryOrderAndTake(string input, string[] data)
        //{
        //    if (data.Length == 5)
        //    {
        //        string courseName = data[1];
        //        string filter = data[2].ToLower();
        //        string takeCommand = data[3].ToLower();
        //        string takeQuantity = data[4].ToLower();

        //        TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, filter);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        //{
        //    if (takeCommand == "take")
        //    {
        //        if (takeQuantity == "all")
        //        {
        //            Data.OrderAndTake(courseName, filter);
        //        }
        //        else
        //        {
        //            int studentsToTake;
        //            bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
        //            if (hasParsed)
        //            {
        //                Data.OrderAndTake(courseName, filter, studentsToTake);
        //            }
        //            else
        //            {
        //                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommand);
        //    }
        //}

        //private static void TryFilterAndTake(string input, string[] data)
        //{
        //    if (data.Length == 5)
        //    {
        //        string courseName = data[1];
        //        string filter = data[2].ToLower();
        //        string takeCommand = data[3].ToLower();
        //        string takeQuantity = data[4].ToLower();

        //        TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        //{
        //    if (takeCommand == "take")
        //    {
        //        if (takeQuantity == "all")
        //        {
        //            Data.FilterAndTake(courseName, filter);
        //        }
        //        else
        //        {
        //            int studentsToTake;
        //            bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
        //            if (hasParsed)
        //            {
        //                Data.FilterAndTake(courseName, filter, studentsToTake);
        //            }
        //            else
        //            {
        //                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //    }
        //}

        //private static void TryShowWantedData(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string courseName = data[1];
        //        Data.GetAllStudentsFromCourse(courseName);
        //    }
        //    else if (data.Length == 3)
        //    {
        //        string courseName = data[1];
        //        string userName = data[2];
        //        Data.GetStudentScoresFromCourse(courseName, userName);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryGetHelp(string input, string[] data)
        //{
        //    //OutputWriter.WriteMessageOnNewLine($"{new string('_', 80)}");
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "open file in directory - open: fileName"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - cdRel: {..} (changes the path relatively one step backwards)"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - cdAbs: absolute path"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -178}|", "filter students in a course by performance - filter {courseName} excellent/average/poor take 2/5/all - (the output is written on the console)"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -178}|", "order students in a course - order {courseName} ascending/descending take 10/20/all - (the output is written on the console)"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
        //    //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
        //    //OutputWriter.WriteMessageOnNewLine($"{new string('_', 80)}");
        //    //OutputWriter.WriteEmptyLine();

        //    if (data.Length == 1)
        //    {
        //        DisplayHelp();
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryReadDatabaseFromFile(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string fileName = data[1];
        //        Data.InitializeData(fileName);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryChangePathAbsolute(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string absolutePath = data[1];
        //        IOManager.ChangeCurrentDirectoryAbsolute(absolutePath);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryChangePathRelatively(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string relPath = data[1];
        //        IOManager.ChangeCurrentDirectoryRelative(relPath);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //public static void TryCompareFiles(string input, string[] data)
        //{
        //    if (data.Length == 3)
        //    {
        //        string firstPath = data[1];
        //        string secondPath = data[2];

        //        Tester.CompareContent(firstPath, secondPath);

        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryTraverseFolders(string input, string[] data)
        //{
        //    if (data.Length == 1)
        //    {
        //        IOManager.TraverseDirectory(0);
        //    }
        //    else if (data.Length == 2)
        //    {
        //        int depth;
        //        bool hasParsed = int.TryParse(data[1], out depth);
        //        if (hasParsed)
        //        {
        //            IOManager.TraverseDirectory(depth);
        //        }
        //        else
        //        {
        //            OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
        //        }
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryCreateDirectory(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string fileName = data[1];
        //        IOManager.CreateDirectoryInCurrentFolder(fileName);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private static void TryOpenFile(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string fileName = data[1];
        //        Process.Start(SessionData.currentPath + "\\" + fileName);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //public static void DisplayInvalidCommandMessage(string input)
        //{
        //    OutputWriter.DisplayException($"The command '{input}' is invalid");
        //}

        //public static void DisplayHelp()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.AppendLine($"{new string('_', 100)}");
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "open file in directory - open: fileName"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "make directory - mkdir: path"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "traverse directory - ls: depth"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "change directory - cdRel: {..} (changes the path relatively one step backwards)"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "change directory - cdAbs: absolute path"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
        //    stringBuilder.AppendLine(string.Format("|{0, -178}|", "filter students in a course by performance - filter {courseName} excellent/average/poor take 2/5/all - (the output is written on the console)"));
        //    stringBuilder.AppendLine(string.Format("|{0, -178}|", "order students in a course - order {courseName} ascending/descending take 10/20/all - (the output is written on the console)"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
        //    stringBuilder.AppendLine(string.Format("|{0, -98}|", "get help – help"));
        //    stringBuilder.AppendLine($"{new string('_', 100)}");
        //    stringBuilder.AppendLine();
        //    OutputWriter.WriteMessageOnNewLine(stringBuilder.ToString());
        //}
    }
}
