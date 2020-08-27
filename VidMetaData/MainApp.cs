using System;
using System.IO;
using System.Linq;
using VidMetaData.Extractor;
using VidMetaData.FileHandling;
using VidMetaData.Models;

namespace VidMetaData
{
    internal sealed class MainApp
    {
        private const string OutputFileName = "VidMetaData.tsv";

        public event EventHandler<ProgressEventArgs> ProgressEvent;

        public string Execute(IVidMetaDataExtractor extractor, string folder)
        {
            var reader = new VidFileReader();
            ConfigureProgress(reader);

            var writer = new OutputWriter();

            var outputPath = GetOutputFilePath(folder);
            writer.Execute(outputPath, reader.Execute(extractor, folder).ToArray());

            return outputPath;
        }

        private void ConfigureProgress(VidFileReader reader)
        {
            if (ProgressEvent != null)
            {
                reader.ProgressEvent += delegate (object sender, ProgressEventArgs args)
                {
                    ProgressEvent?.Invoke(sender, args);
                };
            }
        }

        private string GetOutputFilePath(string folder)
        {
            var baseName = Path.GetFileNameWithoutExtension(OutputFileName);
            var ext = Path.GetExtension(OutputFileName);
            return Path.Combine(folder, $"{baseName}-{DateTime.Now:yyyy-MM-dd hh_mm_ss}{ext}");
        }
    }
}
