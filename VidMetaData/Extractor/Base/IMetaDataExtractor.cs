using VidMetaData.Models;

namespace VidMetaData.Extractor.Base
{
    internal interface IMetaDataExtractor
    {
        AbstractMediaMetaData Extract(string filePath);

        string FileSearchPattern { get; }

        string OutputFileName { get; }
    }
}
