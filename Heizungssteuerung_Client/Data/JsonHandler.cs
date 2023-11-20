using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Heizungssteuerung_Client.Data;
public sealed class JsonHandler
{
    public static void WriteJson(object json, string path) => CreateJsonFile(json, path);
    private static void CreateJsonFile(object jsonObject, string path)
    {
        string? directoryPath = Path.GetDirectoryName(path);
        if (string.IsNullOrEmpty(directoryPath))
            throw new DirectoryNotFoundException(directoryPath);
        Directory.CreateDirectory(directoryPath);
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        FileStream createStream = File.Create(path);
        JsonSerializer.Serialize(createStream, jsonObject, jsonOptions);
    }
}
