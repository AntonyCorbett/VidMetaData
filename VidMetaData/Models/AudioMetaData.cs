﻿using System.Text;

namespace VidMetaData.Models
{
    internal sealed class AudioMetaData : AbstractMediaMetaData
    {
        public int DurationSeconds { get; set; }

        public int AudioBitrate { get; set; }

        public bool AudioIsVariableBitrate { get; set; }

        public int AudioChannels { get; set; }

        public double AudioSampleRate { get; set; }

        public int AudioBitsPerSample { get; set; }

        public string Copyright { get; set; }

        public string Author { get; set; }

        public string Composer{ get; set; }

        public string Artist { get; set; }

        public string AlbumTitle { get; set; }

        public string Genre { get; set; }

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
            sb.Append(nameof(SizeBytes));
            sb.Append(separator);
            sb.Append(nameof(DurationSeconds));
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
            sb.Append(nameof(Copyright));
            sb.Append(separator);
            sb.Append(nameof(Author));
            sb.Append(separator);
            sb.Append(nameof(Composer));
            sb.Append(separator);
            sb.Append(nameof(Artist));
            sb.Append(separator);
            sb.Append(nameof(AlbumTitle));
            sb.Append(separator);
            sb.Append(nameof(Genre));

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
            sb.Append(SizeBytes);
            sb.Append(separator);
            sb.Append(DurationSeconds);
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
            sb.Append(Copyright);
            sb.Append(separator);
            sb.Append(Author);
            sb.Append(separator);
            sb.Append(Composer);
            sb.Append(separator);
            sb.Append(Artist);
            sb.Append(separator);
            sb.Append(AlbumTitle);
            sb.Append(separator);
            sb.Append(Genre);
            
            return sb.ToString();
        }
    }
}
