using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO.Abstractions;

namespace DataProcessor
{
    public class CsvFileProcessor
    {
        private readonly IFileSystem _fileSystem;

        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public CsvFileProcessor(string inputFilePath, string outputFilePath)
            : this(inputFilePath, outputFilePath, new FileSystem()) { }


        public CsvFileProcessor(string inputFilePath, string outputFilePath,
                                IFileSystem fileSystem)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
            _fileSystem = fileSystem;
        }

        public void Process()
        {
            using (StreamReader input = _fileSystem.File.OpenText(InputFilePath))
            using (CsvReader csvReader = new CsvReader(input))
            using (StreamWriter output = _fileSystem.File.CreateText(OutputFilePath))
            using (var csvWriter = new CsvWriter(output))
            {
                csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                csvReader.Configuration.Comment = '@'; // Default is '#'
                csvReader.Configuration.AllowComments = true;
                csvReader.Configuration.RegisterClassMap<ProcessedOrderMap>();

                IEnumerable<ProcessedOrder> records = csvReader.GetRecords<ProcessedOrder>();

                //csvWriter.WriteRecords(records);

                csvWriter.WriteHeader<ProcessedOrder>();
                csvWriter.NextRecord();

                var recordsArray = records.ToArray();
                for (int i = 0; i < recordsArray.Length; i++)
                {

                    csvWriter.WriteField(recordsArray[i].OrderNumber);
                    csvWriter.WriteField(recordsArray[i].Customer);
                    csvWriter.WriteField(recordsArray[i].Amount);

                    bool isLastRecord = i == recordsArray.Length - 1;

                    if (!isLastRecord)
                    {
                        csvWriter.NextRecord();
                    }
                }
            }
        }
    }
}
