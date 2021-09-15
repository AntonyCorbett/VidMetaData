using System.Linq;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace VidMetaData.Extractor.Base
{
    internal abstract class AbstractMediaExtractor
    {
        protected AbstractMediaExtractor()
        {
        }

        protected static bool GetBoolValue(ShellProperties properties, PropertyKey key)
        {
            var result = properties.GetProperty<bool?>(key).Value;
            return result != null && result.Value;
        }

        protected static int GetIntegerValue(ShellProperties properties, PropertyKey key)
        {
            var result = properties.GetProperty<uint?>(key).Value;
            if (result == null)
            {
                return 0;
            }

            return (int)result.Value;
        }

        protected static string GetStringValue(ShellProperties properties, PropertyKey key)
        {
            var result = properties.GetProperty<string>(key)?.Value;
            if (result == null)
            {
                var result2 = properties.GetProperty<string[]>(key)?.Value;
                result = result2?.First();
            }

            return result ?? string.Empty;
        }
    }
}
