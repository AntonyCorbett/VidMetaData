using System;
using System.Collections.Generic;
using System.IO;
using VidMetaData.Extractor;
using VidMetaData.Extractor.Base;
using VidMetaData.Models;

namespace VidMetaData.FileHandling
{
    internal class MediaFileReader
    {
        public event EventHandler<ProgressEventArgs> ProgressEvent;

        public IEnumerable<AbstractMediaMetaData> Execute(IMetaDataExtractor extractor, string folder)
        {
            var files = Directory.EnumerateFiles(folder, extractor.FileSearchPattern);

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
