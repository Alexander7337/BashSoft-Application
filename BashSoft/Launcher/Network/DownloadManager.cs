using System.Threading.Tasks;
using System.Net;

namespace BashSoft
{
    //DownloadManager class handles the download functionality of this application.

    public class DownloadManager
    {
        private WebClient webClient;

        public DownloadManager()
        {
            webClient = new WebClient();
        }

        public void DownloadAsync(string fileURL)
        {
            Task currentTask = Task.Run(() => Download(fileURL));
            SessionData.taskPool.Add(currentTask);
        }


        public void Download(string fileURL)
        {
            //webClient = new WebClient();

            try
            {
                OutputWriter.WriteMessageOnNewLine("Started downloading: ");

                string nameOfFile = ExtractNameOfFile(fileURL);
                string pathToDownload = SessionData.currentPath + "/" + nameOfFile;

                webClient.DownloadFile(fileURL, pathToDownload);

                OutputWriter.WriteMessageOnNewLine("Download complete");
            }
            catch (WebException ex)
            {
                OutputWriter.DisplayException(ex.Message);
            }
        }

        private static string ExtractNameOfFile(string fileURL)
        {
            int indexOfLastBackSlash = fileURL.LastIndexOf("/");

            if (indexOfLastBackSlash != -1)
            {
                return fileURL.Substring(indexOfLastBackSlash + 1);
            }
            else
            {
                throw new WebException(ExceptionMessages.InvalidPath);
            }
        }
    }
}
