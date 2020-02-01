using System.IO;

namespace DataProcessor
{
    public class TextFileProcessor
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public TextFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        public void Process()
        {
            using (var inputStreamReader = new StreamReader(InputFilePath))            
            using (var outputStreamWriter = new StreamWriter(OutputFilePath))
            {
                while (!inputStreamReader.EndOfStream)
                {
                    string line = inputStreamReader.ReadLine();

                    string processedLine = line.ToUpperInvariant();

                    bool isLastLine = inputStreamReader.EndOfStream;

                    if (isLastLine)
                    {
                        outputStreamWriter.Write(processedLine);
                    }
                    else
                    {
                        outputStreamWriter.WriteLine(processedLine);
                    }

                }
            }
        }
    }
}