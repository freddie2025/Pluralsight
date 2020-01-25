using System.IO;

namespace PersonDataReader.CSV
{
    public interface ICSVFileLoader
    {
        string LoadFile();
    }

    public class CSVFileLoader : ICSVFileLoader
    {
        private string _filePath;

        public CSVFileLoader(string filePath)
        {
            _filePath = filePath;
        }

        public string LoadFile()
        {
            using (var reader = new StreamReader(_filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
