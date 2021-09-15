using System.Text;

namespace VidMetaData.Models
{
    internal sealed class VideoMetaData : AbstractMediaMetaData
    {
        public int DurationSeconds { get; set; }
        
        public int Width { get; set; }

        public int Height { get; set; }

        public int AudioBitrate { get; set; }

        public bool AudioIsVariableBitrate { get; set; }

        public int AudioChannels { get; set; }

        public double AudioSampleRate { get; set; }

        public int AudioBitsPerSample { get; set; }

        public double VideoDataRate { get; set; }

        public double VideoTotalBitrate { get; set; }

        public double VideoFrameRate { get; set; }

        public string Copyright { get; set; }
        
        public override string ToDelimitedHeaderText(string separator)
        {
#pragma warning disable U2U1108 // StringBuilders should be initialized with capacity
            var sb = new StringBuilder();
#pragma warning restore U2U1108 // StringBuilders should be initialized with capacity

            sb.Append(nameof(FilePath));
            sb.Append(separator);
            sb.Append(nameof(FolderPath));
            sb.Append(separator);
            sb.Append(nameof(FileName));
            sb.Append(separator);
            sb.Append(nameof(Name));
            sb.Append(separator);
            sb.Append(nameof(DateCreatedUtc));
            sb.Append(separator);
            sb.Append(nameof(DurationSeconds));
            sb.Append(separator);
            sb.Append(nameof(SizeBytes));
            sb.Append(separator);
            sb.Append(nameof(Width));
            sb.Append(separator);
            sb.Append(nameof(Height));
            sb.Append(separator);
            sb.Append(nameof(AudioBitrate));
            sb.Append(separator);
            sb.Append(nameof(AudioIsVariableBitrate));
            sb.Append(separator);
            sb.Append(nameof(AudioChannels));
            sb.Append(separator);
            sb.Append(nameof(AudioSampleRate));
            sb.Append(separator);
            sb.Append(nameof(AudioBitsPerSample));
            sb.Append(separator);
            sb.Append(nameof(VideoDataRate));
            sb.Append(separator);
            sb.Append(nameof(VideoTotalBitrate));
            sb.Append(separator);
            sb.Append(nameof(VideoFrameRate));
            sb.Append(separator);
            sb.Append(nameof(Copyright));

            return sb.ToString();
        }

        public override string ToDelimitedText(string separator)
        {
#pragma warning disable U2U1108 // StringBuilders should be initialized with capacity
            var sb = new StringBuilder();
#pragma warning restore U2U1108 // StringBuilders should be initialized with capacity

            sb.Append(FilePath.Trim());
            sb.Append(separator);
            sb.Append(FolderPath.Trim());
            sb.Append(separator);
            sb.Append(FileName.Trim());
            sb.Append(separator);
            sb.Append(Name?.Trim());
            sb.Append(separator);
            sb.Append(DateCreatedUtc.ToLocalTime().ToString("s"));
            sb.Append(separator);
            sb.Append(DurationSeconds);
            sb.Append(separator);
            sb.Append(SizeBytes);
            sb.Append(separator);
            sb.Append(Width);
            sb.Append(separator);
            sb.Append(Height);
            sb.Append(separator);
            sb.Append(AudioBitrate);
            sb.Append(separator);
            sb.Append(AudioIsVariableBitrate);
            sb.Append(separator);
            sb.Append(AudioChannels);
            sb.Append(separator);
            sb.Append(AudioSampleRate);
            sb.Append(separator);
            sb.Append(AudioBitsPerSample);
            sb.Append(separator);
            sb.Append(VideoDataRate);
            sb.Append(separator);
            sb.Append(VideoTotalBitrate);
            sb.Append(separator);
            sb.Append(VideoFrameRate);
            sb.Append(separator);
            sb.Append(Copyright);

            return sb.ToString();
        }
    }
}
