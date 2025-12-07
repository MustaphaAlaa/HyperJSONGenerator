using System.Text.Json;

namespace Metriflow.HyperJSONGenerator.Service;

public static class JsonSetting
{
    private static JsonWriterOptions _jsonWriterOptions = default;

    public static JsonWriterOptions JsonWriterOptions
    {
        get
        {
            _jsonWriterOptions.Indented = false;
            _jsonWriterOptions.SkipValidation = true;
            return _jsonWriterOptions;
        }
    }

}