using System;
using System.Collections.Generic;
using System.IO;
using VidMetaData.Extractor;
using VidMetaData.Models;

namespace VidMetaData.FileHandling
{
    internal class VidFileReader
    {
        public event EventHandler<ProgressEventArgs> ProgressEvent;

        public IEnumerable<VideoMetaData> Execute(IVidMetaDataExtractor extractor, string folder)
        {
            var files = System.IO.Directory.EnumerateFiles(folder, "*.mp4");

            foreach (var file in files)
            {
                var metaData = extractor.Extract(file);

                var filename = Path.GetFileName(file);
                if (metaData == null)
                {
                    OnProgressEvent(filename, "Could not extract metadata", error: true);
                    continue;
                }

                OnProgressEvent(filename, $"Processing {filename}");
                yield return metaData;
            }
        }

        private void OnProgressEvent(string filename, string text, bool error = false)
        {
            ProgressEvent?.Invoke(this, new ProgressEventArgs
            {
                FileName = filename,
                ProgressText = text,
                Error = error
            });
        }
    }
}
