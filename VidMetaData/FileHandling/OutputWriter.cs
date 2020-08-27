﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using VidMetaData.Models;

namespace VidMetaData.FileHandling
{
    internal class OutputWriter
    {
        private const char Separator = '\t';

        public void Execute(string outputFilePath, IReadOnlyCollection<VideoMetaData> metaDataCollection)
        {
            File.WriteAllText(outputFilePath, ConvertToTabSeparatedText(metaDataCollection));
        }

        private string ConvertToTabSeparatedText(IReadOnlyCollection<VideoMetaData> metaDataCollection)
        {
            var sb = new StringBuilder();

            foreach (var metaData in metaDataCollection)
            {
                sb.Append(metaData.FileName.Trim());
                sb.Append(Separator);
                sb.Append(metaData.Name.Trim());
                sb.Append(Separator);
                sb.Append(metaData.DateCreatedUtc.ToLocalTime().ToString("s"));
                sb.Append(Separator);
                sb.Append(metaData.DurationSeconds);
                sb.Append(Separator);
                sb.Append(metaData.SizeBytes);
                sb.Append(Separator);
                sb.Append(metaData.Width);
                sb.Append(Separator);
                sb.Append(metaData.Height);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}