using System.Collections.Generic;
using SalesProcessTask.Models;

namespace SalesProcessTask.Models
{
    public class SalesResult
    {
        public List<DataFile> Match { get; set; } = new List<DataFile>();
        public List<NumDataFile> NumUnmatch { get; set; } = new List<NumDataFile>();
        public List<PasDataFile> PasUnmatch { get; set; } = new List<PasDataFile>();
    }
}