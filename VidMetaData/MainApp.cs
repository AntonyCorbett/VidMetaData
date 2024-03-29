﻿using System;
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

        public (int count, string outputPath) Execute(
            IMetaDataExtractor extractor, 
            string folder, 
            bool includeSubFolders, 
            bool useParallelProcessing)
        {
            var reader = new MediaFileReader
            {
                UseParallelProcessing = useParallelProcessing
            };

            ConfigureProgress(reader);

            var writer = new OutputWriter();

            var outputPath = GetOutputFilePath(folder, extractor.OutputFileName);
            var count = writer.Execute(outputPath, reader.Execute(extractor, folder, includeSubFolders).ToArray());

            return (count, outputPath);
        }

        private void ConfigureProgress(MediaFileReader reader)
        {
            if (ProgressEvent != null)
            {
                reader.ProgressEvent += (sender, args) => ProgressEvent?.Invoke(sender, args);
            }
        }

        private static string GetOutputFilePath(string folder, string fileName)
        {
            var baseName = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            return Path.Combine(folder, $"{baseName}-{DateTime.Now:yyyy-MM-dd HH_mm_ss}{ext}");
        }
    }
}
