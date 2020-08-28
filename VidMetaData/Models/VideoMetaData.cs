using System;

namespace VidMetaData.Models
{
    internal class VideoMetaData
    {
        public string FileName { get; set; }

        public string Name { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public int DurationSeconds { get; set; }

        public long SizeBytes { get; set; }
        
        public int Width { get; set; }

        public int Height { get; set; }

        public double AudioBitrate { get; set; }

        public bool AudioIsVariableBitrate { get; set; }

        public int AudioChannels { get; set; }

        public double AudioSampleRate { get; set; }

        public int AudioBitsPerSample { get; set; }

        public double VideoDataRate { get; set; }

        public double VideoTotalBitrate { get; set; }

        public double VideoFrameRate { get; set; }
    }
}
