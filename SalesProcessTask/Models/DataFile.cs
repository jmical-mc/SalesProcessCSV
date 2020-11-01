using System;

namespace SalesProcessTask.Models
{
    public class DataFile
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public int? CustomerNumber { get; set; }
        public TimeSpan Time { get; set; }
    }
}