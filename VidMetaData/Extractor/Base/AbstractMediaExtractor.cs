using System.Linq;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace VidMetaData.Extractor.Base
{
    internal class AbstractMediaExtractor
    {
        protected bool GetBoolValue(ShellProperties properties, PropertyKey key)
        {
            var result = properties.GetProperty<bool?>(key).Value;
            if (result == null)
            {
                return false;
            }

            return result.Value;
        }

        protected int GetIntegerValue(ShellProperties properties, PropertyKey key)
        {
            var result = properties.GetProperty<uint?>(key).Value;
            if (result == null)
            {
                return 0;
            }

            return (int)result.Value;
        }

        protected string GetStringValue(ShellProperties properties, PropertyKey key)
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
