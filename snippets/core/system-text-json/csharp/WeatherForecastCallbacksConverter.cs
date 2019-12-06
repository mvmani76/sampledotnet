﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SystemTextJsonSamples
{
    public class WeatherForecastCallbacksConverter : JsonConverter<WeatherForecast>
    {
        public override WeatherForecast Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            // Place "before" code here (OnDeserializing), but note that there is no access here to the POCO instance.
            Console.WriteLine("OnDeserializing");

            WeatherForecast value = JsonSerializer.Deserialize<WeatherForecast>(ref reader); // note: "options" not passed in

            // Place "after" code here (OnDeserialized)
            Console.WriteLine("OnDeserialized");

            return value;
        }

        public override void Write(
            Utf8JsonWriter writer,
            WeatherForecast value, JsonSerializerOptions options)
        {
            // Place "before" code here (OnSerializing)
            Console.WriteLine("OnSerializing");

            JsonSerializer.Serialize(writer, value); // note: "options" not passed in

            // Place "after" code here (OnSerialized)
            Console.WriteLine("OnSerialized");
        }
    }
}
