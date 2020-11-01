using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesProcessTask.Common;
using SalesProcessTask.Models;
using TinyCsvParser;

namespace SalesProcessTask
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            await GetSalesReportAsync();
        }

        private static async Task GetSalesReportAsync()
        {
            var salesResultJson = JsonConvert.SerializeObject(await ProcessSalesReportsAsync());

            string pathToSave = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "report_sales.json");

            await File.WriteAllBytesAsync(pathToSave, Encoding.UTF8.GetBytes(salesResultJson));
        }

        private static async Task<SalesResult> ProcessSalesReportsAsync()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');

            var numCsvParser = new CsvParser<NumDataFile>(csvParserOptions, new NumDataFileMap());
            var numRecords = numCsvParser.ReadFromFile(PathHelper.GetPathToFile("Num.csv"), Encoding.UTF8)
                .Select(s => s.Result)
                .ToArray();

            var pasCsvParser = new CsvParser<PasDataFile>(csvParserOptions, new PasDataFileMap());
            var pasRecords = pasCsvParser.ReadFromFile(PathHelper.GetPathToFile("Pas.csv"), Encoding.UTF8)
                .Select(s => s.Result)
                .ToArray();

            return await MatchRecordsAsync(numRecords, pasRecords);
        }

        private static async Task<SalesResult> MatchRecordsAsync(NumDataFile[] numRecords,
            PasDataFile[] pasRecords)
        {
            return await Task.Run(() =>
            {
                var salesResult = new SalesResult();

                foreach (var numRecord in numRecords)
                {
                    var matchedRecord = pasRecords
                        .FirstOrDefault(w =>
                            w.Amount == numRecord.Amount && w.CustomerNumber.HasValue &&
                            w.CustomerNumber.Value == numRecord.CustomerNumber
                            || w.Amount == numRecord.Amount && w.Time.Minutes - numRecord.Time.Minutes <= 5);

                    if (matchedRecord != null)
                    {
                        salesResult.Match.Add(matchedRecord);
                        salesResult.Match.Add(numRecord);

                        pasRecords = pasRecords.Where(w => w.Id != matchedRecord.Id).ToArray();

                        continue;
                    }

                    salesResult.NumUnmatch.Add(numRecord);
                }

                salesResult.PasUnmatch.AddRange(pasRecords);

                return salesResult;
            });
        }
    }
}