using System;

namespace BashSoft
{
    //Launcher is the class holding the Main() method.
    //From this class you can only call the specific functions you want to execute.
    //Commented stay the tests for different functionalities of this application.
    //When you run the application, the command 'help' will show you how to call functions.

public static class Launcher
    {
        public static void Main(string[] args)
        {
            //C# OOP LAB

            //Quick link for trying downloading functions: 
            http://1.bp.blogspot.com/-nNTgAgy5MW4/VwwjvQuNvYI/AAAAAAAAAGY/5q66ZXGtIB0evi_ZKPHNZGtKDq7qUuhCA/s1600-r/trailrun.jpg
            
            Console.WindowWidth = 150;
            
            Tester tester = new Tester();
            DownloadManager downloadManager = new DownloadManager();
            IOManager ioManager = new IOManager();
            DataReworked repo = new DataReworked(new DataSortersReworked(), new DataFilters());

            CommandInterpreter currentInterpreter = new CommandInterpreter(tester, repo, downloadManager, ioManager);
            InputReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}
