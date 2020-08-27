using System;
using System.IO;
using VidMetaData.Models;

namespace VidMetaData.Extractor
{
    internal class TagLibExtractor : IVidMetaDataExtractor
    {
        public VideoMetaData Extract(string filePath)
        {
            var fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                return null;
            }

            using (var f = TagLib.File.Create(filePath))
            {
                if (f.PossiblyCorrupt)
                {
                    return null;
                }

                return new VideoMetaData
                {
                    FileName = filePath,
                    DateCreatedUtc = fi.CreationTimeUtc,
                    Name = f.Tag.Title,
                    DurationSeconds = (int)Math.Round(f.Properties.Duration.TotalSeconds),
                    Height = f.Properties.VideoHeight,
                    Width = f.Properties.VideoWidth,
                    SizeBytes = fi.Length
                };
            }
        }
    }
}
