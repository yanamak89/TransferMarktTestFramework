using Newtonsoft.Json;
using Serilog;
using TransferMarktTestFramework.Utilities;

public class ConfigLoader
{
    public static Credentials LoadCredentials(string filePath)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Utilities", "credentials.json");
        Log.Information("Loading credentials from: {FullPath}", fullPath);
        
        if (!File.Exists(fullPath))
        {
            Log.Error("Credentials file not found at: {FullPath}", fullPath);
            throw new FileNotFoundException($"Could not find file: {fullPath}");
        }

        var json = File.ReadAllText(fullPath);
        return JsonConvert.DeserializeObject<Credentials>(json);
    }
}