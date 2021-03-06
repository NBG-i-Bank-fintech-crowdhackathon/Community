﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CrowdHacakthon.Converters
{
    class DateTimeToLocalDateTime : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime dt = (DateTime)reader.Value;

            return dt.ToLocalTime();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
