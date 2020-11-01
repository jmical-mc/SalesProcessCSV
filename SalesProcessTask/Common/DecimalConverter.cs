using System;
using TinyCsvParser.TypeConverter;

namespace SalesProcessTask.Common
{
    public class DecimalConverter : ITypeConverter<decimal>
    {
        public Type TargetType { get; }
        
        public bool TryConvert(string value, out decimal result)
        {
            result = decimal.Parse(value.Replace('.', ','));
            
            return true;
        }
    }
}