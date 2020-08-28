using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using VidMetaData.Models;

namespace VidMetaData.Extractor
{
    internal class WinShellExtractor : IVidMetaDataExtractor
    {
        public VideoMetaData Extract(string filePath)
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
                    Name = video.Properties.GetProperty<string>(SystemProperties.System.Title).Value,
                    DurationSeconds = duration == null ? 0 : (int) (duration.Value / 1E+7),
                    Height = GetIntegerValue(video, SystemProperties.System.Video.FrameHeight),
                    Width = GetIntegerValue(video, SystemProperties.System.Video.FrameWidth),
                    SizeBytes = fi.Length,
                    AudioChannels = GetIntegerValue(video, SystemProperties.System.Audio.ChannelCount),
                    AudioBitrate = GetIntegerValue(video, SystemProperties.System.Audio.EncodingBitrate) / (double)1000,
                    AudioIsVariableBitrate = GetBoolValue(video, SystemProperties.System.Audio.IsVariableBitrate),
                    AudioSampleRate = GetIntegerValue(video, SystemProperties.System.Audio.SampleRate) / (double)1000,
                    AudioBitsPerSample = GetIntegerValue(video, SystemProperties.System.Audio.SampleSize),
                    VideoDataRate = GetIntegerValue(video, SystemProperties.System.Video.EncodingBitrate) / (double)1000,
                    VideoTotalBitrate = GetIntegerValue(video, SystemProperties.System.Video.TotalBitrate) / (double)1000,
                    VideoFrameRate = GetIntegerValue(video, SystemProperties.System.Video.FrameRate) / (double)1000
                };
            }
        }

        private bool GetBoolValue(ShellObject video, PropertyKey key)
        {
            var result = video.Properties.GetProperty<bool?>(key).Value;
            if (result == null)
            {
                return false;
            }

            return result.Value;
        }

        private int GetIntegerValue(ShellObject video, PropertyKey key)
        {
            var result = video.Properties.GetProperty<uint?>(key).Value;
            if (result == null)
            {
                return 0;
            }

            return (int)result.Value;
        }
    }
}
