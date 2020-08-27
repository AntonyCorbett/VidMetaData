using VidMetaData.Models;

namespace VidMetaData.Extractor
{
    internal interface IVidMetaDataExtractor
    {
        VideoMetaData Extract(string filePath);
    }
}
