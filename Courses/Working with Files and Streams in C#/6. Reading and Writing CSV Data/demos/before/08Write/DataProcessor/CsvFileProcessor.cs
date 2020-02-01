using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace DataProcessor
{
    class CsvFileProcessor
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public CsvFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        public void Process()
        {
            using (StreamReader input = File.OpenText(InputFilePath))
            using (CsvReader csvReader = new CsvReader(input))
            {
                IEnumerable<ProcessedOrder> records = csvReader.GetRecords<ProcessedOrder>();

                csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                csvReader.Configuration.Comment = '@'; // Default is '#'
                csvReader.Configuration.AllowComments = true;
                //csvReader.Configuration.IgnoreBlankLines = true;
                //csvReader.Configuration.Delimiter = ";";
                //csvReader.Configuration.HasHeaderRecord = false;
                //csvReader.Configuration.HeaderValidated = null;
                //csvReader.Configuration.MissingFieldFound = null;
                csvReader.Configuration.RegisterClassMap<ProcessedOrderMap>();


                foreach (ProcessedOrder record in records)
                {
                    Console.WriteLine(record.OrderNumber);
                    Console.WriteLine(record.Customer);
                    Console.WriteLine(record.Amount);
                }
            }
        }
    }
}
