using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MoreLinq;
using VidMetaData.Extractor.Base;
using VidMetaData.Models;

namespace VidMetaData.FileHandling
{
    internal sealed class MediaFileReader
    {
        public event EventHandler<ProgressEventArgs> ProgressEvent;

        public IEnumerable<AbstractMediaMetaData> Execute(IMetaDataExtractor extractor, string folder, bool includeSubFolders)
        {
            var files = Directory.EnumerateFiles(
                folder, 
                extractor.FileSearchPattern, 
                includeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            if (UseParallelProcessing)
            {
                var result = new List<AbstractMediaMetaData>();
                var locker = new object();
                const int batchSize = 10;

                var batches = files.Batch(batchSize);
#pragma warning disable PH_S022 // Parallel.For with Monitor Synchronization
                Parallel.ForEach(
                    batches, 
                    batch =>
                    {
                        var localResult = ProcessBatch(extractor, batch);

                        lock (locker)
                        {
                            result.AddRange(localResult);
                        }
                    });
#pragma warning restore PH_S022 // Parallel.For with Monitor Synchronization

                return result;
            }
            
            return ProcessBatch(extractor, files);
        }
        
        public bool UseParallelProcessing { get; set; }

        private IEnumerable<AbstractMediaMetaData> ProcessBatch(
            IMetaDataExtractor extractor,
            IEnumerable<string> files)
        {
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
