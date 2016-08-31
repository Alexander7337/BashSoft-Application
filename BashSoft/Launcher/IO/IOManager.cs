using System;
using System.Collections.Generic;
using System.IO;

namespace BashSoft
{
    //IOManager is the class, which will give us the functionality for traversing the folders and 
    //other behaviors.

    public class IOManager
    {
        public void TraverseDirectory(int debth)
        //public static void TraverseDirectory()
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = SessionData.currentPath.Split('\\').Length;
            Queue<string> subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.currentPath);

            while (subFolders.Count != 0)
            {
                string currentPath = subFolders.Dequeue();
                int identation = currentPath.Split('\\').Length - initialIdentation;

                //THIRD LAB PROBLEM.4 MODIFYING THE TRAVERSAL
                if (debth - identation < 0)
                {
                    break;
                }

                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identation), currentPath));

                try
                {
                    foreach (var file in Directory.GetFiles(currentPath))
                    {
                        int indexOfLastSlash = file.LastIndexOf("\\");
                        //string fileName = file.Substring(indexOfLastSlash);
                        //OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + fileName);
                        for (int i = 0; i < indexOfLastSlash; i++)
                        {
                            OutputWriter.WriteMessage("-");
                        }
                        OutputWriter.WriteMessageOnNewLine(file.ToString());
                    }

                    foreach (var directoryPath in Directory.GetDirectories(currentPath))
                    {
                        subFolders.Enqueue(directoryPath);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }
            }

            //Console.WriteLine();
        }

        public void CreateDirectoryInCurrentFolder(string name)
        {
            string path = Directory.GetCurrentDirectory() + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(ExceptionMessages.ForbiddenSymbolsContainedInName);
                //OutputWriter.DisplayException(ExceptionMessages.ForbiddenSymbolsContainedInName);
            }
        }

        //BEFORE IMPLEMENTING SESSIONDATA AS A NEW CLASS AND ADDING USING BASHSOFT
        //public static class SessionData
        //{
        //    public static string currentPath = Directory.GetCurrentDirectory();
        //}

        public void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentPath;
                    int indexOfLastSlash = currentPath.LastIndexOf("\\");
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException("indexOfLastSlash", ExceptionMessages.InvalidDestination);
                    //OutputWriter.DisplayException(ExceptionMessages.InvalidDestination);
                }
            }
            else
            {
                string currentPath = SessionData.currentPath;
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new DirectoryNotFoundException(ExceptionMessages.InvalidPath);
                //OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                //return;
            }
            SessionData.currentPath = absolutePath;
        }
    }
}
