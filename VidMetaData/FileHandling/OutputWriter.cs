using System.Collections.Generic;
using System.IO;
using System.Text;
using VidMetaData.Models;

namespace VidMetaData.FileHandling
{
    internal class OutputWriter
    {
        private const char Separator = '\t';

        public void Execute(string outputFilePath, IReadOnlyCollection<VideoMetaData> metaDataCollection)
        {
            File.WriteAllText(outputFilePath, ConvertToTabSeparatedText(metaDataCollection), Encoding.UTF8);
        }

        private string ConvertToTabSeparatedText(IReadOnlyCollection<VideoMetaData> metaDataCollection)
        {
            var sb = new StringBuilder();
            
            sb.Append(nameof(VideoMetaData.FileName));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.Name));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.DateCreatedUtc));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.DurationSeconds));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.SizeBytes));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.Width));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.Height));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.AudioBitrate));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.AudioIsVariableBitrate));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.AudioChannels));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.AudioSampleRate));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.AudioBitsPerSample));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.VideoDataRate));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.VideoTotalBitrate));
            sb.Append(Separator);
            sb.Append(nameof(VideoMetaData.VideoFrameRate));
            sb.AppendLine();

            foreach (var metaData in metaDataCollection)
            {
                sb.Append(metaData.FileName.Trim());
                sb.Append(Separator);
                sb.Append(metaData.Name.Trim());
                sb.Append(Separator);
                sb.Append(metaData.DateCreatedUtc.ToLocalTime().ToString("s"));
                sb.Append(Separator);
                sb.Append(metaData.DurationSeconds);
                sb.Append(Separator);
                sb.Append(metaData.SizeBytes);
                sb.Append(Separator);
                sb.Append(metaData.Width);
                sb.Append(Separator);
                sb.Append(metaData.Height);
                sb.Append(Separator);
                sb.Append(metaData.AudioBitrate);
                sb.Append(Separator);
                sb.Append(metaData.AudioIsVariableBitrate);
                sb.Append(Separator);
                sb.Append(metaData.AudioChannels);
                sb.Append(Separator);
                sb.Append(metaData.AudioSampleRate);
                sb.Append(Separator);
                sb.Append(metaData.AudioBitsPerSample);
                sb.Append(Separator);
                sb.Append(metaData.VideoDataRate);
                sb.Append(Separator);
                sb.Append(metaData.VideoTotalBitrate);
                sb.Append(Separator);
                sb.Append(metaData.VideoFrameRate);

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
