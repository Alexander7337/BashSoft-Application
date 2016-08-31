using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BashSoft
{
    public class CommandInterpreter
    {
        private Tester judge;
        private DataReworked repository;
        private DownloadManager downloadManager;
        private IOManager inputOutputManager;

        public CommandInterpreter(Tester judge, DataReworked repository, DownloadManager downloadManager,
            IOManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.downloadManager = downloadManager;
            this.inputOutputManager = inputOutputManager;
        }
        public void InterpredCommand(string input)
        {
            string[] data = input.Split(' ');
            string command = data[0];
            command = command.ToLower();

            try
            {
                ParseCommand(input, data, command);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                OutputWriter.DisplayException(dnfe.Message);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                OutputWriter.DisplayException(aoore.Message);
            }
            catch (ArgumentException ae)
            {
                OutputWriter.DisplayException(ae.Message);
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private void ParseCommand(string input, string[] data, string command)
        {
            switch (command)
            {
                case "open":
                    TryOpenFile(input, data);
                    break;
                case "mkdir":
                    TryCreateDirectory(input, data);
                    break;
                case "ls":
                    TryTraverseFolders(input, data);
                    break;
                case "cmp":
                    TryCompareFiles(input, data);
                    break;
                case "cdrel":
                    TryChangePathRelatively(input, data);
                    break;
                case "cdabs":
                    TryChangePathAbsolute(input, data);
                    break;
                case "readdb":
                    TryReadDatabaseFromFile(input, data);
                    break;
                case "help":
                    TryGetHelp(input, data);
                    break;
                case "show":
                    TryShowWantedData(input, data);
                    break;
                case "filter":
                    TryFilterAndTake(input, data);
                    break;
                case "order":
                    TryOrderAndTake(input, data);
                    break;
                //case "decOrder":
                //    // to do
                //    break;
                case "download":
                    TryDownloadRequestedFile(input, data);
                    break;
                case "downloadasynch":
                    TryDownloadRequestedFileAsync(input, data);
                    break;
                case "dropdb":
                    TryDropDb(input, data);
                    break;
                default:
                    DisplayInvalidCommandMessage(input);
                    break;
            }
        }

        private void TryDropDb(string input, string[] data)
        {
            if (data.Length != 1)
            {
                this.DisplayInvalidCommandMessage(input);
                return;
            }
            this.repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }

        private  void TryDownloadRequestedFileAsync(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string url = data[1];
                //DownloadManager.DownloadAsync(url);
                this.downloadManager.DownloadAsync(url);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private  void TryDownloadRequestedFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string url = data[1];
                //DownloadManager.Download(url);
                this.downloadManager.Download(url);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private  void TryOrderAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string filter = data[2].ToLower();
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();

                TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private  void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    //Data.OrderAndTake(courseName, filter);
                    this.repository.OrderAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        //Data.OrderAndTake(courseName, filter, studentsToTake);
                        this.repository.OrderAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommand);
            }
        }

        private  void TryFilterAndTake(string input, string[] data)
        {
            if (data.Length == 5)
            {
                string courseName = data[1];
                string filter = data[2].ToLower();
                string takeCommand = data[3].ToLower();
                string takeQuantity = data[4].ToLower();

                TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private  void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    //Data.FilterAndTake(courseName, filter);
                    this.repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        //Data.FilterAndTake(courseName, filter, studentsToTake);
                        this.repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }

        private  void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string courseName = data[1];
                //Data.GetAllStudentsFromCourse(courseName);
                this.repository.GetAllStudentsFromCourse(courseName);
            }
            else if (data.Length == 3)
            {
                string courseName = data[1];
                string userName = data[2];
                //Data.GetStudentScoresFromCourse(courseName, userName);
                this.repository.GetStudentScoresFromCourse(courseName, userName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private  void TryGetHelp(string input, string[] data)
        {
            //OutputWriter.WriteMessageOnNewLine($"{new string('_', 80)}");
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "open file in directory - open: fileName"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - cdRel: {..} (changes the path relatively one step backwards)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - cdAbs: absolute path"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -178}|", "filter students in a course by performance - filter {courseName} excellent/average/poor take 2/5/all - (the output is written on the console)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -178}|", "order students in a course - order {courseName} ascending/descending take 10/20/all - (the output is written on the console)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            //OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
            //OutputWriter.WriteMessageOnNewLine($"{new string('_', 80)}");
            //OutputWriter.WriteEmptyLine();

            if (data.Length == 1)
            {
                DisplayHelp();
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private  void TryReadDatabaseFromFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                //Data.InitializeData(fileName);
                this.repository.LoadData(fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private  void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string absolutePath = data[1];
                //IOManager.ChangeCurrentDirectoryAbsolute(absolutePath);
                this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private  void TryChangePathRelatively(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string relPath = data[1];
                //IOManager.ChangeCurrentDirectoryRelative(relPath);
                this.inputOutputManager.ChangeCurrentDirectoryRelative(relPath);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        public void TryCompareFiles(string input, string[] data)
        {
            if (data.Length == 3)
            {
                string firstPath = data[1];
                string secondPath = data[2];

                //Tester.CompareContent(firstPath, secondPath);
                this.judge.CompareContent(firstPath, secondPath);

            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryTraverseFolders(string input, string[] data)
        {
            if (data.Length == 1)
            {
                //IOManager.TraverseDirectory(0);
                this.inputOutputManager.TraverseDirectory(0);
            }
            else if (data.Length == 2)
            {
                int depth;
                bool hasParsed = int.TryParse(data[1], out depth);
                if (hasParsed)
                {
                    //IOManager.TraverseDirectory(depth);
                    this.inputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryCreateDirectory(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                //IOManager.CreateDirectoryInCurrentFolder(fileName);
                this.inputOutputManager.CreateDirectoryInCurrentFolder(fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        private void TryOpenFile(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string fileName = data[1];
                Process.Start(SessionData.currentPath + "\\" + fileName);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
            }
        }

        public void DisplayInvalidCommandMessage(string input)
        {
            OutputWriter.DisplayException($"The command '{input}' is invalid");
        }

        public void DisplayHelp()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{new string('_', 100)}");
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "open file in directory - open: fileName"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "make directory - mkdir: path"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "traverse directory - ls: depth"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "change directory - cdRel: {..} (changes the path relatively one step backwards)"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "change directory - cdAbs: absolute path"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "filter students in a course by performance - filter {courseName} excellent/average/poor take 2/5/all - (the output is written on the console)"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "order students in a course - order {courseName} ascending/descending take 10/20/all - (the output is written on the console)"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "clean already read data - dropDb"));
            stringBuilder.AppendLine(string.Format("|{0, -98}|", "get help – help"));
            stringBuilder.AppendLine($"{new string('_', 100)}");
            stringBuilder.AppendLine();
            OutputWriter.WriteMessageOnNewLine(stringBuilder.ToString());
        }
    }
}
