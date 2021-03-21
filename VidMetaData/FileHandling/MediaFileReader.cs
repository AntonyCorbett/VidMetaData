using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MoreLinq;
using VidMetaData.Extractor.Base;
using VidMetaData.Models;

namespace VidMetaData.FileHandling
{
    internal class MediaFileReader
    {
        public event EventHandler<ProgressEventArgs> ProgressEvent;

        public IEnumerable<AbstractMediaMetaData> Execute(IMetaDataExtractor extractor, string folder, bool includeSubFolders)
        {
            var files = Directory.EnumerateFiles(
                folder, 
                extractor.FileSearchPattern, 
                includeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            if (UserParallelProcessing)
            {
                var result = new List<AbstractMediaMetaData>();
                var locker = new object();
                int batchSize = 10;

                var batches = files.Batch(batchSize);
                Parallel.ForEach(
                    batches, 
                    batch =>
                    {
                        lock (locker)
                        {
                            result.AddRange(ProcessBatch(extractor, batch));
                        }
                    });

                return result;
            }
            
            return ProcessBatch(extractor, files);
        }
        
        public bool UserParallelProcessing { get; set; }

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
