using System;

namespace VidMetaData.Models
{
    internal abstract class AbstractMediaMetaData
    {
        public string FileName { get; set; }

        public string Name { get; set; }

        public long SizeBytes { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public abstract string ToDelimitedHeaderText(string separator);

        public abstract string ToDelimitedText(string separator);
    }
}
