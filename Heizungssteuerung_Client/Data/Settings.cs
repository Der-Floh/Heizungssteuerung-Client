using Heizungssteuerung_SDK;
using System.Collections.Concurrent;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Heizungssteuerung_Client.Data;

public static class Settings
{
    public static ConcurrentDictionary<string, string> Default { get; set; } = new ConcurrentDictionary<string, string>();

    public static string? Get(string name)
    {
        Default.TryGetValue(name, out string? value);
        return value;
    }
    public static void Set(string name, string value)
    {
        Default.AddOrUpdate(name, value, (k, oldValue) => value);
    }
    public static bool Remove(string name)
    {
        return Default.TryRemove(name, out _);
    }

    public static string ToJson()
    {
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        return JsonSerializer.Serialize(Default, jsonOptions);
    }

    public static void FromJson(string json)
    {
        Default = JsonSerializer.Deserialize<ConcurrentDictionary<string, string>>(json) ?? new ConcurrentDictionary<string, string>();
    }

    public static async Task Save()
    {
        string json = ToJson();
        string path = Path.Combine(HeatingControlModel.GetDataDirectoryPath(), "config.json");
        await File.WriteAllTextAsync(path, json);
    }

    public static void Load()
    {
        string path = Path.Combine(HeatingControlModel.GetDataDirectoryPath(), "config.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            FromJson(json);
        }
    }
}
