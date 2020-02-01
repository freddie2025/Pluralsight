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
                var currentLineNumber = 1;
                while (!inputStreamReader.EndOfStream)
                {
                    string line = inputStreamReader.ReadLine();

                    if (currentLineNumber == 2)
                    {
                        Write(line.ToUpperInvariant());
                    }
                    else
                    {
                        Write(line);
                    }

                    currentLineNumber++;

                    void Write(string content)
                    {
                        bool isLastLine = inputStreamReader.EndOfStream;

                        if (isLastLine)
                        {
                            outputStreamWriter.Write(content);
                        }
                        else
                        {
                            outputStreamWriter.WriteLine(content);
                        }
                    }

                }
            }
        }
    }
}