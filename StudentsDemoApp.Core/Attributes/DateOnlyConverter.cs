using Newtonsoft.Json.Converters;

namespace Core.Attributes
{
    public class DateOnlyConverter : IsoDateTimeConverter
    {
        public DateOnlyConverter()
        {
            DateTimeFormat = "dd-MM-yyyy";
        }
    }
}
