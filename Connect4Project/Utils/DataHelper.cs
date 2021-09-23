using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Connect4Game.Utils
{
    public static class DataHelper
    {
        private static Dictionary<string, object> settings;
        public static string FilePath = "Data.json";
        public static void Clear()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
        public static void SetValue(string key, object value)
        {
            settings = new();
            using (StreamReader stream = new(File.Open(FilePath, FileMode.OpenOrCreate)))
            {
                var data = stream.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(data))
                    settings = JsonSerializer.Deserialize<Dictionary<string, object>>(data);
                if (settings.ContainsKey(key))
                    settings[key] = value;
                else
                    settings.Add(key, value);
                using (StreamWriter writer = new(stream.BaseStream))
                {
                    writer.BaseStream.Position = 0;
                    writer.Flush();
                    writer.Write(JsonSerializer.Serialize(settings));
                }
            }
        }
        public static T GetValue<T>(string key)
        {
            settings = new();
            using (StreamReader stream = new(File.Open(FilePath, FileMode.OpenOrCreate)))
            {
                try
                {
                    var data = stream.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(data))
                        settings = JsonSerializer.Deserialize<Dictionary<string, object>>(data);
                    if (settings.ContainsKey(key))
                    {
                        var json = JsonSerializer.Serialize(settings[key]);
                        return JsonSerializer.Deserialize<T>(json);
                    }
                    else
                        return default(T);
                }
                catch (Exception ex) { return default(T); }
            }
        }
    }
}
