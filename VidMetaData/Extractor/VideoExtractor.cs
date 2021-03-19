using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using VidMetaData.Extractor.Base;
using VidMetaData.Models;

namespace VidMetaData.Extractor
{
    internal class VideoExtractor : AbstractMediaExtractor, IMetaDataExtractor
    {
        public AbstractMediaMetaData Extract(string filePath)
        {
            var fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                return null;
            }

            using (var video = ShellObject.FromParsingName(filePath))
            {
                var duration = video.Properties.GetProperty<ulong?>(SystemProperties.System.Media.Duration).Value;

                return new VideoMetaData
                {
                    FileName = filePath,
                    DateCreatedUtc = fi.CreationTimeUtc,
                    Name = GetStringValue(video.Properties, SystemProperties.System.Title),
                    DurationSeconds = duration == null ? 0 : (int) (duration.Value / 1E+7),
                    Height = GetIntegerValue(video.Properties, SystemProperties.System.Video.FrameHeight),
                    Width = GetIntegerValue(video.Properties, SystemProperties.System.Video.FrameWidth), SizeBytes = fi.Length,
                    AudioChannels = GetIntegerValue(video.Properties, SystemProperties.System.Audio.ChannelCount),
                    AudioBitrate = (int)(GetIntegerValue(video.Properties, SystemProperties.System.Audio.EncodingBitrate) / (double)1000),
                    AudioIsVariableBitrate = GetBoolValue(video.Properties, SystemProperties.System.Audio.IsVariableBitrate),
                    AudioSampleRate = GetIntegerValue(video.Properties, SystemProperties.System.Audio.SampleRate) / (double)1000,
                    AudioBitsPerSample = GetIntegerValue(video.Properties, SystemProperties.System.Audio.SampleSize),
                    VideoDataRate = GetIntegerValue(video.Properties, SystemProperties.System.Video.EncodingBitrate) / (double)1000,
                    VideoTotalBitrate = GetIntegerValue(video.Properties, SystemProperties.System.Video.TotalBitrate) / (double)1000,
                    VideoFrameRate = GetIntegerValue(video.Properties, SystemProperties.System.Video.FrameRate) / (double)1000,
                    Copyright = GetStringValue(video.Properties, SystemProperties.System.Copyright),
                };
            }
        }

        public string FileSearchPattern => "*.mp4";

        public string OutputFileName => "VideoMetaData.tsv";
    }
}
