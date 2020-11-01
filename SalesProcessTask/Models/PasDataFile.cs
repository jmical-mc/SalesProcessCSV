using SalesProcessTask.Common;
using TinyCsvParser.Mapping;

namespace SalesProcessTask.Models
{
    public class PasDataFile : DataFile
    {
    }
    
    public sealed class PasDataFileMap : CsvMapping<PasDataFile>
    {
        public PasDataFileMap()
        {
            MapProperty(0, m => m.Id);
            MapProperty(1, m => m.Time);
            MapProperty(2, m => m.Amount, new DecimalConverter());
            MapProperty(3, m => m.CustomerNumber);
        }
    }
}