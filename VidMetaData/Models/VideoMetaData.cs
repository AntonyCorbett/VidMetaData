using System;

namespace VidMetaData.Models
{
    internal class VideoMetaData
    {
        public string FileName { get; set; }

        public string Name { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public long SizeBytes { get; set; }

        public int DurationSeconds { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
