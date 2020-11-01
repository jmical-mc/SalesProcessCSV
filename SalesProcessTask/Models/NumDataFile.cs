using SalesProcessTask.Common;
using TinyCsvParser.Mapping;

namespace SalesProcessTask.Models
{
    public class NumDataFile : DataFile
    {
        public string CashierName { get; set; }
    }

    public sealed class NumDataFileMap : CsvMapping<NumDataFile>
    {
        public NumDataFileMap() : base()
        {
            MapProperty(0, m => m.Id);
            MapProperty(1, m => m.Time);
            MapProperty(2, m => m.Amount, new DecimalConverter());
            MapProperty(3, m => m.CustomerNumber);
            MapProperty(4, m => m.CashierName);
        }
    }
}