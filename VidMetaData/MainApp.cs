using System;
using System.IO;
using System.Linq;
using VidMetaData.Extractor;
using VidMetaData.Extractor.Base;
using VidMetaData.FileHandling;
using VidMetaData.Models;

namespace VidMetaData
{
    internal sealed class MainApp
    {
        public event EventHandler<ProgressEventArgs> ProgressEvent;

        public string Execute(IMetaDataExtractor extractor, string folder)
        {
            var reader = new MediaFileReader();
            ConfigureProgress(reader);

            var writer = new OutputWriter();

            var outputPath = GetOutputFilePath(folder, extractor.OutputFileName);
            writer.Execute(outputPath, reader.Execute(extractor, folder).ToArray());

            return outputPath;
        }

        private void ConfigureProgress(MediaFileReader reader)
        {
            if (ProgressEvent != null)
            {
                reader.ProgressEvent += delegate (object sender, ProgressEventArgs args)
                {
                    ProgressEvent?.Invoke(sender, args);
                };
            }
        }

        private string GetOutputFilePath(string folder, string fileName)
        {
            var baseName = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            return Path.Combine(folder, $"{baseName}-{DateTime.Now:yyyy-MM-dd hh_mm_ss}{ext}");
        }
    }
}
