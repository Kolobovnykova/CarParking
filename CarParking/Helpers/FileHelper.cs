using System.IO;

namespace CarParking.Helpers
{
    public static class FileHelper
    {
        public static string ReadLogFile()
        {
            using (StreamReader sr = new StreamReader(Settings.PathToLogFile))
            {
                return sr.ReadToEnd();
            }
        }
    }
}