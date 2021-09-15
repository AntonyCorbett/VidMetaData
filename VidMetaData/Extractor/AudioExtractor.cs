using System.IO;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using VidMetaData.Extractor.Base;
using VidMetaData.Models;

namespace VidMetaData.Extractor
{
    internal sealed class AudioExtractor : AbstractMediaExtractor, IMetaDataExtractor
    {
        public AbstractMediaMetaData Extract(string filePath)
        {
            var fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                return null;
            }

            using (var audio = ShellObject.FromParsingName(filePath))
            {
                var duration = audio.Properties.GetProperty<ulong?>(SystemProperties.System.Media.Duration).Value;

                return new AudioMetaData
                {
                    FilePath = filePath,
                    DateCreatedUtc = fi.CreationTimeUtc,
                    Name = GetStringValue(audio.Properties, SystemProperties.System.Title),
                    DurationSeconds = duration == null ? 0 : (int)(duration.Value / 1E+7),
                    SizeBytes = fi.Length,
                    AudioChannels = GetIntegerValue(audio.Properties, SystemProperties.System.Audio.ChannelCount),
                    AudioBitrate = (int)(GetIntegerValue(audio.Properties, SystemProperties.System.Audio.EncodingBitrate) / (double)1000),
                    AudioIsVariableBitrate = GetBoolValue(audio.Properties, SystemProperties.System.Audio.IsVariableBitrate),
                    AudioSampleRate = GetIntegerValue(audio.Properties, SystemProperties.System.Audio.SampleRate) / (double)1000,
                    AudioBitsPerSample = GetIntegerValue(audio.Properties, SystemProperties.System.Audio.SampleSize),
                    Author = GetStringValue(audio.Properties, SystemProperties.System.Author),
                    Copyright = GetStringValue(audio.Properties, SystemProperties.System.Copyright),
                    Composer = GetStringValue(audio.Properties, SystemProperties.System.Music.Composer),
                    Artist = GetStringValue(audio.Properties, SystemProperties.System.Music.Artist),
                    AlbumTitle = GetStringValue(audio.Properties, SystemProperties.System.Music.AlbumTitle),
                    Genre = GetStringValue(audio.Properties, SystemProperties.System.Music.Genre),
                };
            }
        }

        public string FileSearchPattern => "*.mp3";

        public string OutputFileName => "AudioMetaData.tsv";
    }
}
