﻿using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;

public class Example
{
    public static void Main(string[] args)
    {
        JsonWriterOptions options = new JsonWriterOptions
        {
            Indented = true
        };

        using (MemoryStream stream = new MemoryStream())
        {
            using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
            {
                string dateStr = DateTime.UtcNow.ToString("F", CultureInfo.InvariantCulture);

                writer.WriteStartObject();
                writer.WriteString("date", dateStr);
                writer.WriteNumber("temp", 42);
                writer.WriteEndObject();
            }

            string json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }
    }
}

// The example displays the following output:
// {
//     "date": "Tuesday, 27 August 2019 19:21:44",
//     "temp": 42
// }
